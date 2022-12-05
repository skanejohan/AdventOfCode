using CSharpLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Y2022.Day05
{
    public static class Solver
    {
        public static string Part1()
        {
            var (stacks, operations) = LoadData("data.txt");
            foreach (var (Count, From, To) in operations)
            {
                Move1(stacks, From, To, Count);
            }
            return Message(stacks);
        }

        public static string Part2()
        {
            var (stacks, operations) = LoadData("data.txt");
            foreach (var (Count, From, To) in operations)
            {
                Move2(stacks, From, To, Count);
            }
            return Message(stacks);
        }

        private static void Move1(List<Stack<char>> stacks, int from, int to, int count)
        {
            for (var i = 0; i < count; i++)
            {
                stacks[to - 1].Push(stacks[from - 1].Pop());
            }
        }

        private static void Move2(List<Stack<char>> stacks, int from, int to, int count)
        {
            var tempStack = new Stack<char>();
            for (var i = 0; i < count; i++)
            {
                tempStack.Push(stacks[from - 1].Pop());
            }
            for (var i = 0; i < count; i++)
            {
                stacks[to - 1].Push(tempStack.Pop());
            }
        }

        private static string Message(IEnumerable<Stack<char>> stacks)
        {
            var sb = new StringBuilder();
            foreach (var stack in stacks)
            {
                sb.Append(stack.Peek());
            }
            return sb.ToString();
        }

        private static (List<Stack<char>> Stacks, IEnumerable<(int Count, int From, int To)> Operations) LoadData(string fileName)
        {
            var stacks = new List<Stack<char>>
            {
                new Stack<char>(), new Stack<char>(), new Stack<char>(),
                new Stack<char>(), new Stack<char>(), new Stack<char>(),
                new Stack<char>(), new Stack<char>(), new Stack<char>()
            };

            var lines = new DataLoader(2022, 5).ReadStrings(fileName).ToList();

            for (var lineIdx = 7; lineIdx >= 0; lineIdx--)
            {
                for (var stackIdx = 0; stackIdx < 9; stackIdx++)
                {
                    var c = lines[lineIdx][1 + stackIdx * 4];
                    if (c != ' ')
                    {
                        stacks[stackIdx].Push(c);
                    }
                }
            }

            var operations = lines.Skip(10).Select(line =>
            {
                var parts = line.Split(" from ");
                var count = int.Parse(parts[0].Skip(5).ToArray());
                var countParts = parts[1].Split(" to ");
                var from = int.Parse(countParts[0]);
                var to = int.Parse(countParts[1]);
                return (count, from, to);
            });

            return (stacks, operations);
        }
    }
}
