using UnityEngine;

namespace Assets.Scripts
{
	public class NodeInfluenceTest : MonoBehaviour
	{
		public GameObject textMeshPrefab;
		private ObjectNavigationNode a;
		private ObjectNavigationNode b;
		private ObjectNavigationNode c;
		private ObjectNavigationNode d;
		private ObjectNavigationNode e;

		void Start()
		{
			a = new ObjectNavigationNode(Vector3.zero);
			b = new ObjectNavigationNode(Vector3.up);
			c = new ObjectNavigationNode(Vector3.down);
			d = new ObjectNavigationNode(Vector3.left);
			e = new ObjectNavigationNode(new Vector3(1,-1,0));

			a.AddObject(textMeshPrefab);
			b.AddObject(textMeshPrefab);
			c.AddObject(textMeshPrefab);
			d.AddObject(textMeshPrefab);
			e.AddObject(textMeshPrefab);

			NavigationNode.ConnectNeighbours(a, c);
			NavigationNode.ConnectNeighbours(a, d);
			NavigationNode.ConnectNeighbours(a, e);
			NavigationNode.ConnectNeighbours(b, e);
			NavigationNode.ConnectNeighbours(c, d);
			NavigationNode.ConnectNeighbours(c, e);
			NavigationNode.ConnectNeighbours(d, e);
			
			a.SetInfluence(Influences.Attacker, 5);
			a.IncrementInfluence(Influences.Attacker | Influences.IndimidationBell, 12);
			b.PropagateInfluences();
			Debug.Log(a.ToString());
			Debug.Log(b.ToString());
			Debug.Log(c.ToString());
			Debug.Log(d.ToString());
			Debug.Log(e.ToString());

			a.GameObjects[0].GetComponent<TextMesh>().text = a.Position.ToString() + "\n" + a.InfluencesToString();
			b.GameObjects[0].GetComponent<TextMesh>().text = b.Position.ToString() + "\n" + b.InfluencesToString();
			c.GameObjects[0].GetComponent<TextMesh>().text = c.Position.ToString() + "\n" + c.InfluencesToString();
			d.GameObjects[0].GetComponent<TextMesh>().text = d.Position.ToString() + "\n" + d.InfluencesToString();
			e.GameObjects[0].GetComponent<TextMesh>().text = e.Position.ToString() + "\n" + e.InfluencesToString();
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