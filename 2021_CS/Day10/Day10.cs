using CSharpLib;
using System.Collections.Generic;
using System.Linq;

namespace _2021_CS
{
    public static class Day10
    {
        public static long Part1()
        {
            var values = new Dictionary<char, int>
            {
                { ')', 3 },
                { ']', 57 },
                { '}', 1197 },
                { '>', 25137 },
            };            
            return RealData()
                .Select(ProcessString)
                .Where(x => x.IncorrectClosingChar != ' ')
                .Select(x => values[x.IncorrectClosingChar])
                .Sum();
        }

        public static long Part2()
        {
            var scores = RealData()
                .Select(ProcessString)
                .Where(x => x.IncorrectClosingChar == ' ')
                .Select(x => Score(x.Remaining))
                .OrderBy(s => s)
                .ToList();
            return scores[scores.Count / 2];

            static long Score(string s)
            {
                var values = new Dictionary<char, int>
                {
                    { '(', 1 },
                    { '[', 2 },
                    { '{', 3 },
                    { '<', 4 },
                };
                long score = 0;
                foreach (var c in s)
                {
                    score = score * 5 + values[c];
                }
                return score;
            }
        }

        private static (bool Successful, char IncorrectClosingChar, string Remaining) ProcessString(string s)
        {
            var stack = new Stack<char>();
            foreach (var c in s)
            {
                switch (c)
                {
                    case '(':
                    case '[':
                    case '{':
                    case '<':
                        stack.Push(c);
                        break;
                    case ')':
                        if (stack.Pop() != '(')
                        {
                            return (false, ')', "");
                        }
                        break;
                    case ']':
                        if (stack.Pop() != '[')
                        {
                            return (false, ']', "");
                        }
                        break;
                    case '}':
                        if (stack.Pop() != '{')
                        {
                            return (false, '}', "");
                        }
                        break;
                    case '>':
                        if (stack.Pop() != '<')
                        {
                            return (false, '>', "");
                        }
                        break;
                }
            }
            if (stack.Count == 0)
            {
                return (true, ' ', "");
            }
            else
            {
                return (false, ' ', new string(stack.ToArray()));
            }
        }

        private static IEnumerable<string> TestData() => new DataLoader("2021_CS", 10).ReadStrings("DataTest.txt");
        private static IEnumerable<string> RealData() => new DataLoader("2021_CS", 10).ReadStrings("DataReal.txt");
    }
}
