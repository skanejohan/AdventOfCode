using AdventOfCode.Common;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    static class Day14
    {
        public static long Part1()
        {
            LoadData();
            InitializeInventory();
            Produce("FUEL");
            return long.MaxValue - Inventory["ORE"];
        }

        public static long Part2()
        {
            return 0;
        }

        private static void Produce(string element)
        {
            var rule = ProductionRules[element];            
            while(!InventoryContainsEnoughOfAllRequired(rule.Item1))
            {
                foreach (var required in rule.Item1)
                {
                    if (Inventory[required.Item1] < required.Item2)
                    {
                        Produce(required.Item1);
                    }
                }
            }

            foreach (var required in rule.Item1)
            {
                Inventory[required.Item1] -= required.Item2;
            }
            Inventory[element] += rule.Item2;

            bool InventoryContainsEnoughOfAllRequired(List<(string,long)> required)
                => !required.Any(r => Inventory[r.Item1] < r.Item2);
        }

        private static Dictionary<string, long> Inventory = new Dictionary<string, long>();

        // Wanted => ([Required,RequiredCount],ProducedCount)
        private static Dictionary<string, (List<(string, long)>, long)> ProductionRules;
        
        private static void InitializeInventory()
        {
            Inventory = new Dictionary<string, long>();
            foreach(var element in ProductionRules.Keys)
            {
                Inventory[element] = 0;
            }
            Inventory["ORE"] = long.MaxValue;
        }

        private static void LoadData()
        {
            ProductionRules = new Dictionary<string, (List<(string, long)>, long)> ();
            foreach (var line in DataReader.ReadStrings("Day14Input.txt"))
            {
                var Required = new List<(string, long)>();
                var inputs = line.Split(" => ")[0];
                var output = line.Split(" => ")[1];
                foreach (var i in inputs.Split(", "))
                {
                    Required.Add((i.Split(" ")[1].Trim(), long.Parse(i.Split(" ")[0].Trim())));
                }
                ProductionRules[output.Split(" ")[1].Trim()] = (Required, long.Parse(output.Split(" ")[0].Trim()));
            }
        }
    }
}
