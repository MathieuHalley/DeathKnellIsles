using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class NavigatorData
	{
		private static uint NavigatorCount;

		[SerializeField]
		private float acceleration;
		[SerializeField]
		private GameObject gameObject;
		[SerializeField]
		private readonly string id;
		[SerializeField]
		private readonly Navigator navigator;
		[SerializeField]
		private Node previousNode;
		[SerializeField]
		private Influences priorities;
		[SerializeField]
		private Node targetNode;
		[SerializeField]
		private float topSpeed;

		public readonly NavigatorDataEvents Events;

		public float Acceleration
		{
			get { return acceleration; }
			set
			{
				if (value != acceleration)
				{
					acceleration = value;
					Events.OnAccelerationUpdated(this, this);
				}
			}
		}

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

		public string ID { get { return id; } }

		public Navigator Navigator { get { return navigator; } }

		public Node PreviousNode
		{
			get { return previousNode; }
			set
			{
				if (value != previousNode)
				{
					previousNode = value;
					Events.OnPreviousNodeUpdated(this, this);
				}
			}
		}

		public Influences Priorities
		{
			get { return priorities; }
			set
			{
				if (value != priorities)
				{
					priorities = value;
					Events.OnPrioritiesUpdated(this, this);
				}
			}
		}

		public Node TargetNode
		{
			get { return targetNode; }
			set
			{
				if (value != targetNode)
				{
					targetNode = value;
					Events.OnTargetNodeUpdated(this, this);
				}
			}
		}

		public float TopSpeed
		{
			get { return topSpeed; }
			set
			{
				if (value != topSpeed)
				{
					topSpeed = value;
					Events.OnTopSpeedUpdated(this, this);
				}
			}
		}

		public NavigatorData(Navigator navigator)
		{
			NavigatorCount++;
			Events = new NavigatorDataEvents();
			Acceleration = 100f;
			GameObject = null;
			this.navigator = navigator;
			PreviousNode = null;
			Priorities = Influences.None;
			TargetNode = null;
			TopSpeed = 1f;
		}
	}
}