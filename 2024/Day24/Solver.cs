using CSharpLib;
using CSharpLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2024.Day24;

public static class Solver
{
    public static long Part1()
    {
        var (Values, Functions) = LoadFunctions("data.txt");
        return EvaluateFunctions(Values, Functions);
    }

    public static string Part2()
    {
        var (_, Functions) = LoadFunctions("data.txt");
        return string.Join(',', FindIncorrect(Functions).Order());
    }

    static HashSet<string> FindIncorrect(Dictionary<string, Function> functions)
    {
        HashSet<char> xyz = ['x', 'y', 'z'];
        HashSet<string> incorrect = [];

        foreach (var kvp in functions)
        {
            var output = kvp.Key;
            var function = kvp.Value;
            if (output[0] == 'z' && function.Op != '^' && output != "z45")
            {
                incorrect.Add(output);
            }
            else if (function.Op == '^' && !xyz.Contains(output[0]) && !xyz.Contains(function.Left[0]) && !xyz.Contains(function.Right[0]))
            {
                incorrect.Add(output);
            }
            else if (function.Op == '&' && function.Left != "x00" && function.Right != "x00")
            {
                foreach (var kvp2 in functions)
                {
                    if ((output == kvp2.Value.Left || output == kvp2.Value.Right) && kvp2.Value.Op != '|')
                    {
                        incorrect.Add(output);
                    }
                }
            }
            else if (function.Op == '^')
            {
                foreach (var kvp2 in functions)
                {
                    if ((output == kvp2.Value.Left || output == kvp2.Value.Right) && kvp2.Value.Op == '|')
                    {
                        incorrect.Add(output);
                    }
                }
            }
        }

        return incorrect;
    }

    private static long EvaluateFunctions(Dictionary<string, bool> values, Dictionary<string, Function> functions)
    {
        var n = 0L;
        var zs = functions.Keys.Where(k => k.StartsWith('z')).Order().ToList();
        for (var i = 0; i < zs.Count; i++)
        {
            if (Evaluate(zs[i]))
            {
                n += (long)Math.Pow(2, i);
            }
        }
        return n;

        bool Evaluate(string fn)
        {
            if (values.TryGetValue(fn, out var value))
            {
                return value;
            }

            if (functions.TryGetValue(fn, out var function))
            {
                var l = Evaluate(function.Left);
                var r = Evaluate(function.Right);
                return function.Op switch
                {
                    '&' => l & r,
                    '|' => l | r,
                    _ => l ^ r
                };
            }

            throw new Exception();
        }
    }

    private static (Dictionary<string, bool> Values, Dictionary<string, Function> Functions) LoadFunctions(string fileName)
    {
        var values = new Dictionary<string, bool>();
        var functions = new Dictionary<string, Function>();
        var data = new DataLoader(2024, 24).ReadStrings(fileName).ChunkBy(string.IsNullOrEmpty);

        foreach (var line in data.First())
        {
            var parts = line.Split(": ");
            values[parts[0]] = parts[1] == "1";
        }

        foreach (var line in data.Skip(1).First())
        {
            var parts = line.Split(" -> ");
            var target = parts[1];
            var expr = parts[0];
            var exprParts = expr.Split(" ");
            functions[target] = exprParts[1] switch
            {
                "AND" => new Function(exprParts[0], '&', exprParts[2]),
                "OR" => new Function(exprParts[0], '|', exprParts[2]),
                _ => new Function(exprParts[0], '^', exprParts[2]),
            };
        }

        return (values, functions);
    }

    record Function(string Left, char Op, string Right);
}
