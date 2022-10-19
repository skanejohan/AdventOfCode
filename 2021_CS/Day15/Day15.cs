using CSharpLib;
using CSharpLib.DataStructures;
using CSharpLib.Extensions;
using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;
using System.Collections.Generic;

namespace _2021_CS
{
    public static class Day15
    {
        public static int Part1()
        {
            return Solve(GetData("RealData.txt")).Distance;
        }

        public static int Part2()
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
            return Solve(grid).Distance;
        }

        private static ShortestPathResult Solve(Grid<int> data)
        {
            var graph = new Graph<(int, int), string>();
            var cellIds = new Dictionary<(int, int), uint>();

            foreach (var (Row, Col, _) in data)
            {
                cellIds.Add((Row, Col), graph.AddNode((Row, Col)));
            }

            foreach (var (Row, Col, Value) in data)
            {
                var myId = cellIds[(Row, Col)];
                foreach (var n in data.GetNeighbors4(Row, Col))
                {
                    var neighborId = cellIds[(n.Row, n.Col)];
                    graph.Connect(neighborId, myId, Value, "");
                }
            }

            return graph.Dijkstra(cellIds[(0, 0)], cellIds[(data.NoOfCols() - 1, data.NoOfRows() - 1)]);
        }

        private static Grid<int> GetData(string fileName) => new Grid<int>(new DataLoader(2021, 15).ReadEnumerableInts(fileName));
    }
}
