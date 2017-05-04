using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{

	public class ConversionTest : MonoBehaviour
	{
		public int RingCount = 1;
		// Use this for initialization
		private void Start ()
		{
			var layout = new TriangleMapLayout(Vector2.zero, new TriangleDimensions(1f), MapOrientation.FlatTop, RingCount);

			for (var q = -RingCount; q <= RingCount; ++q)
			{
				var r1 = Mathf.Max(-RingCount, -q - RingCount);
				var r2 = Mathf.Min(RingCount, -q + RingCount);

				for (var r = r1; r <= r2; ++r)
				{
					var corner = new CornerIndex(q,r);
					var cornerIndex = new CornerIndex(corner);
					var position = Corner.CornerIndexToPosition(cornerIndex, layout);
					var newCornerIndex = Corner.PositionToCornerIndex(position, layout);
					Debug.Log(
						"Position: " + position
						+ "\n CornerIndex: (Q:" + cornerIndex.Q + " R:" + cornerIndex.R + " S:" + cornerIndex.S + ") "
						+ "| NewCornerIndex: (Q:" + newCornerIndex.Q + " R: " + newCornerIndex.R + " S: " + newCornerIndex.S + ")");
				}
			}
		}
	}
}
