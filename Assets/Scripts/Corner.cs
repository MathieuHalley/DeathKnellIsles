using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class Corner
	{
		public CornerIndex Index { get; private set; }
		public Vector2 Position { get; private set; }

		public Corner[] NeighbouringCorners { get; private set; }
		public Triangle[] NeighbouringTriangles { get; private set; }

		public static readonly CornerIndex[] CornerNeighbourDirections =
		{
			new CornerIndex(-1,1,0), new CornerIndex(-1,0,1), new CornerIndex(0,-1,1),
			new CornerIndex(1,-1,0), new CornerIndex(1,0,-1), new CornerIndex(0,1,-1)
		};

		public Corner(CornerIndex index, TriangleMapLayout layout)
		{
			Index = index;
			Position = CornerIndexToPosition(index, layout);
		}

		public static Vector2 CornerIndexToPosition(CornerIndex index, TriangleMapLayout layout)
		{
			var x = (index.R - index.Q) * 0.5f * layout.TriangleDimensions.EdgeLength;
			var y = index.S * layout.TriangleDimensions.Height;
			var position = layout.Orientation == MapOrientation.FlatTop ? new Vector2(x, y) : new Vector2(y, x);
			position += layout.MapCenter;

			return position;
		}

		public static CornerIndex PositionToCornerIndex(Vector2 position, TriangleMapLayout layout)
		{
			var flatOrientation = layout.Orientation == MapOrientation.FlatTop;
			var x = flatOrientation ? position.x : position.y;
			var y = flatOrientation ? position.y : position.x;
			var s = Mathf.CeilToInt(y / layout.TriangleDimensions.Height);

			var rqDelta = (int)((x / layout.TriangleDimensions.EdgeLength) * 2);
			var q = (int)(-(rqDelta + s) * 0.5f);
			var r = q + rqDelta;

			return new CornerIndex(q, r, s);
		}

		public void AssignNeighbouringCorners(TriangleMapLayout layout)
		{
			var neighbouringCorners = new List<Corner>();
			foreach (var neighbourDirection in CornerNeighbourDirections)
			{
				var newIndex = neighbourDirection + Index;
				var newNeighbour = new Corner(newIndex, layout);

				neighbouringCorners.Add(newNeighbour);
			}
			NeighbouringCorners = neighbouringCorners.ToArray();
		}

		public void AssignNeighbouringTriangles()
		{
			NeighbouringTriangles = new[]
			{
				new Triangle(new [] {this, NeighbouringCorners[0], NeighbouringCorners[1]}),
				new Triangle(new [] {this, NeighbouringCorners[1], NeighbouringCorners[2]}),
				new Triangle(new [] {this, NeighbouringCorners[2], NeighbouringCorners[3]}),
				new Triangle(new [] {this, NeighbouringCorners[3], NeighbouringCorners[4]}),
				new Triangle(new [] {this, NeighbouringCorners[4], NeighbouringCorners[5]}),
				new Triangle(new [] {this, NeighbouringCorners[5], NeighbouringCorners[0]})
			};
		}

		public CornerIndex[] GetNeighbouringCorners(TriangleMapLayout layout)
		{
			var neighbouringCorners = new CornerIndex[6];
			for (var i = 0; i < 6; ++i)
			{
				neighbouringCorners[i] = new CornerIndex(
					CornerNeighbourDirections[i].Q + Index.Q,
					CornerNeighbourDirections[i].R + Index.R,
					CornerNeighbourDirections[i].S + Index.S
				);
			}
			return neighbouringCorners;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType()) return false;

			return Index == ((Corner)obj).Index;
		}

		public override int GetHashCode()
		{
			return Index.GetHashCode();
		}

		public override string ToString()
		{
			return "(Q:" + Index.Q + ", R:" + Index.R + ", S:" + Index.S + ")";
		}
	}
}
