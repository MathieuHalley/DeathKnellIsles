//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;

//namespace Assets.Scripts
//{
//    public static class NodeInfluenceManager
//	{
//		public static Dictionary<Influences, int> IncrementInfluenceValues(Dictionary<Influences, int> influenceValues, IEnumerable<Influences> selectedInfluences, int delta)
//		{
//            var influencesToUpdate = from influence in influenceValues.Keys
//                                     where selectedInfluences.Contains(influence)
//                                     select influence;

//            foreach (var influence in influencesToUpdate)
//            {
//                influenceValues[influence] = influenceValues[influence] + delta;
//            }

//            return influenceValues;
//        }

//        public static int GetInfluenceValue(Dictionary<Influences, int> influenceValues, Influences selectedInfluence)
//        {
//            Debug.Log(selectedInfluence.ToString());
//            return influenceValues.ContainsKey(selectedInfluence) ? influenceValues[selectedInfluence] : 0;
//        }

//        public static IDictionary<Influences, int> GetInfluenceValues(Dictionary<Influences, int> influenceValues, IEnumerable<Influences> selectedInfluences)
//		{
//			return selectedInfluences.ToDictionary(influence => influence, influence => GetInfluenceValue(influenceValues, influence));
//		}

//        public static int GetInfluenceValuesSum(Dictionary<Influences, int> influenceValues, IEnumerable<Influences> selectedInfluences)
//        {
//            return GetInfluenceValues(influenceValues, selectedInfluences).Sum(influence => influence.Value);
//        }
        
//        public static void PropagateNodeInfluence(Node[] nodes, IEnumerable<Influences> selectedInfluences, int delta = -1, int floor = 0)
//		{
//			Debug.Assert(delta != 0, "PropagateNodeInfluence - delta is 0.");

//			var visitedNodes = new Queue<Node>(nodes);
//			var isDeltaNegative = delta < 0;

//			while (visitedNodes.Count > 0)
//			{
//				var activeNode = visitedNodes.Dequeue();

//				if (!activeNode.IsPassable)
//                {
//                    continue;
//                }

//				foreach (var neighbour in activeNode.Neighbours)
//				{
//					foreach (var influence in selectedInfluences)
//					{
//                        Debug.Log(neighbour.ActiveInfluenceValues.Count);
//                        Debug.Log(neighbour.ActiveInfluenceValues.ContainsKey(influence));
//                        var neighbourValue = neighbour.ActiveInfluenceValues.ContainsKey(influence) ? neighbour.ActiveInfluenceValues[influence] : 0;
//                        var newValue = (activeNode.ActiveInfluenceValues.ContainsKey(influence) ? activeNode.ActiveInfluenceValues[influence] : 0) + delta;

//						newValue = isDeltaNegative 
//							? Mathf.Clamp(newValue, int.MinValue, floor) 
//							: Mathf.Clamp(newValue, floor, int.MaxValue);

//						if ((isDeltaNegative && neighbourValue <= newValue) || (!isDeltaNegative && neighbourValue >= newValue))
//                        {
//                            continue;
//                        }

//						if (!visitedNodes.Contains(neighbour))
//                        {
//                            visitedNodes.Enqueue(neighbour);
//                        }

//                        neighbour.ActiveInfluenceValues = SetInfluenceValues(neighbour.ActiveInfluenceValues, new[] { influence }, newValue);
//					}
//				}
//			}
//		}

//		public static Dictionary<Influences, int> ResetInfluenceValues(Dictionary<Influences,int> influenceValues, IEnumerable<Influences> selectedInfluences)
//		{
//            return (from influence in influenceValues.Keys
//                    where !selectedInfluences.Contains(influence)
//                    select influence)
//                   .ToDictionary(influence => influence, influence => influenceValues[influence]);

//		}

//		public static Dictionary<Influences, int> SetInfluenceValues(Dictionary<Influences, int> influenceValues, Dictionary<Influences, int> newValues)
//		{
//            var influencesToUpdate = from influence in influenceValues.Keys
//                                     where newValues.ContainsKey(influence)
//                                     select influence;

//            foreach (var influence in influencesToUpdate)
//            {
//                influenceValues[influence] = newValues[influence];
//            }

//            return influenceValues;
//		}

//		public static Dictionary<Influences, int> SetInfluenceValues(Dictionary<Influences, int> influenceValues, IEnumerable<Influences> selectedInfluences, int newInfluenceValue)
//		{
//			var clampedValue = Mathf.Clamp(newInfluenceValue, 0, int.MaxValue);

//            if (clampedValue == 0)
//            {
//                return ResetInfluenceValues(influenceValues, selectedInfluences);
//            }
            
//            var influencesToUpdate = from influence in selectedInfluences
//                                     where selectedInfluences.Contains(influence)
//                                     select influence;


//            foreach (var influence in influencesToUpdate)
//			{
//				if (!influenceValues.ContainsKey(influence))
//                {
//                    influenceValues.Add(influence, clampedValue);
//                }
//				else
//                {
//                    influenceValues[influence] = clampedValue;
//                }
//			}

//            return influenceValues;
//		}
//	}
//}