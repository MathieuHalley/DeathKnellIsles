using System.Collections;
using System.Collections.Generic;
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
		// Use this for initialization
		void Start()
		{
			a = new NavigationNode(Vector3.zero);
			b = new NavigationNode(Vector3.up);
			c = new NavigationNode(Vector3.down);
			d = new NavigationNode(Vector3.left);
			e = new NavigationNode(Vector3.right);

			a.AddNeighbour(b);
			a.AddNeighbour(c);
			a.AddNeighbour(d);
			a.AddNeighbour(e);
			b.AddNeighbour(c);
			b.AddNeighbour(d);
			b.AddNeighbour(e);
			c.AddNeighbour(d);
			c.AddNeighbour(e);
			d.AddNeighbour(e);

			a.SetInfluence(Influences.Attacker, 5);
			Debug.Log("Influences.Attacker: " + a.GetInfluence(Influences.Attacker));
			a.IncrementInfluence(Influences.Attacker| Influences.Boat, 4);
			Debug.Log("Influences.Boat: " + a.GetInfluence(Influences.Boat));
			Debug.Log("Influences.Attacker | Influences.Boat: " + a.GetInfluence(Influences.Attacker));
		}

		// Update is called once per frame
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