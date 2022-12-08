using System.Collections.Generic;
using System.Linq;

namespace CSharpLib.Utils
{
    public static class EnumerableUtils
    {
        public static IEnumerable<int> GetRange(int start, int stop)
        {
            return stop > start
                ? Enumerable.Range(start, stop - start + 1)
                : Enumerable.Empty<int>();
        }
    }
}
