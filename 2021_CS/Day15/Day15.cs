using CSharpLib;
using CSharpLib.Algorithms;
using CSharpLib.DataStructures;
using CSharpLib.Extensions;
using System.Collections.Generic;

namespace _2021_CS
{
    public static class Day15
    {
        public static long Part1()
        {
            return Solve(GetData("RealData.txt"));
        }

        public static long Part2()
        {
            var data = GetData("RealData.txt");
            var grid = new Grid<int>(data.NoOfRows() * 5, data.NoOfCols() * 5);

            for (int rowOffset = 0; rowOffset < 5; rowOffset++)
            {
                for (int colOffset = 0; colOffset < 5; colOffset++)
                {
                    foreach (var (Row, Col, Value) in data)
                    {
                        var value = (Value + colOffset + rowOffset).Wrap(9);
                        grid.Set(rowOffset * data.NoOfRows() + Row, colOffset * data.NoOfCols() + Col, value);
                    }
                }
            }
            return Solve(grid);
        }

        private static long Solve(Grid<int> data)
        {
            var start = (0, 0);
            var goal = (data.NoOfCols()-1, data.NoOfRows()-1);

            IEnumerable<((int, int), long)> transformer((int Row, int Col) cell)
            {
                foreach (var (Row, Col, Value) in data.GetNeighbors4(cell.Row, cell.Col))
                {
                    yield return ((Row, Col), Value);
                }
            }

            return new DijkstraV1<(int, int)>().Solve(start, goal, transformer);
        }

        private static Grid<int> GetData(string fileName) => new Grid<int>(new DataLoader("2021_CS", 15).ReadEnumerableInts(fileName));
    }
}
