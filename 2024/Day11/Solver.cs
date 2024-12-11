using System.Collections.Generic;
using System.Linq;

namespace Y2024.Day11;

public static class Solver
{
    public static long Part1()
    {
        var list = new LinkedList<string>(["965842", "9159", "3372473", "311", "0", "6", "86213", "48"]);
        for (var i = 0; i < 25; i++)
        {
            ProcessList(list);
        }
        return list.Count;

        static void ProcessList(LinkedList<string> list)
        {
            var node = list.First;
            while (node != null)
            {
                var nextNode = node.Next;
                if (node.Value == "0")
                {
                    node.Value = "1";
                }
                else if (node.Value.Length % 2 == 0)
                {
                    list.AddAfter(node, $"{long.Parse(node.Value[(node.Value.Length / 2)..])}");
                    node.Value = $"{long.Parse(node.Value[..(node.Value.Length / 2)])}";
                }
                else
                {
                    node.Value = $"{long.Parse(node.Value) * 2024}";
                }
                node = nextNode;
            }
        }
    }

    public static long Part2()
    {
        Dictionary<(string, int), long> resultingStones = [];
        var list = new List<string>(["965842", "9159", "3372473", "311", "0", "6", "86213", "48"]);
        return list.Select(s => NoOfStones(s, 75)).Sum();

        long NoOfStones(string stoneValue, int blink)
        {
            if (blink == 0)
            {
                return 1;
            }
            if (resultingStones.TryGetValue((stoneValue, blink), out long n))
            {
                return n;
            }
            if (stoneValue == "0")
            {
                n = NoOfStones("1", blink - 1);
            }
            else if (stoneValue.Length % 2 == 0)
            {
                n = NoOfStones($"{long.Parse(stoneValue[(stoneValue.Length / 2)..])}", blink - 1) +
                    NoOfStones($"{long.Parse(stoneValue[..(stoneValue.Length / 2)])}", blink - 1);
            }
            else
            {
                n = NoOfStones($"{long.Parse(stoneValue) * 2024}", blink - 1);
            }
            resultingStones[(stoneValue, blink)] = n;
            return n;
        }

    }
}
