using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
	public static class Extension
	{
		/// <summary></summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="transform"></param>
		/// <param name="action"></param>
		/// <returns></returns>
		public static T WithComponent<T>(this Transform transform, Action<T> action) where T : Component
		{
			var component = transform.GetComponent<T>();

			if (component) { action(component); }

			return component;
		}

		/// <summary></summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="gameObject"></param>
		/// <param name="action"></param>
		/// <returns></returns>
		public static T WithComponent<T>(this GameObject gameObject, Action<T> action) where T : Component
		{
			return gameObject.transform.WithComponent(action);
		}

		/// <summary></summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static byte[] IntToByteArray(int value)
		{
			var bytes = BitConverter.GetBytes((int)value);

			if (BitConverter.IsLittleEndian) { Array.Reverse(bytes); }

			return bytes;
		}

		/// <summary></summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string IntToBitString(int value)
		{
			var bitString = "";
			var bitEncountered = false;

			foreach (var b in new BitArray(new[] { value }).Cast<bool>().Reverse().ToArray())
			{
				if (b && !bitEncountered) { bitEncountered = true; }
				if (bitEncountered) { bitString += b ? "1" : "0"; }
			}

			if (bitString == "") { bitString = "0"; }

			return bitString;
		}

		/// <summary></summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static byte ClampToByte(int value)
		{
			return (byte)Mathf.Clamp(value, byte.MinValue, byte.MaxValue);
		}

		/// <summary></summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static byte CeilToByte(float value)
		{
			return (byte)Mathf.CeilToInt(value);
		}

		/// <summary></summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static byte RoundToByte(float value)
		{
			return (byte)Mathf.RoundToInt(value);
		}
	}
}
