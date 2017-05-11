using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
	public enum RotationDirection
	{
		Clockwise,
		CounterClockwise,
	}

	public static class Extension
	{
		public static T WithComponent<T>(this Transform transform, Action<T> action) where T : Component
		{
			var component = transform.GetComponent<T>();

			if (component)
			{
				action(component);
			}

			return component;
		}

		public static T WithComponent<T>(this GameObject gameObject, Action<T> action) where T : Component
		{
			return gameObject.transform.WithComponent(action);
		}

		public static byte[] IntToByteArray(int value)
		{
			var bytes = BitConverter.GetBytes((int)value);

			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes);
			}

			return bytes;
		}

		public static String IntToBitString(int value)
		{
			var bitString = "";
			var bitEncountered = false;

			foreach (var b in new BitArray(new[] { value }).Cast<bool>().Reverse().ToArray())
			{
				if (b && !bitEncountered)
				{
					bitEncountered = true;
				}

				if (bitEncountered)
				{
					bitString += b ? "1" : "0";
				}
			}

			if (bitString == "")
			{
				bitString = "0";
			}

			return bitString;
		}

		public static Vector3[] PolarSort(
			Vector3[] points, Vector3 upDirection,
			RotationDirection sortDirection = RotationDirection.Clockwise)
		{
			points.ToList().Sort((a, b) => PolarComparer(a, b, Vector3.up, RotationDirection.Clockwise) ? 0 : 1);
			return points;
		}


		//	Return true if pointA & pointB are in order; return false if they're out of order
		public static bool PolarComparer(
			Vector3 pointA, Vector3 pointB, Vector3 upDirection,
			RotationDirection sortDirection = RotationDirection.Clockwise)
		{
			Vector2 a, b;
			float upDelta;
			if (upDirection.normalized == Vector3.right)
			{
				a = new Vector2(pointA.y, pointA.z);
				b = new Vector2(pointB.y, pointB.z);
				upDelta = pointA.x - pointB.x;
			}
			else if (upDirection.normalized == Vector3.forward)
			{
				a = new Vector2(pointA.x, pointA.y);
				b = new Vector2(pointB.x, pointB.y);
				upDelta = pointA.z - pointB.z;
			}
			else
			{
				a = new Vector2(pointA.x, pointA.z);
				b = new Vector2(pointB.x, pointB.z);
				upDelta = pointA.y - pointB.y;
			}

			if (sortDirection == RotationDirection.Clockwise)
			{
				var temp = b;
				b = a;
				a = temp;
			}

			if (a.x >= 0 && b.x < 0)
			{
				return true;
			}
			if (a.x < 0 && b.x >= 0)
			{
				return false;
			}
			if (a.x == 0 && b.x == 0)
			{
				if (a.y == b.y)
				{
					return upDelta >= 0;
				}
				if (a.y >= 0 || b.y >= 0)
				{
					return a.y > b.y;
				}
				if (a.y < 0 || b.y < 0)
				{
					return b.y > a.y;
				}
			}

			return a.x * b.y - a.y * b.x < 0;
		}

		public static byte ClampToByte(int value)
		{
			return (byte)Mathf.Clamp(value, byte.MinValue, byte.MaxValue);
		}

		public static byte CeilToByte(float value)
		{
			return (byte)Mathf.CeilToInt(value);
		}

		public static byte RoundToByte(float value)
		{
			return (byte)Mathf.RoundToInt(value);
		}
	}
}
