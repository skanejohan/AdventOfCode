using CSharpLib;
using CSharpLib.DataStructures;
using System.Collections.Generic;
using System.Linq;

namespace _2021_CS
{
    public static class Day09
    {
        public static long Part1()
        {
            var heightMap = new Grid<int>(new DataLoader(2021, 9).ReadEnumerableInts("DataReal.txt"));
            var lowPoints = GetLowPoints(heightMap);
            return lowPoints.Select(p => p.Value).Sum() + lowPoints.Count();
        }

        public static long Part2()
        {
            var heightMap = new Grid<int>(new DataLoader(2021, 9).ReadEnumerableInts("DataReal.txt"));
            var basins = GetLowPoints(heightMap).Select(p => GetBasin(heightMap, p)).Select(bs => bs.Count()).OrderBy(i => -i).ToList();
            return basins[0] * basins[1] * basins[2];
        }

        private static IEnumerable<(int Row, int Col, int Value)> GetLowPoints(Grid<int> heightMap)
        {
            foreach (var h in heightMap)
            {
                if (!heightMap.GetNeighbors4(h.Row, h.Col).Any(c => c.Value <= h.Value))
                {
                    yield return h;
                }
            }
        }

        private static IEnumerable<(int Row, int Col, int Value)> GetBasin(Grid<int> heightMap, (int Row, int Col, int Value) lowPoint)
        {
            var basin = new HashSet<(int, int, int)>();

            void AddToBasin((int Row, int Col, int Value) point)
            {
                if (basin.Contains(point) || point.Value == 9)
                {
                    return;    
                }
                basin.Add(point);
                foreach (var p in heightMap.GetNeighbors4(point.Row, point.Col).ToList())
                {
                    AddToBasin(p);
                }
            }

            AddToBasin(lowPoint);
            return basin;
        }
    }
}
