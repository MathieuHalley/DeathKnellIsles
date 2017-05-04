using UnityEngine;

namespace Assets.Scripts
{

	public class FractionalHex : Hex
	{
		public new readonly float[] V;
		public new float Q { get { return V[0]; } }
		public new float R { get { return V[1]; } }
		public new float S { get { return V[2]; } }

		/*	-------------------------------------------------------------------
		 * 	Constructors
		 *	-------------------------------------------------------------------
		 */
		public FractionalHex() : this(0, 0) { }
		public FractionalHex(Hex h) : this(h.Q, h.S, h.R) { }
		public FractionalHex(Vector2 v) : this(v.x, v.y) { }
		public FractionalHex(Vector3 v) : this(v.x, v.y, v.z) { }
		public FractionalHex(float q, float r) : this(q, r, -q - r) { }

		public FractionalHex(float[] v) : this(v[0], v[1], v[2])
		{
			Debug.Assert(v.Length == 3, v + " doesn't have a length of 3.");
		}

		public FractionalHex(float q, float r, float s)
		{
			V = new[] {q, r, s};
		}

		//	-------------------------------------------------------------------
		public int Magnitude(FractionalHex hex)
		{
			return (int) ((Mathf.Abs(hex.Q) + Mathf.Abs(hex.R) + Mathf.Abs(hex.S)) * 0.5f);
		}

		public int Distance(FractionalHex a, FractionalHex b)
		{
			return Magnitude(a - b);
		}

		/*	-------------------------------------------------------------------
		 * 	Normalise FractionalHex coordinates to Hex coordinates
		 *	-------------------------------------------------------------------
		 */
		public Hex ToHex(float[] v)
		{
			Debug.Assert(v.Length == 3, v + " doesn't have a length of 3.");
			return ToHex(v[0], v[1], v[2]);
		}

		public Hex ToHex(FractionalHex h)
		{
			return ToHex(h.Q, h.R, h.S);
		}

		public Hex ToHex(float q, float r, float s)
		{
			var v = new[] { Mathf.FloorToInt(q), Mathf.FloorToInt(r), Mathf.FloorToInt(s) };
			var vDiff = new[] { Mathf.Abs(v[0] - q), Mathf.Abs(v[1] - r), Mathf.Abs(v[2] - s) };

			//	normalise the members of v to ensure that v[0] + v[1] + v[2] == 0
			if (vDiff[0] > vDiff[1] && vDiff[0] > vDiff[2])
				v[0] = -v[1] - v[2];
			else if (vDiff[1] > vDiff[2])
				v[1] = -v[0] - v[2];
			else
				v[2] = -v[0] - v[1];
			return new Hex(v);
		}

		/*	-------------------------------------------------------------------
		 *	Conversion operators; Vector2 - to & from, Vector3 - to & from, Hex - to & from
		 *	-------------------------------------------------------------------
		 */
		public static explicit operator Vector2(FractionalHex a)
		{
			return new Vector2(a.Q, a.R);
		}

		public static explicit operator FractionalHex(Vector2 v)
		{
			return new FractionalHex(v.x, v.y);
		}

		public static explicit operator FractionalHex(Vector3 v)
		{
			return new FractionalHex(v.x, v.y, v.z);
		}

		public static explicit operator Vector3(FractionalHex a)
		{
			return new Vector3(a.Q, a.R, a.S);
		}

		/*	-------------------------------------------------------------------
		 *	Arithmetic operators; addition, subtraction, multiplication & division
		 *	-------------------------------------------------------------------
		 */
		public static FractionalHex operator +(FractionalHex a, FractionalHex b)
		{
			return new FractionalHex(a.Q + b.Q, a.R + b.R, a.S + b.S);
		}

		public static FractionalHex operator -(FractionalHex a, FractionalHex b)
		{
			return new FractionalHex(a.Q - b.Q, a.R - b.R, a.S - b.S);
		}

		public static FractionalHex operator *(FractionalHex a, float k)
		{
			return new FractionalHex(a.Q * k, a.R * k, a.S * k);
		}

		public static FractionalHex operator /(FractionalHex a, float k)
		{
			return new FractionalHex(a.Q * k, a.R * k, a.S * k);
		}
	}
}