using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Assets.Scripts
{
	public static class Extensions
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
	}

	public class BiDirectionary<TFirst, TSecond> : IEnumerable<KeyValuePair<TFirst, TSecond>>
	{

		private readonly IDictionary<TFirst,IList<TSecond>> _firstToSecond = new Dictionary<TFirst, IList<TSecond>>();
		private readonly IDictionary<TSecond,IList<TFirst>> _secondToFirst = new Dictionary<TSecond, IList<TFirst>>();

		private static readonly IList<TFirst> EmptyFirstList = new TFirst[0];
		private static readonly IList<TSecond> EmptySecondList = new TSecond[0];

		public IList<TFirst> this[TSecond second] { get { return GetBySecond(second); } }
		public IList<TSecond> this[TFirst first] { get { return GetByFirst(first); } }
		public int Count { get { return _firstToSecond.Count; } }
		public bool IsReadOnly { get { return _firstToSecond.IsReadOnly && _secondToFirst.IsReadOnly; } }
		public ICollection<TFirst> Keys { get { return _firstToSecond.Keys; } }
		public ICollection<TSecond> Values { get { return _secondToFirst.Keys; } }

		public void Add(TFirst first, TSecond second)
		{
			IList<TFirst> firsts;
			IList<TSecond> seconds;

			if (!_firstToSecond.TryGetValue(first, out seconds))
			{
				seconds = new List<TSecond>();
				_firstToSecond[first] = seconds;
			}
			if (!_secondToFirst.TryGetValue(second, out firsts))
			{
				firsts = new List<TFirst>();
				_secondToFirst[second] = firsts;
			}
			seconds.Add(second);
			firsts.Add(first);
		}

		public bool ContainsKey(TFirst key)
		{
			return _firstToSecond.ContainsKey(key);
		}

		public bool ContainsValue(TSecond value)
		{
			return _secondToFirst.ContainsKey(value);
		}

		public IList<TSecond> GetByFirst(TFirst first)
		{
			IList<TSecond> list;
			return _firstToSecond.TryGetValue(first, out list)
				? new List<TSecond>(list)
				: EmptySecondList;
		}

		public IList<TFirst> GetBySecond(TSecond second)
		{
			IList<TFirst> list;
			return _secondToFirst.TryGetValue(second, out list)
				? new List<TFirst>(list)
				: EmptyFirstList;
		}

		public bool Remove(TFirst key)
		{
			foreach (var keyValuePair in _firstToSecond)
			{
				foreach (var value in keyValuePair.Value)
				{
					_secondToFirst.Remove(value);
				}
			}
			return _firstToSecond.Remove(key);
		}

		public bool Remove(TSecond value)
		{
			foreach (var keyValuePair in _secondToFirst)
			{
				foreach (var key in keyValuePair.Value)
				{
					_firstToSecond.Remove(key);
				}
			}
			return _secondToFirst.Remove(value);
		}

		public bool TryGetValue(TFirst key, out IList<TSecond> value)
		{
			return _firstToSecond.TryGetValue(key, out value);
		}

		public IEnumerator<KeyValuePair<TFirst, TSecond>> GetEnumerator()
		{
			foreach (var keyValuePair in _firstToSecond)
			{
				foreach (var value in keyValuePair.Value)
				{
					yield return new KeyValuePair<TFirst,TSecond>(keyValuePair.Key,value);
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public void Clear()
		{
			_firstToSecond.Clear();
			_secondToFirst.Clear();
		}

		public bool Contains(KeyValuePair<TFirst, TSecond> item)
		{
			var containsKey = _firstToSecond.ContainsKey(item.Key);
			var containsValue = _secondToFirst.ContainsKey(item.Value);
			return containsKey && containsValue;
		}

		public void CopyTo(KeyValuePair<TFirst, TSecond>[] array, int arrayIndex)
		{
			Add(array[arrayIndex].Key, array[arrayIndex].Value);
		}

		public bool Remove(KeyValuePair<TFirst, TSecond> item)
		{
			var removedKey = _firstToSecond.Remove(
				new KeyValuePair<TFirst, IList<TSecond>>(item.Key, _firstToSecond[item.Key]));
			var removedValue = _secondToFirst.Remove(
				new KeyValuePair<TSecond, IList<TFirst>>(item.Value, _secondToFirst[item.Value]));
			return removedKey && removedValue;
		}
	}
}
