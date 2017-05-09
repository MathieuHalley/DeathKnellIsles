using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Assets.Scripts
{
	public static class Extension
	{
		public static T WithComponent<T>(this Transform transform, Action<T> action) where T : Component
		{
			var component = transform.GetComponent<T>();
			if (component) action(component);
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
	}
}
