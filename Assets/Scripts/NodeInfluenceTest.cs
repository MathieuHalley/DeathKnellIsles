using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class NodeInfluenceTest : MonoBehaviour
	{
		public GameObject textMeshPrefab;
		public GameObject navigatorPrefab;
		private Node n1;
		private Node n2;
		private Node n3;
		private Node n4;
		private Node n5;
		private GameObject navigatorObject;
		public NavigatorComponent[] navigators;

		void Awake()
		{
			n1 = new Node(Vector3.zero * 10);
			n2 = new Node(Vector3.up * 10);
			n3 = new Node(Vector3.down * 10);
			n4 = new Node(Vector3.left * 10);
			n5 = new Node(new Vector3(10,-10,0));

			n1.Data.GameObject = Instantiate(textMeshPrefab, n1.Data.Position, Quaternion.identity);
			n2.Data.GameObject = Instantiate(textMeshPrefab, n2.Data.Position, Quaternion.identity);
			n3.Data.GameObject = Instantiate(textMeshPrefab, n3.Data.Position, Quaternion.identity);
			n4.Data.GameObject = Instantiate(textMeshPrefab, n4.Data.Position, Quaternion.identity);
			n5.Data.GameObject = Instantiate(textMeshPrefab, n5.Data.Position, Quaternion.identity);

			navigatorObject = Instantiate(navigatorPrefab, Vector3.zero, Quaternion.identity);
			navigatorObject.GetComponent<NavigatorComponent>().Navigator.Data.TargetNode = n1;
			navigatorObject.GetComponent<NavigatorComponent>().priorities = Influences.IndimidationBell;

			NodeNeighbourManager.ConnectNeighbours(n1, n4);
			NodeNeighbourManager.ConnectNeighbours(n4, n3);
			NodeNeighbourManager.ConnectNeighbours(n5, n2, n3);
			NodeInfluenceManager.SetInfluenceValues(n1, Influences.Attacker, 5);
			NodeInfluenceManager.IncrementInfluenceValues(n4, Influences.IndimidationBell, 4);
			NodeInfluenceManager.IncrementInfluenceValues(n2, Influences.IndimidationBell, 12);
			NodeInfluenceManager.PropagateNodeInfluence(new[] { n1, n2 }, Influences.Attacker);
			NodeInfluenceManager.PropagateNodeInfluence(new[] { n1, n2 }, Influences.IndimidationBell);
			NodeInfluenceManager.ResetInfluenceValues(n2, Influences.IndimidationBell);

			n1.Data.GameObject.GetComponent<TextMesh>().text = n1.ToString();
			n2.Data.GameObject.GetComponent<TextMesh>().text = n2.ToString();
			n3.Data.GameObject.GetComponent<TextMesh>().text = n3.ToString();
			n4.Data.GameObject.GetComponent<TextMesh>().text = n4.ToString();
			n5.Data.GameObject.GetComponent<TextMesh>().text = n5.ToString();

		}

		void Update()
		{
			var nodes = new[] { n1, n2, n3, n4, n5 };

			foreach (var node in nodes)
			{
				foreach (var neighbour in node.Data.Neighbours)
				{
					Debug.DrawLine(node.Data.Position, neighbour.Data.Position, Color.red);
				}
			}
		}
	}
}