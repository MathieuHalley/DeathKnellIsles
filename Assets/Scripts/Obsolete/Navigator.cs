//using System;
//using System.Text;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;

//namespace Assets.Scripts
//{
//	public class Navigator
//	{
//		private readonly NavigatorController controller;
//		private static uint navigatorCount;

//		[SerializeField] private float acceleration;
//		[SerializeField] private GameObject gameObject;
//		[SerializeField] private Node previousNode;
//		[SerializeField] private ICollection<Influences> activeInfluences;
//		[SerializeField] private Node targetNode;
//		[SerializeField] private float topSpeed;

//		public readonly string ID;

//		public float Acceleration
//		{
//			get
//            {
//                return acceleration;
//            }

//			set
//			{
//				if (value != acceleration)
//                {
//                    acceleration = value;
//                }

				
//			}
//		}

//		public GameObject GameObject
//		{
//			get
//            {
//                return gameObject;
//            }

//			set
//			{
//				if (value != gameObject)
//                {
//                    gameObject = value;
//                }
//			}
//		}

//		public Node PreviousNode
//		{
//			get
//            {
//                return previousNode;
//            }

//			set
//			{
//				if (value != previousNode)
//                {
//                    previousNode = value;
//                }
//            }
//		}

//		public ICollection<Influences> ActiveInfluences
//		{
//			get
//            {
//                return activeInfluences;
//            }

//			set
//			{
//				if (value != activeInfluences)
//                {
//                    activeInfluences = value;
//                }
//			}
//		}

//		public Node TargetNode
//		{
//			get
//            {
//                return targetNode;
//            }

//			set
//			{
//				if (value != targetNode)
//                {
//                    targetNode = value;
//                }
//			}
//		}

//		public float TopSpeed
//		{
//			get
//            {
//                return topSpeed;
//            }

//			set
//			{
//				if (value != topSpeed)
//                {
//                    topSpeed = value;
//                }
//			}
//		}

//		public Navigator()
//		{
//			acceleration = 100f;
//			controller = new NavigatorController();
//			ID = "Navigator: " + (navigatorCount++).ToString();
//			gameObject = null;
//			previousNode = null;
//            activeInfluences = (ICollection<Influences>)Enumerable.Empty<Influences>();
//			targetNode = null;
//			topSpeed = 1f;
//		}

//		/// <summary></summary>
//		/// <param name="ignoredInfluences"></param>
//		public void IdentifyNextTargetNode(IEnumerable<Influences> selectedInfluences)
//		{
//            Debug.Assert(TargetNode != null, "There is no current TargetNode.");

//            Node newTargetNode = null;
//            var highestInfluenceValue = 0;
//			var targetNodeInfluenceValuesSum = NodeInfluenceManager.GetInfluenceValuesSum(TargetNode.ActiveInfluenceValues, selectedInfluences);
			
//			foreach (var neighbour in TargetNode.Neighbours)
//			{
//				var neighbourInfluenceValuesSum = NodeInfluenceManager.GetInfluenceValuesSum(neighbour.ActiveInfluenceValues, selectedInfluences);

//				if (neighbourInfluenceValuesSum <= targetNodeInfluenceValuesSum || neighbourInfluenceValuesSum <= highestInfluenceValue)
//                {
//                    continue;
//                }

//                highestInfluenceValue = neighbourInfluenceValuesSum;
//				newTargetNode = neighbour;
//			}

//			if (newTargetNode != null)
//			{
//				PreviousNode = TargetNode;
//				TargetNode = newTargetNode;
//			}
//		}

//		/// <summary></summary>
//		/// <returns></returns>
//		public string NavigatorInfluencesToString()
//		{
//            var influencesToStringBuilder = new StringBuilder("Active Navigator Influences: ");
//            var influenceCount = ActiveInfluences.Count();
//            var influenceList = new List<Influences>(ActiveInfluences);

//            for (var i = 0; i < influenceCount; i++)
//			{
//                influencesToStringBuilder.Append(Enum.GetName(typeof(Influences), influenceList[i]));

//                if (i < influenceCount)
//                {
//                    influencesToStringBuilder.Append(", ");
//                }
//			}

//			return influencesToStringBuilder.ToString();
//		}

//		public override string ToString()
//		{
//            return new StringBuilder()
//                .AppendLine(NavigatorInfluencesToString())
//                .AppendLine("Previous Node: " + PreviousNode == null ? PreviousNode.NodeInfluencesToString() : "None")
//                .AppendLine("Target Node: " + TargetNode == null ? TargetNode.NodeInfluencesToString() : "None")
//                .ToString();
//		}

//		public override int GetHashCode()
//		{
//			var hash = 17;

//			hash += hash * 23 + Acceleration.GetHashCode();
//			hash += hash * 23 + PreviousNode.GetHashCode();
//			hash += hash * 23 + ActiveInfluences.GetHashCode();
//			hash += hash * 23 + TargetNode.GetHashCode();
//			hash += hash * 23 + TopSpeed.GetHashCode();

//			return hash;
//		}

//		public override bool Equals(object obj)
//		{
//			return obj != null ? GetHashCode() == ((Navigator)obj).GetHashCode() : false;
//		}
//	}
//}
