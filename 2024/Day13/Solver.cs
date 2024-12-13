using CSharpLib;
using CSharpLib.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Y2024.Day13;

public static class Solver
{
    public static long Part1()
    {
        var cost = 0;
        var equationSystems = LoadEquationSystems("Data.txt").ToList();
        foreach(var (AX, BX, RX, AY, BY, RY) in equationSystems)
        {
            if (TryGetValues(AX, BX, RX, AY, BY, RY, out var res))
            {
                cost += res.A * 3 + res.B;
            }
        }
        return cost;
    }

    public static long Part2()
    {
        return 0;
    }

    static bool TryGetValues(int aX, int bX, int rX, int aY, int bY, int rY, out (int A, int B) result)
    {
        // A * aX + B * bX = rX
        // A * aY + B * bY = rY
        //
        // A = ((rX-rY) - (bX-bY) * B) / (aX - aY)
        for (var b = 1; b <= 100; b++)
        {
            var numerator = rX - rY - (bX - bY) * b;
            var denominator = aX - aY;
            if (numerator % denominator == 0)
            {
                var a = numerator / denominator;
                if (a * aX + b * bX == rX && a * aY + b * bY == rY)
                {
                    result = (a, b);
                    return true;
                }
            }
        }
        result = (0, 0);
        return false;
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
