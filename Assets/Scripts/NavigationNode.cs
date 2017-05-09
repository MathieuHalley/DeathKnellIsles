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
		private Dictionary<Influences, int> _nodeInfluences = new Dictionary<Influences, int>();
		private List<NavigationNode> list;

		public IList<INavigationNode> Neighbours
		{
			get
			{
				return _neighbours.AsReadOnly();
			}
		}
		public Vector3 Position { get; set; }

		public NavigationNode(Vector3 position) : this(
			position, 
			new List<INavigationNode>(), 
			new Dictionary<Influences, int>()) { }

		public NavigationNode(Vector3 position, IList<INavigationNode> neighbours) : this(
			position, neighbours,
			new Dictionary<Influences, int>()) { }


		public NavigationNode(
			Vector3 position, 
			IList<INavigationNode> neighbours, 
			IDictionary<Influences, int> influences)
		{
			Position = position;
			_neighbours = (List<INavigationNode>)neighbours;
			_nodeInfluences = (Dictionary<Influences, int>)influences;
		}

		public void AddNeighbour(INavigationNode neighbour)
		{
			if (_neighbours.Contains(neighbour) && neighbour.Neighbours.Contains(this)) return;
//			if (!neighbour.Neighbours.Contains(this)) neighbour.AddNeighbour(this);
			if (!_neighbours.Contains(neighbour)) _neighbours.Add(neighbour);
		}

		public int GetInfluence(Influences influences)
		{
			var totalInfluence = 0;
			Debug.Log(Convert.ToByte((int)influences).ToString());

			foreach (Influences key in Extension.IntToByteArray((int)influences))
			{
				if (key == Influences.None || !_nodeInfluences.ContainsKey(key)) continue;
				totalInfluence += _nodeInfluences[key];
				Debug.Log("totalInfluence:" + key.ToString() + " " + totalInfluence);
			}
			return totalInfluence;
		}

		public Vector3 GetNeighbourDirection(INavigationNode neighbour)
		{
			return neighbour.Position - Position;
		}

		public IList<Vector3> GetNeighbourDirections()
		{
			return _neighbours.Select(n => GetNeighbourDirection(n)).ToList(); 
		}

		public void IncrementInfluence(Influences influences, int value)
		{
			foreach (Influences key in Extension.IntToByteArray((int)influences))
			{
				if (key == Influences.None) continue;
				if (!_nodeInfluences.ContainsKey(key))
				{
					_nodeInfluences.Add(key, value);
					continue;
				}
				_nodeInfluences[key] += value;
			}
		}

		public void RemoveNeighbour(INavigationNode neighbour)
		{
			if (!_neighbours.Contains(neighbour) && !neighbour.Neighbours.Contains(this)) return;
//			if (neighbour.Neighbours.Contains(this)) neighbour.RemoveNeighbour(this);
			if (_neighbours.Contains(neighbour)) _neighbours.Remove(neighbour);
		}

		public void SetInfluence(Influences influences, int value)
		{
			foreach (Influences key in Extension.IntToByteArray((int)influences))
			{
				if (key == Influences.None) continue;
				if (!_nodeInfluences.ContainsKey(key))
				{
					_nodeInfluences.Add(key, value);
					continue;
				}
				_nodeInfluences[key] = value;
			}
		}
	}
}
