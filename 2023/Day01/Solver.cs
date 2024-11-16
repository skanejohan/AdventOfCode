using CSharpLib;
using CSharpLib.Extensions;
using System.Data;
using System.Linq;

namespace Y2023.Day01;

public static class Solver
{
    public static long Part1()
    {
        var num = 0;
        foreach (var line in new DataLoader(2023, 1).ReadStrings("Data.txt"))
        {
            var digits = line.ToCharArray().Where(c => c >= '0' && c <= '9').ToArray();
            var value1 = (digits[0] - '0') * 10;
            var value2 = digits[^1] - '0';
            num += value1 + value2;
        }
        return num;
    }

    public static long Part2()
    {
        var num = 0;
        foreach (var line in new DataLoader(2023, 1).ReadStrings("Data.txt"))
        {
            num += line.FirstNumber() * 10 + line.LastNumber();
        }
        return num;
    }
}
