using UnityEngine;

namespace Assets.Scripts
{
	public class CornerIndex
	{
		public int Q { get { return Coordinates[0]; } }
		public int R { get { return Coordinates[1]; } }
		public int S { get { return Coordinates[2]; } }
		public int[] Coordinates { get; private set; }
		public int CornerIndexRing { get; private set; }

		public CornerIndex(CornerIndex h) : this(h.Q, h.R, h.S) { }
		public CornerIndex(int q, int r) : this(q, r, -q - r) { }
		public CornerIndex(int[] v) : this(v[0], v[1], v[2])
		{
			Debug.Assert(v.Length == 3, v + " doesn't have a length of 3.");
		}

		public CornerIndex(int q, int r, int s)
		{
			Debug.Assert(q + r + s == 0, "The sum of Q, R & S doesn't equal 0.");
			Coordinates = new[] { q, r, s };
			CornerIndexRing = Mathf.Max(Mathf.Abs(q), Mathf.Abs(r), Mathf.Abs(s));
	}

		public bool IsCornerIndexInRing(int ringNumber)
		{
			return CornerIndexRing == ringNumber;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType()) return false;

			return Coordinates == ((CornerIndex)obj).Coordinates;
		}

		public override int GetHashCode()
		{
			return Coordinates.GetHashCode();
		}

		public override string ToString()
		{
			return "(Q:" + Q + ", R:" + R + ", S:" + S + ")";
		}

		public static bool operator <(CornerIndex a, CornerIndex b)
		{
			return a.CornerIndexRing < b.CornerIndexRing;
		}

		public static bool operator >(CornerIndex a, CornerIndex b)
		{
			return a.CornerIndexRing > b.CornerIndexRing;
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
