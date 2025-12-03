using CSharpLib;

namespace Y2025.Day03;

public static class Solver
{
    public static long Part1()
    {
        var sum = 0L;
        var lines = new DataLoader(2025, 3).ReadCharArrays("Data.txt");
        foreach(var line in lines)
        {
            var c1 = FindHighest(line, 0, line.Length - 2);
            var c2 = FindHighest(line, c1.Index + 1, line.Length - 1);
            sum += c1.Value * 10 + c2.Value;
        }
        return sum;
    }

    public static long Part2()
    {
        var sum = 0L;
        var lines = new DataLoader(2025, 3).ReadCharArrays("Data.txt");
        foreach (var line in lines)
        {
            var index = -1;
            var lineSum = 0L;
            for (var i = 0; i < 12; i++)
            {
                var c = FindHighest(line, index + 1, line.Length - (12 - i));
                lineSum = lineSum * 10 + c.Value;
                index = c.Index;
            }
            sum += lineSum;
        }
        return sum;
    }

    private static (int Value, int Index) FindHighest(char[] chars, int minIndex, int maxIndex)
    {
        var highest = (chars[minIndex], minIndex);
        for (var i = minIndex + 1; i <= maxIndex; i++)
        {
            if (chars[i] > highest.Item1)
            {
                highest = (chars[i], i);
                if (chars[i] == '9')
                {
                    break;
                }
            }
        }
        return (highest.Item1 - '0', highest.Item2);
    }
}
