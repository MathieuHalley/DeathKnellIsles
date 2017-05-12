using UnityEngine;

namespace Assets.Scripts
{
	public class NodeInfluenceTest : MonoBehaviour
	{
		public GameObject textMeshPrefab;
		private NavigationNode a;
		private NavigationNode b;
		private NavigationNode c;
		private NavigationNode d;
		private NavigationNode e;

		void Start()
		{
			a = new NavigationNode(Vector3.zero);
			b = new NavigationNode(Vector3.up);
			c = new NavigationNode(Vector3.down);
			d = new NavigationNode(Vector3.left);
			e = new NavigationNode(new Vector3(1,-1,0), false);

			a.NodeObjects.AddObject(textMeshPrefab, a.Position, Quaternion.identity, Vector3.one);
			b.NodeObjects.AddObject(textMeshPrefab, b.Position, Quaternion.identity, Vector3.one);
			c.NodeObjects.AddObject(textMeshPrefab, c.Position, Quaternion.identity, Vector3.one);
			d.NodeObjects.AddObject(textMeshPrefab, d.Position, Quaternion.identity, Vector3.one);
			e.NodeObjects.AddObject(textMeshPrefab, e.Position, Quaternion.identity, Vector3.one);

			NavigationNode.ConnectNeighbours(a, c);
			NavigationNode.ConnectNeighbours(a, d);
			NavigationNode.ConnectNeighbours(a, e);
			NavigationNode.ConnectNeighbours(b, e);
			NavigationNode.ConnectNeighbours(c, d);
			NavigationNode.ConnectNeighbours(c, e);
			NavigationNode.ConnectNeighbours(d, e);
			
			a.NodeInfluences.SetInfluenceValues(Influences.Attacker, 5);
			a.NodeInfluences.IncrementInfluenceValues(Influences.Attacker | Influences.IndimidationBell, 12);
			InfluenceController.PropagateInfluences(Influences.Attacker | Influences.IndimidationBell, a);
			Debug.Log(a.ToString());
			Debug.Log(b.ToString());
			Debug.Log(c.ToString());
			Debug.Log(d.ToString());
			Debug.Log(e.ToString());

			a.NodeObjects[0].GetComponent<TextMesh>().text = a.Position.ToString() + "\n" + a.NodeInfluences.ToString();
			b.NodeObjects[0].GetComponent<TextMesh>().text = b.Position.ToString() + "\n" + b.NodeInfluences.ToString();
			c.NodeObjects[0].GetComponent<TextMesh>().text = c.Position.ToString() + "\n" + c.NodeInfluences.ToString();
			d.NodeObjects[0].GetComponent<TextMesh>().text = d.Position.ToString() + "\n" + d.NodeInfluences.ToString();
			e.NodeObjects[0].GetComponent<TextMesh>().text = e.Position.ToString() + "\n" + e.NodeInfluences.ToString();
		}

		void Update()
		{
			var nodes = new[] { a, b, c, d, e };
			foreach (var node in nodes)
			{
				foreach (var neighbour in node.Neighbours)
				{
					Debug.DrawLine(node.Position, neighbour.Position);
				}
			}
		}
	}
}