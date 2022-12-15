using CSharpLib;
using CSharpLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2022.Day15
{
    public static class Solver
    {
        public static long Part1()
        {
            const int ROW = 2000000;
            data = LoadData("data.txt");
            var covered = BeaconCantBeAtX(ROW);
            var knownBeaconsOnThisRow = data.Where(d => d.B.Y == ROW).Select(d => d.B.X).ToHashSet();
            return covered.Count - knownBeaconsOnThisRow.Count;
        }

        public static long Part2()
        {
            const int MAX = 4000000;
            data = LoadData("data.txt").ToList();

            foreach (var sensor in data)
            {
                var outside = PositionsOutsideRangeOf(sensor.S, sensor.D)
                    .Where(p => p.X >= 0 && p.X <= MAX && p.Y >= 0 && p.Y <= MAX);
                foreach (var pos in outside)
                {
                    var isOutsideRange = true;
                    foreach (var otherSensor in data.Where(s => s != sensor))
                    {
                        if (GeometryUtils.ManhattanDistance(pos, otherSensor.S) <= otherSensor.D)
                        {
                            isOutsideRange = false;
                            break;
                        }
                    }
                    if (isOutsideRange)
                    {
                        return (long)pos.X * 4000000 + (long)pos.Y;
                    }
                }
            }
            return 0;
        }

        private static IEnumerable<(int X, int Y)> PositionsOutsideRangeOf((int X, int Y) sensor, int sensorRange)
        {
            var left = sensor.X - sensorRange - 1;
            for (int i = 0; i < sensorRange + 2; i++)
            {
                yield return (left + i, sensor.Y + i);
                yield return (left + i, sensor.Y - i);
            }

            var right = sensor.X + sensorRange + 1;
            for (int i = 0; i < sensorRange + 2; i++)
            {
                yield return (right - i, sensor.Y + i);
                yield return (right - i, sensor.Y - i);
            }
        }

        private static HashSet<int> BeaconCantBeAtX(int row, int? minX = null, int? maxX = null)
        {
            var covered = new HashSet<int>();
            foreach (var (S, _, D) in data)
            {
                var x = S.X;
                var dx = D - Math.Abs(S.Y - row);
                for (int i = 0; i <= dx; i++)
                {
                    if (!minX.HasValue || minX.Value <= x - i)
                    {
                        covered.Add(x - i);
                    }
                    if (!maxX.HasValue || maxX.Value >= x + i)
                    {
                        covered.Add(x + i);
                    }
                }
            }
            return covered;
        }

        private static IEnumerable<((int X, int Y) S, (int X, int Y) B, int D)> LoadData(string fileName)
        {
            foreach (var line in new DataLoader(2022, 15).ReadStrings(fileName))
            {
                var parts = line["Sensor at x=".Length..].Split(": closest beacon is at x=");
                var s = parts[0].Split(", y=");
                var sensor = (int.Parse(s[0]), int.Parse(s[1]));
                var b = parts[1].Split(", y=");
                var beacon = (int.Parse(b[0]), int.Parse(b[1]));
                yield return (sensor, beacon, GeometryUtils.ManhattanDistance(sensor, beacon));
            }
        }

        private static IEnumerable<((int X, int Y) S, (int X, int Y) B, int D)> data;
    }
}
