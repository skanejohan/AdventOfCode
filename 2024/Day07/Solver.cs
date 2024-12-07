using CSharpLib;
using System.Collections.Generic;
using System.Linq;

namespace Y2024.Day07;

public static class Solver
{
    public static long Part1()
    {
        return Solve("Data.txt", false);
    }

    public static long Part2()
    {
        return Solve("Data.txt", true);
    }

    static long Solve(string fileName, bool allowConcat)
    {
        long sum = 0;
        foreach (var (Target, Numbers) in LoadData(fileName))
        {
            if (CanReachTarget(Target, Numbers, allowConcat))
            {
                sum += Target;
            }
        }
        return sum;
    }

    static IEnumerable<(long Target, List<long> Numbers)> LoadData(string fileName)
    {
        return new DataLoader(2024, 7).ReadStrings(fileName).Select(s =>
        {
            var parts = s.Split(":");
            return (long.Parse(parts[0].Trim()), parts[1].Trim().Split(" ").Select(long.Parse).ToList());
        });
    }

    static bool CanReachTarget(long target, List<long> numbers, bool allowConcat)
    {
        bool targetFound = false;
        CanReach(0, 0);
        return targetFound;

        void CanReach(long soFar, int index)
        {
            if (soFar == target && index == numbers.Count)
            {
                targetFound = true;
                return;
            }
            if (soFar > target || index == numbers.Count)
            {
                return;
            }
            CanReach(soFar * numbers[index], index + 1);
            if (!targetFound)
            {
                CanReach(soFar + numbers[index], index + 1);
            }
            if (!targetFound && allowConcat)
            {
                CanReach(long.Parse($"{soFar}{numbers[index]}"), index + 1);
            }
        }
    }
}
