using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public sealed class NodeData
	{
		private static uint NodeCount;

		[SerializeField]
		private readonly string id;
		[SerializeField]
		private Dictionary<Influences, int> influences;
		[SerializeField]
		private bool isPassable;
		[SerializeField]
		private GameObject gameObject;
		[SerializeField]
		private readonly Node node;
		[SerializeField]
		private IList<Node> neighbours;
		[SerializeField]
		private Vector3 position;
		[SerializeField]
		private float radius;

		public readonly NodeDataEvents Events;

		public string ID { get { return id; } }

		public Dictionary<Influences, int> Influences
		{
			get { return influences; }
			set
			{
				if (value != influences)
				{
					influences = value;
					Events.OnInfluencesUpdated(this, this);
				}
			}
		}

		public bool IsPassable
		{
			get { return isPassable; }
			set
			{
				if (value != isPassable)
				{
					isPassable = value;
					Events.OnIsPassableUpdated(this, this);
				}
			}
		}

		public IList<Node> Neighbours
		{
			get { return neighbours; }
			set
			{
				if (value != neighbours)
				{
					neighbours = value;
					Events.OnNeighboursUpdated(this, this);
				}
			}
		}

		public Node Node { get { return node; } }

		public GameObject GameObject
		{
			get { return gameObject; }
			set
			{
				if (value != gameObject)
				{
					gameObject = value;
					Events.OnGameObjectUpdated(this, this);
				}
			}
		}

		public Vector3 Position
		{
			get { return position; }
			set
			{
				if (value != position)
				{
					position = value;
					Events.OnPositionUpdated(this, this);
				}
			}
		}

		public float Radius
		{
			get { return Mathf.Abs(radius); }
			set
			{
				if (value != radius)
				{
					radius = value;
					Events.OnRadiusUpdated(this, this);
				}
			}
		}

		public NodeData(Node node)
		{
			NodeCount++;
			Events = new NodeDataEvents();
			id = "Node: " + NodeCount;
			influences = new Dictionary<Influences, int>();
			gameObject = null;
			isPassable = true;
			neighbours = new List<Node>();
			this.node = node;
			position = new Vector3();
			radius = 0.5f;
		}
	}
}
