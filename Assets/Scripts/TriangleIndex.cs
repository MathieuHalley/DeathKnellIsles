using UnityEngine;

namespace Assets.Scripts
{
	public class TriangleIndex
	{
		public CornerIndex[] CornerNeighbours { get; private set; }

		public TriangleIndex(CornerIndex a, CornerIndex b, CornerIndex c)
		{
			CornerNeighbours = new[] {a, b, c};
		}
	}
}
