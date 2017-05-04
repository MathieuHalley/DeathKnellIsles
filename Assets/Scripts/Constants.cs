using UnityEngine;

namespace Assets.Scripts
{
	public static class Constants
	{
		public static readonly float SqrtThree = Mathf.Sqrt(3);
		public static readonly float HalfSqrtThree = SqrtThree * 0.5f;
		public static readonly float ThirdSqrtThree = SqrtThree / 3f;
		public static readonly float SixthSqrtThree = SqrtThree / 6f;
		public static readonly float TwoPi = 2 * Mathf.PI;
		public static readonly float TriangleCenter = HalfSqrtThree * (2f / 3f);
		public static readonly float Cos30Degrees = Mathf.Cos(30 * Mathf.Deg2Rad);
		public static readonly float Cos60Degrees = Mathf.Cos(60 * Mathf.Deg2Rad);
		public static readonly float Cos120Degrees = Mathf.Cos(120 * Mathf.Deg2Rad);
	}
}
