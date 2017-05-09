using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{

	public class ConversionTest : MonoBehaviour
	{
		public int RingCount = 1;
		public GameObject cornerPrefab;

		private TriangleMapLayout layout;
		public Dictionary<CornerIndex, Corner> cornerMap = new Dictionary<CornerIndex, Corner>();
		// Use this for initialization
		private void Start ()
		{
			layout = new TriangleMapLayout(
				Vector2.zero, 
				new TriangleDimensions(1f), 
				MapOrientation.FlatTop, RingCount);

			var originCorner = new Corner(new CornerIndex(0, 0, 0), layout);
			var cornerQueue = new Queue<Corner>();
			cornerQueue.Enqueue(originCorner);

			while (cornerQueue.Count > 0 && cornerQueue.Peek().Index.CornerIndexRing <= RingCount)
			{
				var currentCorner = cornerQueue.Dequeue();

				currentCorner.AssignNeighbouringCorners(layout);

				foreach (var corner in currentCorner.NeighbouringCorners)
				{
					if (cornerQueue.Contains(corner) || cornerMap.ContainsKey(corner.Index))
						continue;
					cornerQueue.Enqueue(corner);
				}

				Debug.Log("cornerQueue.Count:" + cornerQueue.Count);
				if (cornerMap.ContainsKey(currentCorner.Index)) continue;

				cornerMap.Add(currentCorner.Index, currentCorner);
				var position = Corner.CornerIndexToPosition(currentCorner.Index, layout);
				var cornerObject = Instantiate(cornerPrefab, new Vector3(position.x, 0, position.y), Quaternion.identity);
				cornerObject.name = currentCorner.Index.ToString();
			}
		}

		private void Update()
		{
			foreach(var corner in cornerMap)
			{
				var cornerIndex = corner.Key;
				var cornerValue = corner.Value;
				var position = new Vector3(cornerValue.Position.x, 0, cornerValue.Position.y);
				foreach (var neighbour in cornerValue.NeighbouringCorners)
				{
//					if (neighbour.Index.CornerIndexRing > layout.RingCount) continue;
					var neighbourPosition = new Vector3(neighbour.Position.x, 0, neighbour.Position.y);
					Debug.DrawLine(position, neighbourPosition);
				}
			}
		}
	}
}
