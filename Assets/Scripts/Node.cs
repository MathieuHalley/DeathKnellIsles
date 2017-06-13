using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class Node
	{
		public readonly NodeController Controller;
		public readonly NodeData Data;

		public Node()
		{
			Data = new NodeData(this);
			Controller = new NodeController(this);
		}

		public Node(Vector3 position, bool isPassable = true) : this()
		{
			Data.Position = position;
			Data.IsPassable = isPassable;
		}

		/// <summary>Generates a string that represents this node's list of Influences.</summary>
		/// <returns>A string that represents this node's list of Influences.</returns>
		public string InfluencesToString()
		{
			var s = "Influences\n";

			var flags = new List<Influences>((~Influences.None).GetFlags());

			for (var i = 0; i < flags.Count; i++)
			{
				if (Data.Influences.ContainsKey(flags[i]))
				{
					var influenceName = Enum.GetName(typeof(Influences), flags[i]);
					var influenceValue = Data.Influences[flags[i]].ToString();
					s += influenceName + " - " + influenceValue + (i < flags.Count ? "\n " : "");
				}
			}

			return s;
		}

		/// <summary>Generates a string that represents this node's IsPassable state.</summary>
		/// <returns>A string that represents this node's IsPassable state.</returns>
		public string IsPassableToString()
		{
			return "IsPassable: " + Data.IsPassable.ToString();
		}

		/// <summary>Generates a string that represents the list of this node's Neighbours.</summary>
		/// <returns>A string that represents the list of this node's Neighbours.</returns>
		public string NeighboursToString()
		{
			var s = "Neighbours\n";

			foreach (var neighbour in Data.Neighbours)
			{
				s += neighbour.Data.Position + "\n";
			}

			return s;
		}

		/// <summary>Generates a string that represents this node's GameObject.</summary>
		/// <returns>A string that represents this node's GameObject.</returns>
		public string GameObjectToString()
		{
			return "Game Object\n" + (Data.GameObject == null ? "null" : Data.GameObject.ToString());
		}

		/// <summary>Generates a string that represents the state of IsPassable in this NodeData.</summary>
		/// <returns>A string that represents the state of IsPassable in this NodeData.</returns>
		public string PositionToString()
		{
			return "Position: " + Data.Position.ToString();
		}
		
		/// <summary>Generates a string that represents the contents of this Node.</summary>
		/// <returns>A string that represents the contents of this Node.</returns>
		public override string ToString()
		{
			return Data.ID + "\n" + IsPassableToString() + "\n" + InfluencesToString();
		}

		/// <summary>Generates & returns a hashcode that represents this Node.</summary>
		/// <returns>A hashcode that represents this Node.</returns>
		public override int GetHashCode()
		{
			var hash = 17;

			hash += hash * 23 + base.GetHashCode();
			hash += hash * 23 + Data.GetHashCode();

			return hash;
		}

		/// <summary>Compares this Node to an unknown object.</summary>
		/// <param name="obj">The object that this Node is being compared to.</param>
		/// <returns>The return represents whether obj is equivilent to this Node.</returns>
		public override bool Equals(object obj)
		{
			return obj != null ? GetHashCode() == ((Node)obj).GetHashCode() : false;
		}
	}
}
