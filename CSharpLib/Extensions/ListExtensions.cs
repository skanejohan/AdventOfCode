using System;
using System.Collections.Generic;

namespace CSharpLib.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Remove the first item from the list and return it.
        /// </summary>
        public static T Pop<T>(this List<T> list)
        {
            var result = list[0];
            list.RemoveAt(0);
            return result;
        }

        /// <summary>
        /// Returns the number of times an increase between two adjacent items is found.
        /// </summary>
        public static int NoOfIncreases(this List<int> items)
        {
            return items.NoOfChanges((a, b) => a < b);
        }

        /// <summary>
        /// Returns a copy of the list with one item replaced.
        /// </summary>
        public static List<T> WithItemAt<T>(this List<T> items, T item, int index) where T : new()
        {
            var result = new List<T>(items);
            result[index] = item;
            return result;
        }

        public static int FindIndex<T>(this List<T> items, Func<T, bool> predicate)
        {
            for (var i = 0; i < items.Count; i++)
            {
                if (predicate(items[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns a new enumerable where each item is the aggregation of three adjacent items in the original list.
        /// </summary>
        public static IEnumerable<T2> SlidingWindow3<T1, T2>(this List<T1> items, Func<T1, T1, T1, T2> producer)
        {
            for (var i = 0; i < items.Count - 2; i++)
            {
                yield return producer(items[i], items[i + 1], items[i + 2]);
            }
        }

        /// <summary>
        /// Returns an enumerable where each item a n-sized window.
        /// </summary>
        public static IEnumerable<IEnumerable<T>> SlidingWindow<T>(this List<T> items, int n)
        {
            var window = new List<T>();
            foreach (var item in items)
            {
                window.Add(item);
                if (window.Count > n)
                {
                    window.RemoveAt(0);
                }
                if (window.Count == n)
                {
                    yield return window;
                }
            }
        }

        private static int NoOfChanges<T>(this List<T> items, Func<T, T, bool> isChange)
        {
            var item = items[0];
            var changes = 0;
            for (var i = 1; i < items.Count; i++)
            {
                if (isChange(item, items[i]))
                {
                    changes++;
                }
                item = items[i];
            }
            return changes;
        }
    }
}
