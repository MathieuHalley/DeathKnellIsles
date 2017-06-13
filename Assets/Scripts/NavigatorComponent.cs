using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{

	public class NavigatorComponent : MonoBehaviour
	{
		public Navigator Navigator;
		public float Accleration = 10f;
		public float TopSpeed = 10f;
		public Influences priorities;
		
		private new Rigidbody rigidbody;
		private Rigidbody Rigidbody
		{
			get
			{
				if (rigidbody == null)
				{
					rigidbody = GetComponent<Rigidbody>();
				}
				return rigidbody;
			}
			set { rigidbody = value; }
		}

		private Vector3 TargetNodeOffset
		{
			get { return Navigator.Data.TargetNode.Data.Position - transform.position; }
		}

		private void Awake()
		{
			Debug.Assert(GetComponent<Rigidbody>() != null, "NavigatorObject doesn't contain a Rigidbody");

			Navigator = new Navigator();
			Navigator.Data.GameObject = gameObject;

			if (Navigator.Data.TopSpeed != TopSpeed)
			{
				Navigator.Data.TopSpeed = TopSpeed;
			}

			if (Navigator.Data.Priorities != priorities)
			{
				Navigator.Data.Priorities = priorities;
			}
		}

		private void FixedUpdate()
		{
			if (Navigator.Data.TargetNode == null || TargetNodeOffset.magnitude < Navigator.Data.TargetNode.Data.Radius)
			{
				Navigator.Data.Events.OnTargetNodeReached(gameObject, Navigator.Data);
				Navigator.IdentifyNextTargetNode();

				return;
			}

			MoveTowardsTargetNode();
		}

		public void MoveTowardsTargetNode()
		{
			var acceleration = TargetNodeOffset * Time.fixedDeltaTime * Navigator.Data.Acceleration;

			Rigidbody.AddForce(acceleration, ForceMode.Acceleration);
			Rigidbody.velocity = Vector3.ClampMagnitude(Rigidbody.velocity, Navigator.Data.TopSpeed);
		}
	}
}