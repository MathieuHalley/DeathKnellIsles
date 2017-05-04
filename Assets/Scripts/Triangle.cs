using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts
{
	public struct TriangleDimensions
	{
		public float Center { get; private set; }
		public float EdgeLength { get; private set; }
		public float Height { get; private set; }

		public TriangleDimensions(float edgeLength) : this()
		{
			EdgeLength = edgeLength;
			Height = Constants.HalfSqrtThree * edgeLength;
			Center = Height / 3f;
		}
	}

	public class Triangle
	{
		public TriangleIndex Index;
		public GameObject Object;
		public Corner[] NeighbouringCorners;
		public Triangle[] NeighbouringTriangles;
		public Vector2 Position;

		public Triangle(Corner[] neighbouringCorners)
		{
			Index = new TriangleIndex(
				neighbouringCorners[0].Index, 
				neighbouringCorners[1].Index, 
				neighbouringCorners[2].Index);
			NeighbouringTriangles = new Triangle[3];
			Position = SetPosition();
		}

		public void DebugDrawTriangle()
		{
			Debug.DrawLine(NeighbouringCorners[0].Position, NeighbouringCorners[1].Position);
			Debug.DrawLine(NeighbouringCorners[1].Position, NeighbouringCorners[2].Position);
			Debug.DrawLine(NeighbouringCorners[2].Position, NeighbouringCorners[0].Position);
		}

		private Vector2 SetPosition()
		{
			var position =
				NeighbouringCorners[0].Position + 
				NeighbouringCorners[0].Position + 
				NeighbouringCorners[0].Position; 
			position /= 3f;
			return position;
		}
	}
}
