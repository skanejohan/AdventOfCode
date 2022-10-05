using System.Collections.Generic;
using System.Linq;

namespace CSharpLib
{
    public static class StringExtensions
    {
        // GetPairs("abcdefg") -> [ "ab", "bc", "cd", "de", "ef", "fg" ]
        public static IEnumerable<string> GetPairs(this string s)
        {
            return Enumerable.Range(0, s.Length - 1).Select(i => s.Substring(i, 2));
        }

    }
}
