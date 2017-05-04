using UnityEngine;

namespace Assets.Scripts
{
	public class Corner
	{
		public CornerIndex Index { get; private set; }
		public Vector2 Position { get; private set; }

		public Corner[] NeighbouringCorners { get; private set; }
		public Triangle[] NeighbouringTriangles { get; private set; }

		public static readonly int[][] CornerNeighbourDirections =
		{
			new [] {-1,1,0}, new [] {-1,0,1}, new [] {0,-1,1},
			new [] {1,-1,0}, new [] {1,0,-1}, new [] {0,1,-1}
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

		public void AssignNeighbouringCorners(CornerIndex index, TriangleMapLayout layout)
		{
			NeighbouringCorners = new Corner[6];
			for (var i = 0; i < 6; ++i)
			{
				NeighbouringCorners[i] = new Corner(new CornerIndex( new[] {
					CornerNeighbourDirections[i][0] * Index.Q,
					CornerNeighbourDirections[i][1] * Index.R,
					CornerNeighbourDirections[i][2] * Index.S
				}), layout);
			}
		}

		public void AssignNeighbouringTriangles()
		{
			NeighbouringTriangles = new[]
			{
				new Triangle(new []{this, NeighbouringCorners[0], NeighbouringCorners[1]}),
				new Triangle(new []{this, NeighbouringCorners[1], NeighbouringCorners[2]}),
				new Triangle(new []{this, NeighbouringCorners[2], NeighbouringCorners[3]}),
				new Triangle(new []{this, NeighbouringCorners[3], NeighbouringCorners[4]}),
				new Triangle(new []{this, NeighbouringCorners[4], NeighbouringCorners[5]}),
				new Triangle(new []{this, NeighbouringCorners[5], NeighbouringCorners[0]})
			};
		}

		public static int[][] GetNeighbouringCorners(int[] index, TriangleMapLayout layout)
		{
			var neighbouringCorners = new int[6][];
			for (var i = 0; i < 6; ++i)
			{
				neighbouringCorners[i] = new[] 
				{
					CornerNeighbourDirections[i][0] * index[0],
					CornerNeighbourDirections[i][1] * index[1],
					CornerNeighbourDirections[i][2] * index[2]
				};
			}
			return neighbouringCorners;
		}
	}
}
