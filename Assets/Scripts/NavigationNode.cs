using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
	public class NavigationNode : INavigationNode
	{
		private List<INavigationNode> _neighbours = new List<INavigationNode>();
		private Dictionary<Influences, byte> _influences = new Dictionary<Influences, byte>();

		public ReadOnlyCollection<INavigationNode> Neighbours
		{
			get
			{
				return _neighbours.AsReadOnly();
			}
		}

		public bool IsPassable { get; set; }
		public Vector3 Position { get; private set; }

		//
		//	Constructors
		//
		public NavigationNode(Vector3 position, bool isPassable = true) 
			: this(position, new List<INavigationNode>(), new Dictionary<Influences, byte>(), isPassable)
		{ }

		public NavigationNode(Vector3 position, IList<INavigationNode> neighbours, bool isPassable = true) 
			: this(position, neighbours, new Dictionary<Influences, byte>(), isPassable)
		{ }

		public NavigationNode(Vector3 position, IList<INavigationNode> neighbours, IDictionary<Influences, byte> influences, bool isPassable = true)
		{
			Position = position;
			_neighbours = (List<INavigationNode>)neighbours;
			_influences = (Dictionary<Influences, byte>)influences;
			IsPassable = IsPassable;
		}

		//
		// Neighbour List modification & access functions
		//		ConnectNeighbours & DisconnectNeighbours functions are static to avoid recursion
		//
		public void AddNeighbour(INavigationNode neighbour)
		{
			if (!_neighbours.Contains(neighbour))
			{
				_neighbours.Add(neighbour);
			}

			Extension.PolarSort(_neighbours.Select(x => x.Position).ToArray(), Vector3.up, RotationDirection.Clockwise);
		}

		public static void ConnectNeighbours(INavigationNode nodeA, INavigationNode nodeB)
		{
			if (nodeA.Neighbours.Contains(nodeB) && nodeB.Neighbours.Contains(nodeA))
			{
				return;
			}

			if (!nodeA.Neighbours.Contains(nodeB))
			{
				nodeA.AddNeighbour(nodeB);
			}

			if (!nodeB.Neighbours.Contains(nodeA))
			{
				nodeB.AddNeighbour(nodeA);
			}
		}

		public static void DisconnectNeighbours(INavigationNode nodeA, INavigationNode nodeB)
		{
			if (!nodeA.Neighbours.Contains(nodeB) && !nodeB.Neighbours.Contains(nodeA))
			{
				return;
			}

			if (nodeA.Neighbours.Contains(nodeB))
			{
				nodeA.RemoveNeighbour(nodeB);
			}

			if (nodeB.Neighbours.Contains(nodeA))
			{
				nodeB.RemoveNeighbour(nodeA);
			}
		}

		public void RemoveNeighbour(INavigationNode neighbour)
		{
			if (_neighbours.Contains(neighbour))
			{
				_neighbours.Remove(neighbour);
			}
		}

		//
		//	Accessors for neighbour(s) relative position
		//
		public Vector3 GetNeighbourDirection(INavigationNode neighbour)
		{
			return neighbour.Position - Position;
		}

		public IList<Vector3> GetNeighbourDirections()
		{
			return _neighbours.Select(n => GetNeighbourDirection(n)).ToList();
		}

		//
		//	Node influence effector functions Get/Set & variations
		//
		public byte GetInfluence(Influences influences)
		{
			var totalInfluence = (byte)0;

			//	Combine the influence values of any existing influences
			foreach (Influences key in (Influences[])Enum.GetValues(typeof(Influences)))
			{
				if (key == Influences.None || !_influences.ContainsKey(key) || (influences & key) != key)
				{
					continue;
				}

				totalInfluence += _influences[key];
			}

			return totalInfluence;
		}

		public void IncrementInfluence(Influences influences, sbyte value)
		{
			foreach (Influences key in (Influences[])Enum.GetValues(typeof(Influences)))
			{
				if (key == Influences.None || (influences & key) != key)
				{
					continue;
				}

				var containsKey = _influences.ContainsKey(key);
				var baseValue = containsKey ? _influences[key] : (byte)0;
				var newValue = Extension.ClampToByte(baseValue + value);

				if (!containsKey)
				{
					_influences.Add(key, newValue);
				}
				else if (containsKey)
				{
					_influences[key] = newValue;
				}
			}
		}

		public void PropagateInfluences()
		{
			PropagateInfluences(~Influences.None, this);
		}

		public void PropagateInfluences(Influences influences, INavigationNode origin)
		{
			foreach (Influences key in (Influences[])Enum.GetValues(typeof(Influences)))
			{
				if (key == Influences.None || (influences & key) != key)
				{
					continue;
				}

				var unvisitedNodes = new Queue<INavigationNode>();

				unvisitedNodes.Enqueue(origin);

				while (unvisitedNodes.Count > 0)
				{
					var currentNode = unvisitedNodes.Dequeue();
					var currentNodeInfluence = currentNode.GetInfluence(key);
					if (currentNode != origin && currentNodeInfluence == 0)
					{
						currentNodeInfluence = byte.MaxValue;
					}

					foreach (var neighbour in currentNode.Neighbours)
					{
						var neighbourCost = Extension.CeilToByte((neighbour.Position - currentNode.Position).sqrMagnitude);
						var maxNeighbourInfluence = Extension.ClampToByte(neighbourCost + currentNodeInfluence);
						var neighbourInfluence = neighbour.GetInfluence(key);

						if (neighbour != origin && neighbourInfluence == 0)
						{
							neighbourInfluence = byte.MaxValue;
						}

						if (neighbourInfluence > maxNeighbourInfluence)
						{
							if (!unvisitedNodes.Contains(neighbour))
							{
								unvisitedNodes.Enqueue(neighbour);
							}

							neighbour.SetInfluence(key, maxNeighbourInfluence);
						}
					}
				}
			}
		}

		public void SetInfluence(Influences influences, byte value)
		{
			foreach (Influences key in (Influences[])Enum.GetValues(typeof(Influences)))
			{
				if (key == Influences.None || (influences & key) != key)
				{
					continue;
				}

				var containsKey = _influences.ContainsKey(key);

				if (!containsKey)
				{
					_influences.Add(key, value);
				}
				else if (containsKey)
				{
					_influences[key] = value;
				}
			}
		}

		public void ResetInfluence(Influences influences)
		{
			foreach (Influences key in (Influences[])Enum.GetValues(typeof(Influences)))
			{
				if (key == Influences.None || (influences & key) != key)
				{
					continue;
				}

				if (_influences.ContainsKey(key))
				{
					_influences.Remove(key);
				}
			}
		}

		//
		//	Object Overrides
		//
		public string InfluencesToString()
		{
			var s = "Influences: \n";

			foreach (var key in (Influences[])Enum.GetValues(typeof(Influences)))
			{
				s += "\t" + Enum.GetName(typeof(Influences), key) + " - " +
					(_influences.ContainsKey(key) ? _influences[key].ToString() : byte.MaxValue.ToString()) 
					+ "\n";
			}

			return s;
		}

		public string NeighboursToString()
		{
			var s = "Neighbours: \n";

			foreach (var neighbour in _neighbours)
			{
				s += "\t" + neighbour.Position + "\n";
			}

			return s;
		}

		public override string ToString()
		{
			return InfluencesToString() + "\n" + NeighboursToString();
		}

		public override int GetHashCode()
		{
			var hash = 17;

			hash = hash * 23 + base.GetHashCode();
			hash = hash * 23 + _neighbours.GetHashCode();
			hash = hash * 23 + _influences.GetHashCode();
			hash = hash * 23 + IsPassable.GetHashCode();
			hash = hash * 23 + Position.GetHashCode();

			return hash;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			return GetHashCode() == ((NavigationNode)obj).GetHashCode();
		}
	}
}
