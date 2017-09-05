//using System;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Assets.Scripts
//{
//    public static class NodeNeighbourManager
//	{
//		public static void ConnectNeighbours(Node node, params Node[] neighbours)
//		{
//			foreach (var neighbour in neighbours)
//			{
//				AddNeighbour(node, neighbour);
//				AddNeighbour(neighbour, node);
//			}
//		}

//		public static void DisconnectNeighbours(Node node, params Node[] neighbours)
//		{
//			foreach (var neighbour in neighbours)
//			{
//                RemoveNeighbour(node, neighbour);
//				RemoveNeighbour(neighbour, node);
//			}
//		}

//		public static IEnumerable<Vector3> GetNeighbourDirection(Node node, params Node[] neighbours)
//		{
//			foreach (var neighbourPosition in GetNeighbourRelativePositions(node, neighbours))
//            {
//                yield return neighbourPosition.normalized;
//            }
//		}

//		public static IEnumerable<Dictionary<Influences, int>> GetNeighbourInfluenceValues (Node node, params Node[] neighbours)
//		{
//			foreach (var neighbour in GetNeighbours(node, neighbours))
//            {
//                yield return neighbour.ActiveInfluenceValues;
//            }
//		}

//    	private static IEnumerable<Node> GetNeighbours(Node node, params Node[] neighbours)
//		{
//			foreach (var neighbour in neighbours)
//			{
//				if (!node.Neighbours.Contains(neighbour))
//                {
//                    continue;
//                }

//				yield return neighbour;
//			}
//		}

//        private static void RemoveNeighbour(Node node, Node neighbour)
//        {
//            if (node.Neighbours.Contains(neighbour))
//            {
//                node.Neighbours.Remove(neighbour);
//            }
//        }

//        private static void AddNeighbour(Node node, Node neighbour)
//        {
//            if (!node.Neighbours.Contains(neighbour))
//            {
//                node.Neighbours.Add(neighbour);
//            }
//        }
//	}
//}