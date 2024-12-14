using CSharpLib;
using CSharpLib.Extensions;
using CSharpLib.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Y2024.Day13;

public static class Solver
{
    public static long Part1()
    {
        var cost = 0L;
        var equationSystems = LoadEquationSystems("Data.txt").ToList();
        foreach(var (AX, BX, RX, AY, BY, RY) in equationSystems)
        {
            if (MathUtils.TrySolve(AX, BX, RX, AY, BY, RY, out var res))
            {
                cost += res.X * 3 + res.Y;
            }
        }
        return cost;
    }

    public static long Part2()
    {
        var cost = 0L;
        var equationSystems = LoadEquationSystems("Data.txt").ToList();
        foreach (var (AX, BX, RX, AY, BY, RY) in equationSystems)
        {
            if (MathUtils.TrySolve(AX, BX, 10000000000000 + RX, AY, BY, 10000000000000 + RY, out var res))
            {
                cost += res.X * 3 + res.Y;
            }
        }
        return cost;
    }

    static IEnumerable<(int AX, int BX, int RX, int AY, int BY, int RY)> LoadEquationSystems(string fileName)
    {
        foreach (var es in new DataLoader(2024, 13).ReadStrings(fileName).ChunkBy(s => s.Trim() == ""))
        {
            var aXaY = es[0].Replace("Button A: X+", "").Replace(" Y+", "").Split(",").Select(int.Parse).ToList();
            var bXbY = es[1].Replace("Button B: X+", "").Replace(" Y+", "").Split(",").Select(int.Parse).ToList();
            var rXrY = es[2].Replace("Prize: X=", "").Replace(" Y=", "").Split(",").Select(int.Parse).ToList();
            yield return (aXaY[0], bXbY[0], rXrY[0], aXaY[1], bXbY[1], rXrY[1]);
        }
    }
}
