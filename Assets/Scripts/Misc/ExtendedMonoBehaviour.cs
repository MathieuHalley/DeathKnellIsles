using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class ExtendedMonoBehaviour : MonoBehaviour
	{
		private readonly Dictionary<Type, Component> cachedComponents = new Dictionary<Type, Component>();

		public Collider Collider { get { return GetAndCacheComponent<Collider>(); } }
		public Collider2D Collider2D { get { return GetAndCacheComponent<Collider2D>(); } }
		public Rigidbody Rigidbody { get { return GetAndCacheComponent<Rigidbody>(); } }
		public Rigidbody2D Rigidbody2D { get { return GetAndCacheComponent<Rigidbody2D>(); } }
		public Transform Transform { get { return GetAndCacheComponent<Transform>(); } }

		private void Awake()
		{
			var obj = GameObject.Find("app");

			if (obj == null) { UnityEngine.SceneManagement.SceneManager.LoadScene("preload"); }
		}

		public T WithComponent<T>(Action<T> action) where T : Component
		{
			var component = GetAndCacheComponent<T>();

			if (component) { action(component); }

			return component;
		}
		
		private T GetAndCacheComponent<T>() where T : Component
		{
			var type = typeof(T);
			var contains = cachedComponents.ContainsKey(type);
			var component = contains ? cachedComponents[type] as T : GetComponent<T>();

			if (!component && contains) { cachedComponents.Remove(type); }
			else if (component && !contains) { cachedComponents[type] = component; }

			return component;
		}
	}
}
