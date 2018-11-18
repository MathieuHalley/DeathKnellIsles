using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Misc
{
    public class ExtendedMonoBehaviour : MonoBehaviour
    {
        readonly Dictionary<Type, Component> _cachedComponents = new Dictionary<Type, Component>();

        public Collider Collider => GetAndCacheComponent<Collider>();
        public Collider2D Collider2D => GetAndCacheComponent<Collider2D>();
        public Rigidbody Rigidbody => GetAndCacheComponent<Rigidbody>();
        public Rigidbody2D Rigidbody2D => GetAndCacheComponent<Rigidbody2D>();
        public Transform Transform => GetAndCacheComponent<Transform>();

        void Awake()
        {
            if (GameObject.Find("app") == null)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("preload");
            }
        }

        public T WithComponent<T>(Action<T> action) where T : Component
        {
            var component = GetAndCacheComponent<T>();

            if (component)
            { action(component); }

            return component;
        }

        T GetAndCacheComponent<T>() where T : Component
        {
            var type = typeof(T);
            var contains = _cachedComponents.ContainsKey(type);
            var component = contains ? _cachedComponents[type] as T : GetComponent<T>();

            if (!component && contains)
            {
                _cachedComponents.Remove(type);
            }
            else if (component && !contains)
            {
                _cachedComponents[type] = component;
            }

            return component;
        }
    }
}