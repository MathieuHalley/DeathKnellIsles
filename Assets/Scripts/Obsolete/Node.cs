//using System;
//using System.Text;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//namespace Assets.Scripts
//{
//	public enum Influences
//	{
//		None = 0,                   //	0
//		Boat = 1 << 0,              //	1
//		Tower = 1 << 1,             //	2
//		House = 1 << 2,             //	4
//		Attacker = 1 << 3,          //	8
//		Defender = 1 << 4,          //	16
//		Villager = 1 << 5,          //	32
//		HideBell = 1 << 6,          //	64
//		PowerBell = 1 << 7,         //	128
//		RallyBell = 1 << 8,         //	256
//		SleepBell = 1 << 9,         //	512
//		RetreatBell = 1 << 10,      //	1024
//		RaiseBell = 1 << 11,        //	2048
//		IndimidationBell = 1 << 12, //	4196
//	}

//	public class Node
//	{
//		private readonly NodeController controller;
//		private static uint count;
   
//        [SerializeField] private Dictionary<Influences, int> activeInfluenceValues;
//		[SerializeField] private bool isPassable;
//		[SerializeField] private GameObject gameObject;
//		[SerializeField] private ICollection<Node> neighbours;
//		[SerializeField] private float radius;

//        public readonly string ID;

//        public Dictionary<Influences, int> ActiveInfluenceValues
//		{
//			get
//            {
//                if (activeInfluenceValues == null)
//                {
//                    activeInfluenceValues = new Dictionary<Influences, int>();
//                }

//                return activeInfluenceValues;
//            }

//			set
//			{
//				if (value != activeInfluenceValues)
//                {
//                    activeInfluenceValues = value;
//                }
//            }
//		}

//		public bool IsPassable
//		{
//			get
//            {
//                return isPassable;
//            }

//			set
//			{
//				if (value != isPassable)
//                {
//                    isPassable = value;
//                }
//            }
//		}

//		public ICollection<Node> Neighbours
//		{
//			get
//            {
//                return neighbours;
//            }

//			set
//			{
//				if (value != neighbours)
//                {
//                    neighbours = value;
//                }
//            }
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

//		public float Radius
//		{
//			get
//            {
//                return Mathf.Abs(radius);
//            }

//			set
//			{
//				if (value != radius)
//                {
//                    radius = value;
//                }
//            }
//		}

//        public Node()
//		{
//			controller = new NodeController();
//			gameObject = null;
//			ID = "Node: " + (count++).ToString();
//			activeInfluenceValues = new Dictionary<Influences, int>();
//			isPassable = true;
//			neighbours = new List<Node>();
//			radius = 0.5f;
//		}

//		public Node(GameObject gameObject, bool isPassable = true) : this()
//		{
//            GameObject = gameObject;
//			IsPassable = isPassable;
//		}

//		public string NodeInfluencesToString()
//		{
//            var influencesToStringBuilder = new StringBuilder().AppendLine("Influences");
            
//            foreach (var influence in GetActiveInfluences())
//            {
//                influencesToStringBuilder
//                    .Append("\t")
//                    .Append(Enum.GetName(typeof(Influences), influence))
//                    .Append(": ")
//                    .AppendLine(ActiveInfluenceValues[influence].ToString());
//            }

//			return influencesToStringBuilder.ToString();
//		}

//		public string NodeIsPassableToString()
//		{
//            return new StringBuilder("IsPassable: ")
//                .Append(isPassable)
//                .ToString();
//		}

//		public string NodeNeighboursToString()
//		{
//            var neighboursToStringBuilder = new StringBuilder("Neighbours").AppendLine();

//			foreach (var neighbour in Neighbours)
//            {
//                neighboursToStringBuilder
//                    .Append("\t")
//                    .AppendLine(neighbour.ID);
//            }

//			return neighboursToStringBuilder.ToString();
//		}

//		public string NodeGameObjectToString()
//		{
//            return new StringBuilder()
//                .AppendLine("Game Object: ")
//                .Append(GameObject == null ? "null" : GameObject.ToString())
//                .ToString();
//		}

//		public override string ToString()
//		{
//            return new StringBuilder()
//                .AppendLine(ID)
//                .AppendLine(NodeIsPassableToString())
//                .Append(NodeInfluencesToString())
//                .ToString();
//		}

//		public override int GetHashCode()
//		{
//			var hash = 17;

//			hash += hash * 23 + base.GetHashCode();
//			hash += hash * 23 + gameObject.GetHashCode();
//			hash += hash * 23 + neighbours.GetHashCode();

//			return hash;
//		}

//		public override bool Equals(object obj)
//		{
//			return obj != null ? GetHashCode() == ((Node)obj).GetHashCode() : false;
//		}

//        private IEnumerable<Influences> GetActiveInfluences()
//        {
//            return from influence in (~Influences.None).GetFlags()
//                   where ActiveInfluenceValues.ContainsKey(influence)
//                   select influence;
//        }
//    }
//}
