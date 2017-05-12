using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{

	public class InfluenceController : IInfluenceController
	{

		private Dictionary<Influences, byte> _influenceValues = new Dictionary<Influences, byte>();
//		public Dictionary<Influences, byte> InfluenceValues { get { return _influenceValues; } }

		private static IList<Influences> IdentifyIndividualInfluences(Influences influences)
		{
			var influenceCollection = new List<Influences>();

			foreach (var influence in (Influences[])Enum.GetValues(typeof(Influences)))
			{
				if (influence == Influences.None || (influences & influence) != influence)
				{
					continue;
				}

				influenceCollection.Add(influence);
			}

			return influenceCollection;
		}

		public static void PropagateInfluences(Influences influences, INavigationNode originNode)
		{
			foreach (var influence in IdentifyIndividualInfluences(influences))
			{
				var unvisitedNodes = new Queue<INavigationNode>();

				unvisitedNodes.Enqueue(originNode);

				while (unvisitedNodes.Count > 0)
				{
					var activeNode = unvisitedNodes.Dequeue();

					if (activeNode.IsPassable)
					{
						PropagateInfluenceToNeighbours(activeNode, influence, ref unvisitedNodes);
					}
				}
			}
		}

		private static void PropagateInfluenceToNeighbours(INavigationNode node, Influences influence, ref Queue<INavigationNode> unvisitedNodes)
		{
			var influenceValue = node.NodeInfluences.GetCollatedInfluenceValue(influence);

			foreach (var neighbour in node.Neighbours)
			{
				var neighbourCost = Extension.CeilToByte((neighbour.Position - node.Position).sqrMagnitude);
				var newInfluenceValue = Extension.ClampToByte(influenceValue - neighbourCost);

				if (neighbour.NodeInfluences.GetCollatedInfluenceValue(influence) < newInfluenceValue)
				{
					if (!unvisitedNodes.Contains(neighbour))
					{
						unvisitedNodes.Enqueue(neighbour);
					}

					neighbour.NodeInfluences.SetInfluenceValues(influence, newInfluenceValue);
				}
			}
		}

		public Influences GetActiveInfluences()
		{
			var totalInfluence = Influences.None;

			//	Combine the influence values of any existing influences
			foreach (var influence in IdentifyIndividualInfluences(~Influences.None))
			{
				if (!_influenceValues.ContainsKey(influence))
				{
					continue;
				}

				totalInfluence |= influence;
			}

			return totalInfluence;
		}

		public byte GetCollatedInfluenceValue(Influences influences)
		{
			var totalInfluence = (byte)0;

			//	Combine the influence values of any existing influences
			foreach (var influence in IdentifyIndividualInfluences(influences))
			{
				if (!_influenceValues.ContainsKey(influence))
				{
					continue;
				}

				totalInfluence += _influenceValues[influence];
			}

			return totalInfluence;
		}

		public void IncrementInfluenceValues(Influences influences, sbyte value)
		{
			foreach (var influence in IdentifyIndividualInfluences(influences))
			{
				var containsKey = _influenceValues.ContainsKey(influence);
				var baseValue = containsKey ? _influenceValues[influence] : (byte)0;
				var newValue = Extension.ClampToByte(baseValue + value);

				if (!containsKey)
				{
					_influenceValues.Add(influence, newValue);
				}
				else
				{
					_influenceValues[influence] = newValue;
				}
			}
		}

		public void SetInfluenceValues(Influences influences, byte value)
		{
			foreach (var influence in IdentifyIndividualInfluences(influences))
			{
				var containsKey = _influenceValues.ContainsKey(influence);

				if (!containsKey)
				{
					_influenceValues.Add(influence, value);
				}
				else
				{
					_influenceValues[influence] = value;
				}
			}
		}

		public void ResetInfluenceValues(Influences influences = ~Influences.None)
		{
			foreach (var influence in IdentifyIndividualInfluences(influences))
			{

				if (_influenceValues.ContainsKey(influence))
				{
					_influenceValues.Remove(influence);
				}
			}
		}

		public override string ToString()
		{
			var s = "Influences: \n";

			foreach (var influence in IdentifyIndividualInfluences(GetActiveInfluences()))
			{
				s += "\t" + Enum.GetName(typeof(Influences), influence) + " - " + _influenceValues[influence].ToString() + "\n";
			}

			return s;
		}

		public override int GetHashCode()
		{
			var hash = 17;

			hash = hash * 23 + base.GetHashCode();
			hash = hash * 23 + _influenceValues.GetHashCode();

			return hash;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			return GetHashCode() == ((IInfluenceController)obj).GetHashCode();
		}
	}
}