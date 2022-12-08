using CSharpLib;
using CSharpLib.DataStructures;
using CSharpLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2022.Day08
{
    public static class Solver
    {
        public static long Part1()
        {
            var visible = new HashSet<(int, int)>();
            var heightMap = new Grid<int>(LoadData("data.txt"));

            for (int r = 0; r < heightMap.NoOfRows(); r++)
            {
                var max = -1;
                for (int c = 0; c < heightMap.NoOfCols(); c++)
                {
                    if (heightMap.Get(r, c) > max)
                    {
                        visible.Add((r, c));
                        max = heightMap.Get(r, c);
                    }
                }
                max = -1;
                for (int c = heightMap.NoOfCols() - 1; c >= 0; c--)
                {
                    if (heightMap.Get(r, c) > max)
                    {
                        visible.Add((r, c));
                        max = heightMap.Get(r, c);
                    }
                }
            }
            for (int c = 0; c < heightMap.NoOfCols(); c++)
            {
                var max = -1;
                for (int r = 0; r < heightMap.NoOfRows(); r++)
                {
                    if (heightMap.Get(r, c) > max)
                    {
                        visible.Add((r, c));
                        max = heightMap.Get(r, c);
                    }
                }
                max = -1;
                for (int r = heightMap.NoOfRows()-1; r >= 0; r--)
                {
                    if (heightMap.Get(r, c) > max)
                    {
                        visible.Add((r, c));
                        max = heightMap.Get(r, c);
                    }
                }
            }
            return visible.Count;
        }

        public static long Part2()
        {
            var maxScenicScore = -1;
            var heightMap = new Grid<int>(LoadData("data.txt"));
            for (int sourceR = 0; sourceR < heightMap.NoOfRows(); sourceR++)
            {
                for (int sourceC = 0; sourceC < heightMap.NoOfCols(); sourceC++)
                {
                    var myHeight = heightMap.Get(sourceR, sourceC);
                    var canSeeUp = CanSeeCol(myHeight, sourceC, EnumerableUtils.GetRange(0, sourceR - 1).Reverse());
                    var canSeeLeft = CanSeeRow(myHeight, EnumerableUtils.GetRange(0, sourceC - 1).Reverse(), sourceR);
                    var canSeeDown = CanSeeCol(myHeight, sourceC, EnumerableUtils.GetRange(sourceR + 1, heightMap.NoOfRows() - 1));
                    var canSeeRight = CanSeeRow(myHeight, EnumerableUtils.GetRange(sourceC + 1, heightMap.NoOfCols() - 1), sourceR);
                    maxScenicScore = Math.Max(maxScenicScore, canSeeRight * canSeeDown * canSeeLeft * canSeeUp);
                }
            }

            int CanSeeCol(int myHeight, int col, IEnumerable<int> rows)
            {
                var canSee = 0;
                foreach (var r in rows)
                {
                    canSee++;
                    if (heightMap.Get(r, col) >= myHeight)
                    {
                        break;
                    }
                }
                return canSee;
            }

            int CanSeeRow(int myHeight, IEnumerable<int> cols, int row)
            {
                var canSee = 0;
                foreach (var c in cols)
                {
                    canSee++;
                    if (heightMap.Get(row, c) >= myHeight)
                    {
                        break;
                    }
                }
                return canSee;
            }

            return maxScenicScore;
        }

        private static IEnumerable<IEnumerable<int>> LoadData(string fileName)
        {
            return new DataLoader(2022, 8).ReadEnumerableInts(fileName);
        }

        private static IEnumerable<IEnumerable<int>> TestData()
        {
            yield return new List<int> { 3, 0, 3, 7, 3 };
            yield return new List<int> { 2, 5, 5, 1, 2 };
            yield return new List<int> { 6, 5, 3, 3, 2 };
            yield return new List<int> { 3, 3, 5, 4, 9 };
            yield return new List<int> { 3, 5, 3, 9, 0 };
        }
    }
}
