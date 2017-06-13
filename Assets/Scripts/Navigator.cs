using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class Navigator
	{
		public NavigatorController Controller;
		public NavigatorData Data;

		public Vector3 PreviousNodePosition { get { return Data.PreviousNode.Data.Position; } }
		public Vector3 TargetNodePosition { get { return Data.TargetNode.Data.Position; } }

		public Navigator()
		{
			Data = new NavigatorData(this);
			Controller = new NavigatorController(this);
		}

		public void IdentifyNextTargetNode(Influences ignoredInfluences = Influences.None)
		{
			Node newTargetNode = null;
			var highestInfluence = 0;
			var chosenPriorities = Data.Priorities & ~ignoredInfluences;
			var targetNodeInfluences = NodeInfluenceManager.GetTotalInfluenceValue(Data.TargetNode, chosenPriorities);
			
			if (Data.TargetNode == null)
			{
				Debug.Log("There is no current TargetNode.");
			}

			foreach (var neighbour in Data.TargetNode.Data.Neighbours)
			{
				var neighbourInfluences = NodeInfluenceManager.GetTotalInfluenceValue(neighbour, chosenPriorities);

				if (neighbourInfluences <= targetNodeInfluences || neighbourInfluences <= highestInfluence)
				{
					continue;
				}

				highestInfluence = neighbourInfluences;
				newTargetNode = neighbour;
			}

			if (newTargetNode != null)
			{
				Data.PreviousNode = Data.TargetNode;
				Data.TargetNode = newTargetNode;
			}
		}

		public string PrioritiesToString()
		{
			var s = "Priorities\n";

			var flags = new List<Influences>((~Influences.None).GetFlags());

			for (var i = 0; i < flags.Count; i++)
			{
				var priorityName = Enum.GetName(typeof(Influences), flags[i]);
				s += priorityName + (i < flags.Count ? ", " : "");
			}

			return s;
		}

		public override string ToString()
		{
			return
				  "Influences: " + PrioritiesToString() + "\n"
				+ "Previous Node: " 
				+ Data.PreviousNode == null ? Data.PreviousNode.InfluencesToString() : "None" + "\n"
				+ "Target Node: " 
				+ Data.TargetNode == null ? Data.TargetNode.InfluencesToString() : "None";
		}

		public override int GetHashCode()
		{
			var hash = 17;

			hash += hash * 23 + Data.Acceleration.GetHashCode();
			hash += hash * 23 + Data.PreviousNode.GetHashCode();
			hash += hash * 23 + Data.Priorities.GetHashCode();
			hash += hash * 23 + Data.TargetNode.GetHashCode();
			hash += hash * 23 + Data.TopSpeed.GetHashCode();

			return hash;
		}

		public override bool Equals(object obj)
		{
			return obj != null ? GetHashCode() == ((Navigator)obj).GetHashCode() : false;
		}
	}
}
