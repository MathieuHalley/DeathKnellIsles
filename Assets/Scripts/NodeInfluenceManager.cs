using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Assets.Scripts
{
	[Flags]
	public enum Influences
	{
		None = 0,                   //	0
		Boat = 1 << 0,              //	1
		Tower = 1 << 1,             //	2
		House = 1 << 2,             //	4
		Attacker = 1 << 3,          //	8
		Defender = 1 << 4,          //	16
		Villager = 1 << 5,          //	32
		HideBell = 1 << 6,          //	64
		PowerBell = 1 << 7,         //	128
		RallyBell = 1 << 8,         //	256
		SleepBell = 1 << 9,         //	512
		RetreatBell = 1 << 10,      //	1024
		RaiseBell = 1 << 11,        //	2048
		IndimidationBell = 1 << 12, //	4196
	}

	public static class NodeInfluenceManager
	{
		private static Func<Dictionary <Influences, int>, Influences, int> 
			GetInfluence = (influences, key) => influences.ContainsKey(key) ? influences[key] : 0;

		public static void IncrementInfluenceValues(Node node, Influences selectedInfluences, int delta)
		{
			foreach (var key in selectedInfluences.GetFlags())
			{
				var currentValue = GetInfluence(node.Data.Influences, key);
				var newValue = Mathf.Clamp(currentValue + delta, 0, int.MaxValue);

				SetInfluenceValues(node, selectedInfluences, newValue);
			}

			node.Data.Events.OnInfluencesUpdated(node, node.Data);
		}

		public static void IncrementInfluenceValues(Node[] nodes, Influences selectedInfluences, int delta)
		{
			foreach (var node in nodes)
			{
				IncrementInfluenceValues(node, selectedInfluences, delta);
			}
		}

		public static int GetTotalInfluenceValue(Node node, Influences selectedInfluences)
		{
			return selectedInfluences.GetFlags().Sum(influence => GetInfluence(node.Data.Influences, influence));
		}

		public static IDictionary<Influences, int> GetInfluenceValues(Node node, Influences selectedInfluences)
		{
			return selectedInfluences.GetFlags().ToDictionary(k => k, v => GetInfluence(node.Data.Influences, v));
		}

		public static void PropagateNodeInfluence(Node[] nodes, Influences selectedInfluences)
		{
			var visitedNodes = new Queue<Node>(nodes);

			while (visitedNodes.Count > 0)
			{
				var activeNode = visitedNodes.Dequeue();

				if (!activeNode.Data.IsPassable)
				{
					continue;
				}

				foreach (var neighbour in activeNode.Data.Neighbours)
				{
					foreach (var influence in selectedInfluences.GetFlags())
					{
						var currentValue = GetInfluence(activeNode.Data.Influences,influence);
						var currentNeighbourValue = GetInfluence(neighbour.Data.Influences, influence);
						var newValue = Mathf.Clamp(currentValue - 1, 0, int.MaxValue);

						if (currentNeighbourValue < newValue)
						{
							if (!visitedNodes.Contains(neighbour))
							{
								visitedNodes.Enqueue(neighbour);
							}

							SetInfluenceValues(neighbour, influence, newValue);
						}
					}
				}
			}
		}

		public static void ResetInfluenceValues(Node node, Influences selectedInfluences)
		{
			node.Data.Influences = ToDictionary(
				node.Data.Influences.Where(pair => (pair.Key & selectedInfluences) == 0));
			node.Data.Events.OnInfluencesUpdated(node, node.Data);
		}

		public static void ResetInfluenceValues(Node[] nodes, Influences selectedInfluences)
		{
			foreach (var node in nodes)
			{
				ResetInfluenceValues(node, selectedInfluences);
			}
		}

		public static void SetInfluenceValues(Node node, Dictionary<Influences, int> newInfluences)
		{
			node.Data.Influences = ToDictionary(
				node.Data.Influences.Concat(
					newInfluences.Where(pair => !node.Data.Influences.ContainsKey(pair.Key))));


			foreach (var key in newInfluences.Keys)
			{
				node.Data.Influences[key] = newInfluences[key];
			}

			node.Data.Events.OnInfluencesUpdated(node, node.Data);
		}

		public static void SetInfluenceValues(Node[] nodes, Dictionary<Influences, int> newInfluences)
		{
			foreach(var node in nodes)
			{
				SetInfluenceValues(node, newInfluences);
			}
		}

		public static void SetInfluenceValues(Node node, Influences selectedInfluences, int value)
		{
			var clampedValue = Mathf.Clamp(value, 0, int.MaxValue);
			var isClampedValuePositive = clampedValue > 0;

			foreach (var key in selectedInfluences.GetFlags())
			{
				if (!node.Data.Influences.ContainsKey(key) && isClampedValuePositive)
				{
					node.Data.Influences.Add(key, clampedValue);
				}
				else if (!isClampedValuePositive)
				{
					node.Data.Influences.Remove(key);
				}
				else
				{
					node.Data.Influences[key] = clampedValue;
				}
			}

			node.Data.Events.OnInfluencesUpdated(node, node.Data);

		}

		public static void SetInfluenceValues(Node[] nodes, Influences selectedInfluences, int value)
		{
			foreach(var node in nodes)
			{
				SetInfluenceValues(node, selectedInfluences, value);
			}
		}



		private static Dictionary<T, U> ToDictionary<T, U>(IEnumerable<KeyValuePair<T, U>> pairs)
		{
			return pairs.ToDictionary(pair => pair.Key, pair => pair.Value);
		}
	}
}