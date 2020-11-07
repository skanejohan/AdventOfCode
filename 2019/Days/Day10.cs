using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    internal static class Day10
    {
        public static long Part1() 
            => NoOfCellsVisibleFrom(FindBestPosition());

        public static long Part2()
        {
            int removedCounter = 0;
            var laserAsteroid = FindBestPosition();
            var asteroids = AllCoordinates().Where(Occupied).ToList();
            while (asteroids.Count > 1) // 1 because it contains the station cell, which will never be removed
            {
                var visibleAsteroids = asteroids.Where(c => CanSee(c, laserAsteroid)).ToList();
                while (visibleAsteroids.Count > 0)
                {
                    var remove = FindVisibleAsteroidWithSmallestAngle(visibleAsteroids, laserAsteroid);
                    asteroids.Remove(remove);
                    visibleAsteroids.Remove(remove);
                    removedCounter++;
                    if (removedCounter == 200)
                    {
                        return remove.Item1 * 100 + remove.Item2;
                    }
                }
            }
            return 0;
        }

        static Day10()
        {
            data = DataReader.ReadStrings("Day10Input.txt").ToList();
        }

        private static List<string> data;

        private static (int,int) FindVisibleAsteroidWithSmallestAngle(List<(int,int)> asteroids, (int,int) laserAsteroid)
        {
            var smallestAngle = 1000.0;
            (int,int) result = (0, 0);
            foreach (var a in asteroids)
            {
                var angle = Math.PI - Math.Atan2(a.Item1 - laserAsteroid.Item1, a.Item2 - laserAsteroid.Item2);
                if (angle < smallestAngle)
                {
                    smallestAngle = angle;
                    result = a;
                }
            }
            return result;
        }

        private static (int, int) FindBestPosition()
            => AllCoordinates().Where(Occupied).OrderBy(NoOfCellsVisibleFrom).Last();

        private static int NoOfCellsVisibleFrom((int, int) from)
            => CellsVisibleFrom(from).Count();

        private static IEnumerable<(int,int)> CellsVisibleFrom((int,int) from)
            => AllCoordinates().Where(Occupied).Where(c => CanSee(c, from));

        private static bool CanSee((int, int) target, (int, int) from)
            => target != from && !CellsBetween(from, target).Any(Occupied);

        private static bool Occupied((int,int) cell) => data[cell.Item2][cell.Item1] == '#';

        private static IEnumerable<(int,int)>AllCoordinates()
        {
            for (var y = 0; y < data.Count; y++)
            {
                for (var x = 0; x < data[0].Length; x++)
                {
                    yield return (x, y);
                }
            }
        }

        private static IEnumerable<(int, int)> CellsBetween((int, int) cell1, (int, int) cell2)
        {
            int x1 = cell1.Item1, y1 = cell1.Item2;
            int x2 = cell2.Item1, y2 = cell2.Item2;
            var startX = Math.Min(x1, x2);
            var startY = Math.Min(y1, y2);
            var endX = Math.Max(x1, x2);
            var endY = Math.Max(y1, y2);

            if (startX == endX)
            {
                for (var y = startY + 1; y < endY; y++)
                {
                    yield return (startX, y);
                }
            }
            else if (startY == endY)
            {
                for (var x = startX + 1; x < endX; x++)
                {
                    yield return (x, startY);
                }
            }
            else
            {
                var tan = Math.Atan2(y2 - y1, x2 - x1);
                for (var x = startX + 1; x != endX; x++)
                {
                    for (var y = startY + 1; y != endY; y++)
                    {
                        if (Math.Abs(Math.Atan2(y - y1, x - x1) - tan) < 0.0001)
                        {
                            yield return (x, y);
                        }
                    }
                }
            }
        }
    }
}
