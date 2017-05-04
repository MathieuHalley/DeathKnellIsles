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
		public int[] MapIndex;
		public GameObject Object;
		public Corner[] Corners;
		public Triangle[] Neighbours;
		public Vector2 Position;
		private readonly TriangleMap _map;

		public Triangle(TriangleMap map, int[] mapIndex, GameObject triangleObject)
		{
			MapIndex = mapIndex;
			Object = triangleObject;
			Neighbours = new Triangle[3];
			Position = SetPosition();
			_map = map;
		}

		public void DebugDrawTriangle()
		{
			Debug.DrawLine(Corners[0].Position, Corners[1].Position);
			Debug.DrawLine(Corners[1].Position, Corners[2].Position);
			Debug.DrawLine(Corners[2].Position, Corners[0].Position);
		}

		private Vector2 SetPosition(TriangleMap _map)
		{
			var origin = _map.Layout.MapCenter;
			var mapScale = _map.Layout.

		}
	}
}
