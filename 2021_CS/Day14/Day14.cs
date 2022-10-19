using CSharpLib;
using CSharpLib.DataStructures;
using CSharpLib.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace _2021_CS
{
    public static class Day14
    {
        public static long Part1()
        {
            return Run("RealData.txt", 10);
        }

        public static long Part2()
        {
            return Run("RealData.txt", 40);
        }

        private static long Run(string fileName, int noOfSteps)
        {
            var (template, first, last, rules) = GetData(fileName);
            template = ApplyRulesN(template, rules, noOfSteps);
            var elementCounts = GetElementCounts(template, first, last);
            var max = elementCounts.Select(c => c.Count).Max();
            var min = elementCounts.Select(c => c.Count).Min();
            return max - min;
        }

        private static IEnumerable<(char Character, long Count)> GetElementCounts(CountedSet<string> template, char first, char last)
        {
            var result = new CountedSet<char>();
            foreach (var (Item, Count) in template)
            {
                result.Add(Item[0], Count);
                result.Add(Item[1], Count);
            }
            foreach (var (Item, Count) in result)
            {
                if (Item == first || Item == last)
                {
                    yield return (Item, Count / 2 + 1);
                }
                else
                {
                    yield return (Item, Count / 2);
                }
            }
        }

        private static CountedSet<string> ApplyRulesN(CountedSet<string> template, Dictionary<string, List<string>> rules, int n)
        {
            for (var i = 0; i < n; i++)
            {
                template = ApplyRules(template, rules);
            }
            return template;
        }

        private static CountedSet<string> ApplyRules(CountedSet<string> template, Dictionary<string, List<string>> rules)
        {
            var result = new CountedSet<string>();
            foreach (var (Item, Count) in template)
            {
                foreach(var target in rules[Item])
                {
                    result.Add(target, Count);
                }
            }
            return result;
        }

        private static (CountedSet<string> Template, char First, char Last, Dictionary<string, List<string>> Rules) GetData(string fileName)
        {
            var parts = new DataLoader(2021, 14).ReadStrings(fileName).ChunkBy(line => line == "").ToList();

            var templateString = parts[0].Single();
            var template = new CountedSet<string>();
            foreach (var pair in templateString.GetPairs())
            {
                template.Add(pair);
            }

            var rules = new Dictionary<string, List<string>>();
            foreach (var ruleLine in parts[1])
            {
                char[] fromChars = { ruleLine[0], ruleLine[1] };
                char[] toChars1 = { ruleLine[0], ruleLine[6] };
                char[] toChars2 = { ruleLine[6], ruleLine[1] };
                var from = new string(fromChars);
                var to1 = new string(toChars1);
                var to2 = new string(toChars2);
                rules.Add(from, new List<string> { to1, to2 });
            }

            return (template, templateString.First(), templateString.Last(), rules);
        }
    }
}
