using CSharpLib;
using CSharpLib.Algorithms;
using CSharpLib.DataStructures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Y2022.Day12
{
    public static class Solver
    {
        public static long Part1()
        {
            LoadData("data.txt");
            return new Dijkstra<(int, int)>().Solve(start, goal, FindNeighbors);
        }

        public static long Part2()
        {
            LoadData("data.txt");
            var minDistance = long.MaxValue;
            foreach (var startingPoint in map.Find('a'))
            {
                try
                {
                    var distance = new Dijkstra<(int, int)>().Solve(startingPoint, goal, FindNeighbors);
                    minDistance = Math.Min(distance, minDistance);
                }
                catch
                { 
                    // No path found
                }
            }
            return minDistance;
        }

        private static IEnumerable<((int Row, int Col) Pos, long Cost)> FindNeighbors((int R, int C) pos)
        {
            return map
                .GetNeighbors4(pos.R, pos.C)
                .Where(n => heightAt((n.Row, n.Col)) <= heightAt(pos) + 1)
                .Select(n => ((n.Row, n.Col), 1L));
        }

        private static int heightAt((int R, int C) p)
        {
            return map.Get(p.R, p.C) - 'a';
        }

        private static void LoadData(string fileName)
        {
            map = new Grid<char>(new DataLoader(2022, 12).ReadEnumerableChars(fileName));
            start = map.Find('S').Single();
            map.Set(start.C, start.R, 'a');
            goal = map.Find('E').Single();
            map.Set(goal.C, goal.R, 'z');
        }

        private static Grid<char> map;
        private static (int C, int R) start;
        private static (int C, int R) goal;
    }
}
