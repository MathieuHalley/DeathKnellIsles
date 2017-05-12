using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
	public class NavigationNode : INavigationNode
	{
		private List<INavigationNode> _neighbours = new List<INavigationNode>();
		private IInfluenceController _nodeInfluences = new InfluenceController();
		private IObjectCollection _nodeObjects = new ObjectCollection();

		public ReadOnlyCollection<INavigationNode> Neighbours { get { return _neighbours.AsReadOnly(); } }
		public bool IsPassable { get; private set; }
		public Vector3 Position { get; private set; }
		public IInfluenceController NodeInfluences { get { return _nodeInfluences; } }
		public IObjectCollection NodeObjects { get { return _nodeObjects; } }

		//
		//	Constructors
		//
		public NavigationNode(bool isPassable = true) : this(new Vector3(), isPassable)
		{ }

		public NavigationNode(Vector3 position, bool isPassable = true)
		{
			Position = position;
			IsPassable = isPassable;
		}


		public static void ConnectNeighbours(INavigationNode nodeA, INavigationNode nodeB)
		{
			if (nodeA.Neighbours.Contains(nodeB) && nodeB.Neighbours.Contains(nodeA))
			{
				return;
			}

			nodeA.AddNeighbour(nodeB);
			nodeB.AddNeighbour(nodeA);
		}

		public static void DisconnectNeighbours(INavigationNode nodeA, INavigationNode nodeB)
		{
			if (!nodeA.Neighbours.Contains(nodeB) && !nodeB.Neighbours.Contains(nodeA))
			{
				return;
			}

			nodeA.RemoveNeighbour(nodeB);
			nodeB.RemoveNeighbour(nodeA);
		}

		//
		// Neighbour List modification & access functions
		//		ConnectNeighbours & DisconnectNeighbours functions are static to avoid recursion
		//
		public void SortNeighbours(INavigationNode neighbour)
		{
			Extension.PolarSort(Neighbours.Select(x => x.Position).ToArray(), Vector3.up, RotationDirection.Clockwise);
		}

		public void AddNeighbour(INavigationNode node)
		{
			if (_neighbours.Contains(node) || node == this)
			{
				return;
			}

			_neighbours.Add(node);
		}

		public void RemoveNeighbour(INavigationNode node)
		{
			if (!_neighbours.Contains(node))
			{
				return;
			}

			_neighbours.Remove(node);
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
			return Neighbours.Select(n => GetNeighbourDirection(n)).ToList();
		}

		public string NeighboursToString()
		{
			var s = "Neighbours: \n";

			foreach (var neighbour in Neighbours)
			{
				s += "\t" + neighbour.Position + "\n";
			}

			return s;
		}

		public override string ToString()
		{
			return NeighboursToString() + "\n" + _nodeInfluences.ToString();
		}

		public override int GetHashCode()
		{
			var hash = 17;

			hash = hash * 23 + base.GetHashCode();
			hash = hash * 23 + Neighbours.GetHashCode();
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
