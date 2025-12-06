using CSharpLib;
using System.Collections.Generic;
using System.Linq;

namespace Y2025.Day06;

public static class Solver
{
    public static long Part1()
    {
        var data = LoadDataPart1("Data.txt");
        var lineResults = data.Select(d => d.Operator == '+' ? d.Numbers.Sum() : d.Numbers.Aggregate(1L, (x, y) => x * y));
        return lineResults.Sum();
    }

    public static long Part2()
    {
        var data = LoadDataPart2("Data.txt");
        var lineResults = data.Select(d => d.Operator == '+' ? d.Numbers.Sum() : d.Numbers.Aggregate(1L, (x, y) => x * y));
        return lineResults.Sum();
    }

    private static IEnumerable<(List<long> Numbers, char Operator)> LoadDataPart1(string fileName)
    {
        var data = LoadData(fileName);
        foreach (var (Numbers, Operator) in data)
        {
            yield return (Numbers.Select(long.Parse).ToList(), Operator);
        }
    }

    private static IEnumerable<(List<long> Numbers, char Operator)> LoadDataPart2(string fileName)
    {
        var data = LoadData(fileName);
        foreach (var (Numbers, Operator) in data)
        {
            var rotatedNumbers = Rotate(Numbers);
            yield return (rotatedNumbers.Select(long.Parse).ToList(), Operator);
        }

        static List<string> Rotate(List<string> numbers)
        {
            var len = numbers.First().Length;
            var result = new List<string>();
            for (var i = 0; i < len; i++)
            {
                result.Add("");
            }

            foreach (var number in numbers)
            {
                for (var i = 0; i < len; i++)
                {
                    if (number[i] != ' ')
                    {
                        result[i] = $"{result[i]}{number[i]}";
                    }
                }
            }
            return result;
        }
    }

    private static List<(List<string> Numbers, char Operator)> LoadData(string fileName)
    {
        var result = new List<(List<string> Numbers, char Operator)>();

        var lines = new DataLoader(2025, 6).ReadStrings(fileName).ToList();
        var columnWidths = GetColumnWidths(lines.Last());
        var operators = GetOperators(lines.Last()).ToList();

        var i = 0;
        foreach(var number in SplitLine(lines.First(), columnWidths))
        {
            result.Add((new List<string> { number }, operators[i++]));
        }

        foreach (var line in lines.Skip(1).SkipLast(1))
        {
            i = 0;
            foreach (var number in SplitLine(line, columnWidths))
            {
                result[i++].Numbers.Add(number);
            }
        }

        return result;

        static IEnumerable<string> SplitLine(string line, IEnumerable<int> columnWidths)
        {
            var acc = 0;
            foreach (var columnWidth in columnWidths)
            {
                yield return line.Substring(acc, columnWidth);
                acc += columnWidth + 1;
            }
        }

        static IEnumerable<char> GetOperators(string operatorLine)
        {
            for (var i = 0; i < operatorLine.Length; i++)
            {
                if (operatorLine[i] != ' ')
                {
                    yield return operatorLine[i];
                }
            }
        }

        static IEnumerable<int> GetColumnWidths(string operatorLine)
        {
            // In order to make this work, I ensured that the operator
            // column is one character longer than the others.
            var len = 0;
            for (var i = 1; i < operatorLine.Length; i++)
            {
                if (operatorLine[i] == ' ')
                {
                    len++;
                }
                else
                {
                    yield return len;
                    len = 0;
                }
            }
            yield return len;
        }
    }
}
