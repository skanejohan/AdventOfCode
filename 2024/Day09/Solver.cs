using CSharpLib;
using System.Collections.Generic;
using System.Linq;

namespace Y2024.Day09;

public static class Solver
{
    public static long Part1()
    {
        var data = ReadData("Data.txt");
        Compress1(data);
        return CalculateChecksum1(data);
    }

    public static long Part2()
    {
        var data = ReadData("Data.txt");
        Compress2(data);
        return CalculateChecksum2(data);
    }

    static long CalculateChecksum1(List<int?> data)
    {
        var sum = 0L;
        for (var i = 0; data[i] != null; i++)
        {
            sum += i * data[i]!.Value;
        }
        return sum;
    }

    static void Compress1(List<int?> data)
    {
        var lastIndex = data.Count - 1;
        var nullIndex = data.IndexOf(null);
        while (lastIndex > nullIndex)
        {
            var n = data[lastIndex];
            data[lastIndex] = null;
            lastIndex--;
            data[nullIndex] = n;
            while (data[nullIndex] != null)
            {
                nullIndex++;
            }
        }
    }

    static long CalculateChecksum2(List<int?> data)
    {
        var sum = 0L;
        for (var i = 0; i < data.Count - 1; i++)
        {
            if (data[i] != null)
            {
                sum += i * data[i]!.Value;
            }
        }
        return sum;
    }

    static void Compress2(List<int?> data)
    {
        var (spans, gaps) = CalculateSpansAndGaps();

        for (var i = spans.Count-1; i >= 0; i--)
        {
            // Find first gap with room for this
            for (int j = 0; j < gaps.Count-1; j++)
            {
                if (gaps[j].Length >= spans[i].Length && gaps[j].StartIndex < spans[i].StartIndex)
                {
                    // Move it
                    var n = data[spans[i].StartIndex];
                    for (var k = 0; k < spans[i].Length; k++)
                    {
                        data[gaps[j].StartIndex + k] = n;
                        data[spans[i].StartIndex + k] = null;
                    }
                    // Remove or reduce gap
                    if (gaps[j].Length == spans[i].Length)
                    {
                        gaps.RemoveAt(j);
                    }
                    else
                    {
                        gaps[j] = (gaps[j].StartIndex + spans[i].Length, gaps[j].Length - spans[i].Length);
                    }
                    break;
                }
            }
        }


        (List<(int StartIndex, int Length)> Spans, List<(int StartIndex, int Length)> Gaps) CalculateSpansAndGaps()
        {
            var gaps = new List<(int StartIndex, int Length)>();
            var spans = new List<(int StartIndex, int Length)>();
            var startIndex = 0;
            int? current = null;
            int? previous = data[0];
            for(var i = 0; i < data.Count; i++)
            {
                current = data[i];
                if (current != previous || i == data.Count-1)
                {
                    var offset = i == data.Count - 1 ? 1 : 0;
                    if (previous is null)
                    {
                        gaps.Add((startIndex, i - startIndex + offset));
                    }
                    else 
                    {
                        spans.Add((startIndex, i - startIndex + offset));
                    }
                    startIndex = i;
                }
                previous = current;
            }
            return (spans, gaps);
        }
    }

    static List<int?> ReadData(string fileName)
    {
        var num = 0;
        var parsingNumber = true;
        var data = new List<int?>();
        foreach (var c in new DataLoader(2024, 9).ReadStrings(fileName).First())
        {
            var n = c - '0';
            if (parsingNumber)
            {
                for (var j = 0; j < n; j++)
                {
                    data.Add(num);
                }
                parsingNumber = false;
                num++;
            }
            else
            {
                for (var j = 0; j < n; j++)
                {
                    data.Add(null);
                }
                parsingNumber = true;
            }
        }
        return data;
    }
}
