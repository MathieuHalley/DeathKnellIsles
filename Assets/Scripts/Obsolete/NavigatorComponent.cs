//using UnityEngine;
//using System.Linq;
//using System.Collections.Generic;

//namespace Assets.Scripts
//{
//    public class NavigatorComponent : MonoBehaviour
//	{
//        public Navigator Navigator;
//		public float Accleration = 10f;
//		public float TopSpeed = 10f;
//		public List<Influences> ActiveInfluences;

//		private Vector3 GetTargetNodeOffset
//		{
//			get
//            {
//                return Navigator.TargetNode.GameObject.transform.position - transform.position;
//            }
//		}

//		private void Awake()
//		{
//			Debug.Assert(GetComponent<Rigidbody>() != null, "NavigatorObject doesn't contain a Rigidbody");

//            Navigator = new Navigator()
//            {
//                GameObject = gameObject
//            };

//            if (Navigator.TopSpeed != TopSpeed)
//            {
//                Navigator.TopSpeed = TopSpeed;
//            }

//            if (Navigator.ActiveInfluences != ActiveInfluences)
//            {
//                Navigator.ActiveInfluences = ActiveInfluences;
//            }
//		}

//		private void FixedUpdate()
//		{
//			if (Navigator.TargetNode == null || GetTargetNodeOffset.magnitude < Navigator.TargetNode.Radius)
//			{
//				Navigator.IdentifyNextTargetNode(ActiveInfluences);

//				return;
//			}

//			MoveTowardsTargetNode();
//		}

//		public void MoveTowardsTargetNode()
//		{
//            var rigidbody = GetComponent<Rigidbody>();
//            Debug.Assert(rigidbody != null, "This navigator doesn't have a rigidbody.");

//			var acceleration = GetTargetNodeOffset * Time.fixedDeltaTime * Navigator.Acceleration;

//			rigidbody.AddForce(acceleration, ForceMode.Acceleration);
//			rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, Navigator.TopSpeed);
//		}

//        public void SetTargetNode(GameObject nodeObject)
//        {
//            Navigator.TargetNode = nodeObject;
//        }
//	}
//}