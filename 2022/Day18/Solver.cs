using CSharpLib;
using CSharpLib.Algorithms;
using System.Collections.Generic;
using System.Linq;

namespace Y2022.Day18
{
    public static class Solver
    {
        public static long Part1()
        {
            var (cubes, _, _, _, _, _, _) = LoadData("data.txt");
            return cubes.Select(c => 6 - Neighbors(c, cubes).Count()).Sum();
        }

        public static long Part2()
        {
            var (cubes, minX, maxX, minY, maxY, minZ, maxZ) = LoadData("data.txt");
            EliminatePockets(cubes, minX, maxX, minY, maxY, minZ, maxZ);
            return cubes.Select(c => 6 - Neighbors(c, cubes).Count()).Sum();
        }

        private static void EliminatePockets(HashSet<(int X, int Y, int Z)> cubes, int minX, int maxX, int minY, int maxY, int minZ, int maxZ)
        {
            // All cubes withing the min and max coordinates that are not already in the cubes
            // list will be added to it if there is no path to a position outside the cube.

            var goal = (minX, minY, minZ - 1);
            for (var x = minX + 1; x < maxX; x++)
            {
                for (var y = minY + 1; y < maxY; y++)
                {
                    for (var z = minZ + 1; z < maxZ; z++)
                    {
                        if (!cubes.Contains((x, y, z)))
                        {
                            try
                            {
                                new Dijkstra<(int, int, int)>().Solve((x, y, z), goal, FindNeighbors);
                            }
                            catch
                            {
                                cubes.Add((x, y, z));
                            }
                        }
                    }
                }
            }

            IEnumerable<((int X, int Y, int Z) Pos, long Cost)> FindNeighbors((int X, int Y, int Z) pos)
            {
                // In this case, neighbors are all neighboring cells that are empty, i.e. not in cubes.
                return new List<(int X, int Y, int Z)>
                {
                    (pos.X - 1, pos.Y, pos.Z),
                    (pos.X + 1, pos.Y, pos.Z),
                    (pos.X, pos.Y - 1, pos.Z),
                    (pos.X, pos.Y + 1, pos.Z),
                    (pos.X, pos.Y, pos.Z - 1),
                    (pos.X, pos.Y, pos.Z + 1)
                }
                .Where(p => !cubes.Contains((p.X, p.Y, p.Z)))
                .Select(c => (c, 1L));
            }

        }

        private static IEnumerable<(int X, int Y, int Z)> Neighbors((int X, int Y, int Z) pos, HashSet<(int, int, int)> cubes)
        {
            return new List<(int X, int Y, int Z)>
            {
                (pos.X - 1, pos.Y, pos.Z),
                (pos.X + 1, pos.Y, pos.Z),
                (pos.X, pos.Y - 1, pos.Z),
                (pos.X, pos.Y + 1, pos.Z),
                (pos.X, pos.Y, pos.Z - 1),
                (pos.X, pos.Y, pos.Z + 1)
            }.Where(p => cubes.Contains((p.X, p.Y, p.Z)));
        }

        private static (HashSet<(int X, int Y, int Z)>, int minX, int maxX, int minY, int maxY, int minZ, int maxZ) LoadData(string fileName)
        {
            var minX = int.MaxValue;
            var maxX = -int.MaxValue;
            var minY = int.MaxValue;
            var maxY = -int.MaxValue;
            var minZ = int.MaxValue;
            var maxZ = -int.MaxValue;
            var cubes = new DataLoader(2022, 18).ReadStrings(fileName)
                .Select(s => 
                {
                    var parts = s.Split(',');
                    var x = int.Parse(parts[0]);
                    var y = int.Parse(parts[1]);
                    var z = int.Parse(parts[2]);
                    minX = x < minX ? x : minX;
                    maxX = x > maxX ? x : maxX;
                    minY = y < minY ? y : minY;
                    maxY = y > maxY ? y : maxY;
                    minZ = z < minZ ? z : minZ;
                    maxZ = z > maxZ ? z : maxZ;
                    return (x, y, z);  
                })
                .ToHashSet();
            return (cubes, minX, maxX, minY, maxY, minZ, maxZ);
        }
    }
}
