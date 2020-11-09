using AdventOfCode.Common;
using System;
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
            ProduceElement("FUEL", 1);
            return oreProduced;
        }

        public static long Part2()
        {
            LoadData();

            var ore = 1000000000000L;
            var fuelToProduce = 1L;
            while (true)
            {
                InitializeInventory();
                oreProduced = 0;
                ProduceElement("FUEL", fuelToProduce);

                var newFuelToProduce = (long)((double)ore / oreProduced * fuelToProduce); // binary search until correct ore used
                if (newFuelToProduce == fuelToProduce)
                {
                    return newFuelToProduce;
                }
                fuelToProduce = newFuelToProduce;
            }
        }

        private static long oreProduced = 0;
        private static Queue<(string, long)> Need = new Queue<(string, long)>();
        private static Dictionary<string, long> Inventory = new Dictionary<string, long>();
        private static Dictionary<string, (List<(string, long)>, long)> ProductionRules; // Wanted => ([Required,RequiredCount],ProducedCount)

        private static void ProduceElement(string element, long count)
        {
            Need.Enqueue((element, count));
            while (Need.Any())
            {
                var (e, n) = Need.Dequeue();
                if (e == "ORE")
                {
                    oreProduced += n;
                }
                else
                {
                    var rule = ProductionRules[e];
                    var fromInv = Math.Min(n, Inventory[e]);
                    n -= fromInv;
                    Inventory[e] -= fromInv;

                    if (n > 0)
                    {
                        var multiplier = (long)Math.Ceiling((decimal)n / rule.Item2);
                        Inventory[e] = Math.Max(0, multiplier * rule.Item2 - n);

                        foreach (var required in rule.Item1)
                        {
                            Need.Enqueue((required.Item1, required.Item2 * multiplier));
                        }
                    }
                }
            }
        }
        
        private static void InitializeInventory()
        {
            Inventory = new Dictionary<string, long>();
            foreach(var element in ProductionRules.Keys)
            {
                Inventory[element] = 0;
            }
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
