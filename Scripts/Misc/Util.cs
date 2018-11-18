using System;
using System.Collections.Generic;

namespace Assets.Scripts.Misc
{
    public static class Util
    {
        /// <summary>Returns distinct elements from a sequence by using a specified anonymous function to compare values.</summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the elements of source.</typeparam>
        /// <param name="source">The sequence to remove duplicate elements from.</param>
        /// <param name="keySelector">An anonymous function to compare values.</param>
        /// <returns>An IEnumerable&lt;T&gt; that contains distinct elements from the source sequence.</returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();

            //  elements are only added to the hashset if they haven't been encountered before
            foreach (var element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        /// <summary>Creates a HashSet&lt;T&gt; from an IEnumerable&lt;T&gt;.</summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">The IEnumerable&lt;T&gt; to create a HashSet&lt;T&gt; from.</param>
        /// <returns>A HashSet&lt;T&gt; that contains elements from the input sequence.</returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }
    }
}