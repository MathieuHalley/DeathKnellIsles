using System.Collections;
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
		private readonly TriangleMap _map;

		public static readonly int[][] CornerNeighbourDirections =
		{
			new [] {-1,1,0}, new [] {-1,0,1}, new [] {0,-1,1},
			new [] {1,-1,0}, new [] {1,0,-1}, new [] {0,1,-1}
		};

		public Corner(CornerIndex index, TriangleMapLayout layout)
		{
			Index = index;
			Position = SetPosition(layout);
		}

		private Vector2 SetPosition(TriangleMapLayout layout)
		{
			var triangleDimensions = layout.TriangleDimensions;
			var edgeQScaled = Index.Q * triangleDimensions.EdgeLength;
			var edgeRScaled = Index.R * triangleDimensions.EdgeLength;
			var edgeSScaled = Index.S * triangleDimensions.Height;
			var edgeQRSquared = Mathf.Pow(edgeQScaled, 2) + Mathf.Pow(edgeRScaled, 2);
			edgeQRSquared -= 2 * edgeQScaled * edgeRScaled * Constants.Cos120Degrees;
			var unknownEdge = Mathf.Sqrt(edgeQRSquared + Mathf.Pow(edgeSScaled, 2));

			var position = layout.Orientation == MapOrientation.FlatTop 
				? new Vector2(unknownEdge, edgeSScaled) 
				: new Vector2(edgeSScaled, unknownEdge);

			return position + layout.MapCenter;
		}

		public void AssignNeighbouringCorners()
		{
			NeighbouringCorners = new CornerIndex[6];
			for (var i = 0; i < 6; ++i)
			{
				NeighbouringCorners[i] = new Corner(new CornerIndex( new[] {
					CornerNeighbourDirections[i][0] * Index.Q,
					CornerNeighbourDirections[i][1] * Index.R,
					CornerNeighbourDirections[i][2] * Index.S
				}), _map);
			}
		}

		public void AssignNeighbouringTriangles()
		{
			NeighbouringTriangles = new[]
			{
				new TriangleIndex(Index, NeighbouringCorners[0], NeighbouringCorners[1]),
				new TriangleIndex(Index, NeighbouringCorners[1], NeighbouringCorners[2]),
				new TriangleIndex(Index, NeighbouringCorners[2], NeighbouringCorners[3]),
				new TriangleIndex(Index, NeighbouringCorners[3], NeighbouringCorners[4]),
				new TriangleIndex(Index, NeighbouringCorners[4], NeighbouringCorners[5]),
				new TriangleIndex(Index, NeighbouringCorners[5], NeighbouringCorners[0])
			};
		}
	}
}
