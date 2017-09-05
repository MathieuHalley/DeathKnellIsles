//using UnityEngine;

//namespace Assets.Scripts
//{
//    public class NodeInfluenceTest : MonoBehaviour
//	{
//		public GameObject nodePrefab;
//		public GameObject navigatorPrefab;
//		private GameObject[] nodeObjects;
//		private GameObject navigatorObject;
//		public NavigatorComponent[] navigators;

//		void Awake()
//		{
//            nodeObjects = new GameObject[5];
//            nodeObjects[0] = Instantiate(nodePrefab, Vector3.zero * 10, Quaternion.identity);
//            nodeObjects[1] = Instantiate(nodePrefab, Vector3.up * 10, Quaternion.identity);
//            nodeObjects[2] = Instantiate(nodePrefab, Vector3.down * 10, Quaternion.identity);
//            nodeObjects[3] = Instantiate(nodePrefab, Vector3.left * 10, Quaternion.identity);
//            nodeObjects[4] = Instantiate(nodePrefab, new Vector3(10, -10, 0), Quaternion.identity);

//			navigatorObject = Instantiate(navigatorPrefab, Vector3.zero, Quaternion.identity);
//			navigatorObject.GetComponent<NavigatorComponent>()
//                .Navigator
//                .TargetNode 
//                = nodes[0];
//			navigatorObject
//                .GetComponent<NavigatorComponent>()
//                .ActiveInfluences.Add(Influences.IndimidationBell);

//			NodeNeighbourManager.ConnectNeighbours(nodes[0], nodes[3]);
//			NodeNeighbourManager.ConnectNeighbours(nodes[3], nodes[2]);
//			NodeNeighbourManager.ConnectNeighbours(nodes[4], nodes[1], nodes[2]);

//			NodeInfluenceManager.SetInfluenceValues(nodes[0].ActiveInfluenceValues, new[] { Influences.Attacker }, 5);
//            nodes[3].ActiveInfluenceValues = NodeInfluenceManager.IncrementInfluenceValues(nodes[3].ActiveInfluenceValues, new[] { Influences.IndimidationBell }, 4);
//            nodes[1].ActiveInfluenceValues = NodeInfluenceManager.IncrementInfluenceValues(nodes[1].ActiveInfluenceValues, new[] { Influences.IndimidationBell }, 12);

//			NodeInfluenceManager.PropagateNodeInfluence(new[] { nodes[0], nodes[1] }, new[] { Influences.Attacker });
//			NodeInfluenceManager.PropagateNodeInfluence(new[] { nodes[0], nodes[1] }, new[] { Influences.IndimidationBell });

//            nodes[1].ActiveInfluenceValues = NodeInfluenceManager.ResetInfluenceValues(nodes[1].ActiveInfluenceValues, new[] { Influences.IndimidationBell });

//			nodes[0].GameObject.GetComponent<TextMesh>().text = nodes[0].ToString();
//			nodes[1].GameObject.GetComponent<TextMesh>().text = nodes[1].ToString();
//			nodes[2].GameObject.GetComponent<TextMesh>().text = nodes[2].ToString();
//			nodes[3].GameObject.GetComponent<TextMesh>().text = nodes[3].ToString();
//			nodes[4].GameObject.GetComponent<TextMesh>().text = nodes[4].ToString();
//		}

//		void Update()
//		{
//			foreach (var node in nodes)
//			{
//				foreach (var neighbour in node.Neighbours)
//                {
//                    Debug.DrawLine(node.Position, neighbour.Position, Color.red);
//                }
//			}
//		}
//	}
//}