using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpLib.DataStructures
{
    /// <summary>
    /// Maintains information about how many times an item occurs.
    /// </summary>
    public class CountedSet<T> : IEnumerable<(T Item, long Count)> where T : notnull
    {
        public void Add(T item, long count = 1)
        {
            if (!map.TryGetValue(item, out var value))
            {
                value = 0;
            }
            map[item] = value + count;
        }

        public void Remove(T item)
        {
            if (!map.TryGetValue(item, out var value))
            {
                return;
            }
            if (value == 1)
            {
                map.Remove(item);
            }
            else
            {
                map[item] = value - 1;
            }
        }

        public long Occurs(T item)
        {
            return map.TryGetValue(item, out var value) ? value : 0;
        }

        public IEnumerator<(T Item, long Count)> GetEnumerator()
        {
            foreach (var key in map.Keys)
            {
                yield return (key, map[key]);
            }
        }

        public override string ToString()
        {
            return string.Join(',', this.Select(kv => $"[{kv.Item}={kv.Count}]"));
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private readonly Dictionary<T, long> map = new Dictionary<T, long>();
    }
}
