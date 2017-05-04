using UnityEngine;

namespace Assets.Scripts
{
	public struct CornerIndex
	{
		private readonly int[] _coordinates;

		public int Q { get { return _coordinates[0]; } }
		public int R { get { return _coordinates[1]; } }
		public int S { get { return _coordinates[2]; } }
		public int[] Coordinates { get { return _coordinates; } }

		public CornerIndex(CornerIndex h) : this(h.Q, h.R, h.S) { }
		public CornerIndex(int q, int r) : this(q, r, -q - r) { }

		public CornerIndex(int[] v) : this(v[0], v[1], v[2])
		{
			Debug.Assert(v.Length == 3, v + " doesn't have a length of 3.");
		}

		public CornerIndex(int q, int r, int s) : this()
		{
			Debug.Assert(q + r + s == 0, "The sum of Q, R & S doesn't equal 0.");
			_coordinates = new[] { q, r, s };
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType()) return false;

			return Coordinates == ((CornerIndex)obj).Coordinates;
		}

		public override int GetHashCode()
		{
			return _coordinates.GetHashCode();
		}

		public override string ToString()
		{
			return "(Q:" + Q + ", R:" + R + ", S:" + S + ")";
		}

		public static CornerIndex operator -(CornerIndex a, CornerIndex b)
		{
			return new CornerIndex(a.Q - b.Q, a.R - b.R, a.S - b.S);
		}

		public static CornerIndex operator +(CornerIndex a, CornerIndex b)
		{
			return new CornerIndex(a.Q + b.Q, a.R + b.R, a.S + b.S);
		}

		public static CornerIndex operator *(CornerIndex a, int b)
		{
			return new CornerIndex(a.Q * b, a.R * b, a.S * b);
		}
	}
}
