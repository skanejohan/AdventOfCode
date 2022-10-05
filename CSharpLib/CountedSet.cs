using System.Collections.Generic;
using System.Linq;

namespace CSharpLib
{
    /// <summary>
    /// Maintains information about how many times an item occurs.
    /// </summary>
    public class CountedSet<T> where T : notnull
    {
        public void Add(T item)
        {
            if (!map.TryGetValue(item, out var value))
            {
                value = 0;
            }
            map[item] = value + 1;
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

        public int Occurs(T item)
        {
            return map.TryGetValue(item, out var value) ? value : 0;
        }

        public IEnumerable<(T Item, int Count)> All()
        {
            foreach (var key in map.Keys)
            {
                yield return (key, map[key]);
            }
        }

        public override string ToString()
        {
            return string.Join(',', All().Select(kv => $"[{kv.Item}={kv.Count}]"));
        }

        private readonly Dictionary<T, int> map = new Dictionary<T, int>();
    }
}
