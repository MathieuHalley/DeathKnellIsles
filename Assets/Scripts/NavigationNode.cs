﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
	public class NavigationNode : INavigationNode
	{
		private List<INavigationNode> _neighbours = new List<INavigationNode>();
		private Dictionary<Influences, byte> _nodeInfluences = new Dictionary<Influences, byte>();

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
		public NavigationNode()
			: this(
				Vector3.zero,
				new List<INavigationNode>(),
				new Dictionary<Influences, byte>(),
				true)
		{ }

		public NavigationNode(Vector3 position, bool isPassable = true) 
			: this(
				position, 
				new List<INavigationNode>(), 
				new Dictionary<Influences, byte>(),
				isPassable)
		{ }

		public NavigationNode(Vector3 position, IList<INavigationNode> neighbours, bool isPassable = true) 
			: this(
				position, 
				neighbours,
				new Dictionary<Influences, byte>(),
				isPassable)
		{ }

		public NavigationNode(
			Vector3 position, 
			IList<INavigationNode> neighbours, 
			IDictionary<Influences, byte> influences,
			bool isPassable = true)
		{
			Position = position;
			_neighbours = (List<INavigationNode>)neighbours;
			_nodeInfluences = (Dictionary<Influences, byte>)influences;
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
		public int GetInfluence(Influences influences)
		{
			var totalInfluence = 0;
			//	Combine the influence values of any existing influences
			foreach (Influences key in (Influences[])Enum.GetValues(typeof(Influences)))
			{
				if (key == Influences.None || !_nodeInfluences.ContainsKey(key) || (influences & key) != key)
				{
					continue;
				}

				totalInfluence += _nodeInfluences[key];
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

				var containsKey = _nodeInfluences.ContainsKey(key);

				var newValue = (byte)Mathf.Clamp(
					containsKey ? _nodeInfluences[key] : (byte)value,
					byte.MinValue,
					byte.MaxValue);

				if (!containsKey && newValue > 0)
				{
					_nodeInfluences.Add(key, newValue);
				}
				else if (containsKey && newValue <= 0)
				{
					_nodeInfluences.Remove(key);
				}
				else if (containsKey && newValue > 0)
				{
					_nodeInfluences[key] = newValue;
				}
			}
		}

		public void PropagateInfluence()
		{
			foreach (Influences key in (Influences[])Enum.GetValues(typeof(Influences)))
			{
				if (key == Influences.None || !_nodeInfluences.ContainsKey(key))
				{
					continue;
				}
				foreach (var neighbour in _neighbours)
				{
					if (neighbour.GetInfluence(key) > _nodeInfluences[key] - 1)
					{

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

				var containsKey = _nodeInfluences.ContainsKey(key);

				if (!containsKey && value > 0)
				{
					_nodeInfluences.Add(key, value);
				}
				else if (containsKey && value <= 0)
				{
					_nodeInfluences.Remove(key);
				}
				else if (containsKey && value > 0)
				{
					_nodeInfluences[key] = value;
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

				if (_nodeInfluences.ContainsKey(key))
				{
					_nodeInfluences.Remove(key);
				}
			}
		}

		//
		//	Object Overrides
		//
		public override string ToString()
		{
			var s = "";
			s += "Influences: \n";
			foreach (var influence in _nodeInfluences)
			{
				s += "\t" + Enum.GetName(typeof(Influences), influence.Key) + " - " + influence.Value + "\n";
			}

			s += "Neighbours: \n";
			foreach(var neighbour in _neighbours)
			{
				s += "\t" + neighbour.Position + "\n";
			}
			return s;
		}

		public override int GetHashCode()
		{
			var hash = 17;
			hash = hash * 23 + base.GetHashCode();
			hash = hash * 23 + _neighbours.GetHashCode();
			hash = hash * 23 + _nodeInfluences.GetHashCode();
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
