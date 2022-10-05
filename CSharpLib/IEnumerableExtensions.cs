using System;
using System.Collections.Generic;

namespace CSharpLib
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Returns an enumerable of lists where the individual lists are split by 
        /// a line in the original input for which splitFunction returns true.
        /// </summary>
        public static IEnumerable<List<T>> ChunkBy<T>(this IEnumerable<T> items, Func<T, bool> splitFunction)
        {
            var chunk = new List<T>();
            foreach (var item in items)
            {
                if (splitFunction(item))
                {
                    if (chunk.Count > 0)
                    {
                        yield return chunk;
                    }
                    chunk = new List<T>();
                }
                else
                {
                    chunk.Add(item);
                }
            }
            if (chunk.Count > 0)
            {
                yield return chunk;
            }
        }
    }
}
