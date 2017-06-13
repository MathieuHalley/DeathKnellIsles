using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{ 
	public static class NodeNeighbourManager
	{
		/// <summary>Connects this Node to a list of Nodes as neighbours.</summary>
		/// <param name="neighbours">The Nodes that will be added to this Node's neighbours.</param>
		public static void ConnectNeighbours(Node node, params Node[] neighbours)
		{
			Action<Node, Node> addNeighbour = (nodeA, nodeB) =>
				{
					if (!nodeA.Data.Neighbours.Contains(nodeB))
					{
						nodeA.Data.Neighbours.Add(nodeB);
						nodeA.Data.Events.OnNeighboursUpdated(nodeA, nodeA.Data);
					}
				};

			foreach (var neighbour in neighbours)
			{
				addNeighbour(node, neighbour);
				addNeighbour(neighbour, node);
			}
		}

		/// <summary>Disconnects this Node from a list of its neighbours.</summary>
		/// <param name="node">The Nodes that will be removed this Node's neighbours.</param>
		public static void DisconnectNeighbours(Node node, params Node[] neighbours)
		{
			Action<Node, Node> removeNeighbour = (nodeA, nodeB) =>
				{
					if (nodeA.Data.Neighbours.Contains(nodeB))
					{
						nodeA.Data.Neighbours.Remove(nodeB);
						nodeA.Data.Events.OnNeighboursUpdated(nodeA, nodeA.Data);
					}
				};

			foreach (var neighbour in neighbours)
			{
				removeNeighbour(node, neighbour);
				removeNeighbour(neighbour, node);
			}
		}
	}
}