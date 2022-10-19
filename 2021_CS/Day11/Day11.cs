using CSharpLib;
using CSharpLib.DataStructures;
using System.Collections.Generic;
using System.Linq;

namespace _2021_CS
{
    public static class Day11
    {
        public static long Part1()
        {
            var energyLevels = new Grid<int>(new DataLoader(2021, 11).ReadEnumerableInts("DataReal.txt"));
            var flashed = 0;
            for (var i = 0; i < 100; i++)
            {
                flashed += Step(energyLevels);
            }
            return flashed;
        }

        public static long Part2()
        {
            var energyLevels = new Grid<int>(new DataLoader(2021, 11).ReadEnumerableInts("DataReal.txt"));
            var steps = 0;
            while (true)
            {
                steps++;
                var flashed = Step(energyLevels);
                if (flashed == energyLevels.NoOfRows() * energyLevels.NoOfCols())
                {
                    return steps;
                }
            }
        }

        private static int Step(Grid<int> energyLevels)
        {
            var flashed = new HashSet<(int, int)>();

            foreach (var (Row, Col, Value) in energyLevels)
            {
                energyLevels.Set(Row, Col, Value + 1);
            }
            Flash();
            foreach (var (Row, Col, Value) in energyLevels.Where(x => x.Value > 9))
            {
                energyLevels.Set(Row, Col, 0);
            }
            return flashed.Count();

            void Flash()
            {
                var flashing = energyLevels.Where(x => x.Value > 9 && !flashed.Contains((x.Row, x.Col)));
                foreach (var f in flashing) 
                {
                    flashed.Add((f.Row, f.Col));
                    foreach (var (Row, Col, Value) in energyLevels.GetNeighbors8(f.Row, f.Col))
                    {
                        energyLevels.Set(Row, Col, Value + 1);
                    }
                    Flash();
                }
            }
        }
    }
}
