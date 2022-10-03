using System.Collections.Generic;

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

        public IEnumerable<(T, int)> All()
        {
            foreach (var key in map.Keys)
            {
                yield return (key, map[key]);
            }
        }

        private readonly Dictionary<T, int> map = new Dictionary<T, int>();
    }
}
