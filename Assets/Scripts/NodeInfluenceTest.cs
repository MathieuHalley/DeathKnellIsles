using UnityEngine;

namespace Assets.Scripts
{
	public class NodeInfluenceTest : MonoBehaviour
	{
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
			e = new NavigationNode(Vector3.right);

			NavigationNode.ConnectNeighbours(a, b);
			NavigationNode.ConnectNeighbours(a, c);
			NavigationNode.ConnectNeighbours(a, d);
			NavigationNode.ConnectNeighbours(a, e);
			NavigationNode.ConnectNeighbours(b, c);
			NavigationNode.ConnectNeighbours(b, d);
			NavigationNode.ConnectNeighbours(b, e);
			NavigationNode.ConnectNeighbours(c, d);
			NavigationNode.ConnectNeighbours(c, e);
			NavigationNode.ConnectNeighbours(d, e);
			
			a.SetInfluence(Influences.Attacker, 5);
			a.IncrementInfluence(Influences.Attacker | Influences.IndimidationBell, 12);
			a.IncrementInfluence(Influences.Boat | Influences.IndimidationBell, -7);
			Debug.Log(a.ToString());
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