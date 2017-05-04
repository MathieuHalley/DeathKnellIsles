using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class TriangleMap : MonoBehaviour
	{
		[SerializeField]
		private GameObject _triPrefab;
		[SerializeField]
		private int mapRadius = 4;
		public float MapScale = 1f;
		public Dictionary<CornerIndex, Corner> Corners { get; private set; }
		public Dictionary<TriangleIndex, Triangle> Triangles { get; private set; }
		public TriangleMapLayout Layout { get; private set; }

		private void Awake()
		{
			Layout = new TriangleMapLayout(Vector2.zero, new TriangleDimensions(1f), MapOrientation.FlatTop, 1);
			Corners = new Dictionary<CornerIndex, Corner>();
			Triangles = new Dictionary<TriangleIndex, Triangle>();
			var initialAngle = Mathf.Deg2Rad * (Layout.Orientation == MapOrientation.FlatTop ? 30f : 0f);
			for (var q = -mapRadius; q <= mapRadius; ++q)
			{
				var r1 = Mathf.Max(-mapRadius, -q - mapRadius);
				var r2 = Mathf.Min( mapRadius, -q + mapRadius);

				for (var r = r1; r <= r2; ++r)
				{
					for (var i = 0; i < 6; ++i)
					{
						//var corner = CreateCorner(new[] { q, r });
						//var angle = Constants.TwoPi * (0.5f + Layout.Orientation + i) / 6f;
						//var offset = new Vector2(Mathf.Cos(angle) * triCenter.x, Mathf.Sin(angle) * triCenter.y);
						//var position = new Vector3(hexPosition.x + offset.x, 0, hexPosition.y + offset.y);
						//var tri = Instantiate(_triPrefab, position, Quaternion.Euler(0, (240f - 60f * i) % 360, 0));
						//tri.name = "Tri (" + q + "," + r + ") - " + i +")";
					}
				}
			}
		}

		private Corner CreateCorner(int[] cornerCoordinates, Vector2 position)
		{
			var cornerIndex = new CornerIndex(cornerCoordinates);
			var corner = new Corner(cornerIndex, Layout);
//			corner.Index.AssignNeighbouringCorners(position, mapRadius);
//			corner.Index.AssignNeighbouringTriangles(position);
			return corner;
		}

		//private void AddCorner(Corner corner, Vector2 position)
		//{
		//	if (Corners.ContainsKey(corner.Index)) return;
		//	Corners.Add(corner.Index, corner, position);
		//}
	}
}