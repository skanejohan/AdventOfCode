using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpLib.Extensions
{
    public static class StringExtensions
    {
        // GetPairs("abcdefg") -> [ "ab", "bc", "cd", "de", "ef", "fg" ]
        public static IEnumerable<string> GetPairs(this string s)
        {
            return Enumerable.Range(0, s.Length - 1).Select(i => s.Substring(i, 2));
        }

        public static byte[] ToHex(this string s)
        {
            return Enumerable.Range(0, s.Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(s.Substring(x, 2), 16))
                         .ToArray();
        }

        /// <summary>
        /// Return the index of the first occurrence of any of the supplied parameters.
        /// Example: "ninefourone1four".IndexOf("five", "four") will return 4
        /// Example: "ninefourone1four".IndexOf("nine", "four") will return 0
        /// Example: "ninefourone1four".IndexOf("five", "six") will return -1
        /// </summary>
        public static int IndexOf(this string s, params string[] patterns)
        {
            var idx = -1;
            foreach (var pattern in patterns)
            {
                var i = (s.IndexOf(pattern));
                if (i > -1 && (idx == -1 || i < idx))
                {
                    idx = i;
                }
            }
            return idx;
        }

        /// <summary>
        /// Return the index of the last occurrence of any of the supplied parameters.
        /// Example: "ninefourone1four".IndexOf("five", "four") will return 12
        /// Example: "ninefourone1four".IndexOf("nine", "four") will return 0
        /// Example: "ninefourone1four".IndexOf("five", "six") will return -1
        /// </summary>
        public static int LastIndexOf(this string s, params string[] patterns)
        {
            var idx = -1;
            foreach (var pattern in patterns)
            {
                var i = (s.LastIndexOf(pattern));
                if (i > idx)
                {
                    idx = i;
                }
            }
            return idx;
        }

        /// <summary>
        /// Return the first number found in the given string, where numbers are either "1", "2" etc. or they are "one", "two", etc.
        /// Example: "rgxjrsldrfmzq25szhbldzqhrhbjpkbjlsevenseven".FirstNumber() will return 2
        /// Example: "45xj".FirstNumber() will return 4
        /// </summary>
        public static int FirstNumber(this string s)
        {
            var num = 0;
            var idx = int.MaxValue;
            foreach (var (N, S) in numbers)
            {
                var i = s.IndexOf($"{N}", S);
                if (i != -1 && i < idx)
                {
                    idx = i;
                    num = N;
                }
            }
            return num;
        }

        /// <summary>
        /// Return the last number found in the given string, where numbers are either "1", "2" etc. or they are "one", "two", etc.
        /// Example: "rgxjrsldrfmzq25szhbldzqhrhbjpkbjlsevenseven".LastNumber() will return 7
        /// Example: "45xj".LastNumber() will return 5
        /// </summary>
        public static int LastNumber(this string s)
        {
            var num = 0;
            var idx = -1;
            foreach (var (N, S) in numbers)
            {
                var i = s.LastIndexOf($"{N}", S);
                if (i > idx)
                {
                    idx = i;
                    num = N;
                }
            }
            return num;
        }

        private static readonly (int N, string S)[] numbers = { (1, "one"), (2, "two"), (3, "three"), (4, "four"), (5, "five"), (6, "six"), (7, "seven"), (8, "eight"), (9, "nine") };

        public static string ReplaceFromLeft(this string s, Dictionary<string, string> translations)
        {
            var idx = 0;
            string converted = "";
            while (true)
            {
                if (idx > s.Length - 1)
                {
                    return converted;
                }
                if (!Replace())
                {
                    converted += s[idx];
                    idx++;
                }
            }

            bool Replace()
            {
                foreach (var translation in translations)
                {
                    if (s.IndexOf(translation.Key) == idx)
                    {
                        converted += translation.Value;
                        idx += translation.Key.Length;
                        return true;
                    }
                }
                return false;
            }
        }

        public static string ReplaceNumbersFromLeft(this string s)
        {
            return ReplaceFromLeft(s, new Dictionary<string, string> {
                { "zero", "0" },
                { "one", "1" },
                { "two", "2" },
                { "three", "3" },
                { "four", "4" },
                { "five", "5" },
                { "six", "6" },
                { "seven", "7" },
                { "eight", "8" },
                { "nine", "9" }
            });
        }

        public static string WithReplacedChar(this string str, int index, char c)
        {
            char[] array = str.ToCharArray();
            array[index] = c;
            return new string(array);
        }

        public static int FindIndex(this string s, Func<char, bool> predicate)
        {
            for (var i = 0; i < s.Length; i++)
            {
                if (predicate(s[i]))
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
