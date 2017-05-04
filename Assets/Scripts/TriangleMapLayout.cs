using UnityEngine;

namespace Assets.Scripts
{
	public enum MapOrientation
	{
		FlatTop,
		PointyTop
	}

	public struct TriangleMapLayout
	{
		public readonly Vector2 MapCenter;
		public readonly TriangleDimensions TriangleDimensions;
		public readonly MapOrientation Orientation;
		public readonly float RingCount;

		public TriangleMapLayout(
			Vector2 mapCenter, 
			TriangleDimensions triangleDimensions, 
			MapOrientation orientation, 
			float ringCount)
		{
			MapCenter = mapCenter;
			TriangleDimensions = triangleDimensions;
			Orientation = orientation;
			RingCount = ringCount;
		}
	}
}