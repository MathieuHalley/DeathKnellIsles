using UnityEngine;

namespace Assets.Scripts
{
	public enum HexOffset { Even = 1, Odd = -1 }

	public class Hex
	{
		public readonly int[] V;
		public Hex[] Neighbours = new Hex[6];
		public int Q { get { return V[0]; } }
		public int R { get { return V[1]; } }
		public int S { get { return V[2]; } }

		private static readonly Hex[] NeighbourDirections = 
		{
			new Hex( 1, 0,-1), new Hex( 1,-1, 0), new Hex(0,-1, 1),
			new Hex(-1, 0, 1), new Hex(-1, 1, 0), new Hex(0, 1,-1)
		};

		/*	-------------------------------------------------------------------
		 * 	Constructors
		 *	-------------------------------------------------------------------
		 */
		public Hex() : this(0, 0) { }
		public Hex(Hex h) : this(h.Q, h.S, h.R) { }
		public Hex(Vector2 v) : this(v.x, v.y) { }
		public Hex(Vector3 v) : this(v.x, v.y, v.z) { }
		public Hex(int q, int r) : this(q, r, -q - r) { }
		public Hex(float q, float r) : this((int)q, (int)r) { }
		public Hex(float q, float r, float s) : this((int)q, (int)r, (int)s) { }

		public Hex(int[] v) : this(v[0], v[1], v[2])
		{
			Debug.Assert(v.Length == 3, v + " doesn't have a length of 3.");
		}

		public Hex(int q, int r, int s)
		{
			Debug.Assert(q + r + s == 0, "The sum of Q, R & S doesn't equal 0.");
			V = new []{q, r, s};

		}

		//	-------------------------------------------------------------------
		public int Magnitude(Hex hex)
		{
			return (int)((Mathf.Abs(hex.Q) + Mathf.Abs(hex.R) + Mathf.Abs(hex.S)) * 0.5f);
		}

		public int Distance(Hex a, Hex b)
		{
			return Magnitude(a - b);
		}

		public static Hex NeighbourDirection(uint direction)
		{
			return NeighbourDirections[direction % 6];
		}

		public Hex Neighbour(uint direction)
		{
			return Neighbour(this, direction);
		}

		public static Hex Neighbour(Hex hex, uint direction)
		{
			return hex.Neighbours[direction] ?? (hex.Neighbours[direction] = hex + NeighbourDirection(direction));
		}

		/*	-------------------------------------------------------------------
		 * 	Utility functions
		 *	-------------------------------------------------------------------
		 */
		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			var hexObj = (Hex)obj;
			return Q == hexObj.Q && R == hexObj.R && S == hexObj.S;
		}

		public override int GetHashCode()
		{
			return V.GetHashCode();
		}

		public override string ToString()
		{
			return "(Q:" + Q + ",R:" + R + ",S:" + S + ")";
		}



		/*	-------------------------------------------------------------------
		 * 	Conversion
		 *	-------------------------------------------------------------------
		 */
		public static Vector2 AxialToOffsetPointy(HexOffset offset, Hex h)
		{
			var col = h.Q;
			var row = h.R + Mathf.FloorToInt((h.Q + (int)offset * (h.Q & 1)) / 2f);
			return new Vector2(col, row);
		}

		public static Hex OffsetToAxialPointy(HexOffset offset, Vector2 coord)
		{
			var q = (int)coord.x;
			var r = (int)coord.y + Mathf.FloorToInt((coord.x + (int)offset * ((int)coord.x & 1)) / 2f);
			return new Hex(q, r);
		}

		public static Vector2 AxialToOffsetFlat(HexOffset offset, Hex h)
		{
			var col = h.Q + Mathf.FloorToInt((h.R + (int)offset * (h.R & 1)) / 2f);
			var row = h.R;
			return new Vector2(col, row);
		}

		public static Hex OffsetToAxialFlat(HexOffset offset, Vector2 coord)
		{
			var q = (int)coord.x + Mathf.FloorToInt((coord.y + (int)offset * ((int)coord.y & 1)) / 2f);
			var r = (int)coord.y;
			return new Hex(q, r);
		}

		public static explicit operator Vector2(Hex a)
		{
			return new Vector2(a.Q, a.R);
		}

		public static explicit operator Vector3(Hex a)
		{
			return new Vector3(a.Q, a.R, a.S);
		}

		public static explicit operator Hex(Vector2 v)
		{
			return new Hex(v.x, v.y);
		}

		public static explicit operator Hex(Vector3 v)
		{
			return new Hex(v.x, v.y, v.z);
		}

		/*	-------------------------------------------------------------------
		 *	Boolean comparison operators; equality & inequality
		 *	-------------------------------------------------------------------
		 */
		public static bool operator ==(Hex a, Hex b)
		{
			if (ReferenceEquals(a, b)) return true;
			if ((object)a == null || (object)b == null) return false;
			return a.Equals(b);
		}

		public static bool operator !=(Hex a, Hex b)
		{
			if (ReferenceEquals(a, b)) return false;
			if ((object)a == null || (object)b == null) return true;
			return !(a == b);
		}

		/*	-------------------------------------------------------------------
		 *	Arithmetic operators; addition, subtraction & multiplication
		 *	-------------------------------------------------------------------
		 */
		public static Hex operator +(Hex a, Hex b)
		{
			return new Hex(a.Q + b.Q, a.R + b.R, a.S + b.S);
		}

		public static Hex operator -(Hex a, Hex b)
		{
			return new Hex(a.Q - b.Q, a.R - b.R, a.S - b.S);
		}

		public static Hex operator *(Hex a, int k)
		{
			return new Hex(a.Q * k, a.R * k, a.S * k);
		}
	}
}
