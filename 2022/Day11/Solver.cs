using CSharpLib;
using CSharpLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2022.Day11
{
    public static class Solver
    {
        public static long Part1()
        {
            var monkeys = LoadData("data.txt").ToList();
            for (int i = 0; i < 20; i++)
            {
                ProcessMonkeys(monkeys, 0, 3);
            }
            var inspected = monkeys.Select(m => m.ItemsInspected).OrderBy(i => i).Reverse().ToList();
            return inspected[0] * inspected[1];
        }

        public static long Part2()
        {
            var monkeys = LoadData("data.txt").ToList();
            for (int i = 0; i < 10000; i++)
            {
                ProcessMonkeys(monkeys, monkeys.Select(m => m.Divisor).Lcm(), 1);
            }
            var inspected = monkeys.Select(m => m.ItemsInspected).OrderBy(i => i).Reverse().ToList();
            return inspected[0] * inspected[1];
        }

        private static void ProcessMonkeys(List<Monkey> monkeys, long lcm, int worryLevelReduction)
        {
            foreach(var monkey in monkeys)
            {
                while (monkey.TryGetFirstItem(out var item))
                {
                    item = monkey.Operation switch
                    {
                        Operation.Add => item + monkey.Factor,
                        Operation.Mult => item * monkey.Factor,
                        _ => item * item
                    };
                    if (lcm != 0 && item > lcm)
                    {
                        item = item % lcm;
                    }
                    if (worryLevelReduction > 1)
                    {
                        item /= worryLevelReduction;
                    }
                    if (item % monkey.Divisor == 0)
                    {
                        monkeys[monkey.WhenTrue].AddItem(item);
                    }
                    else
                    {
                        monkeys[monkey.WhenFalse].AddItem(item);
                    }
                }
            }
        }

        private static IEnumerable<Monkey> LoadData(string fileName)
        {
            return new DataLoader(2022, 11)
                .ReadStrings(fileName)
                .ChunkBy(s => s == "")
                .Select(sl => new Monkey(sl));
        }

        enum Operation { Add, Mult, MultSelf }

        class Monkey
        {
            public Operation Operation { get; }
            public long Factor { get; }
            public long Divisor { get; }
            public int WhenTrue { get; }
            public int WhenFalse { get; }
            public long ItemsInspected { get; private set; }

            public Monkey(List<string> strings)
            {
                items.AddRange(strings[1]["  Starting items: ".Length..].Split(", ").Select(long.Parse));
                var op = strings[2]["  Operation: new = old ".Length..];
                if (op == "* old")
                {
                    Operation = Operation.MultSelf;
                    Factor = 1;
                }
                else if (op.Contains('*'))
                {
                    Operation = Operation.Mult;
                    Factor = int.Parse(op["* ".Length..]);
                }
                else 
                {
                    Operation = Operation.Add;
                    Factor = int.Parse(op["+ ".Length..]);
                }
                Divisor = int.Parse(strings[3]["  Test: divisible by ".Length..]);
                WhenTrue = int.Parse(strings[4]["    If true: throw to monkey ".Length..]);
                WhenFalse = int.Parse(strings[5]["    If false: throw to monkey ".Length..]);
                ItemsInspected = 0;
            }

            public bool TryGetFirstItem(out long item)
            {
                if (items.Any())
                {
                    item = items[0];
                    items.RemoveAt(0);
                    ItemsInspected++;
                    return true;
                }
                item = 0;
                return false;
            }

            public void AddItem(long item)
            {
                items.Add(item);
            }

            private readonly List<long> items = new();
        }
    }
}
