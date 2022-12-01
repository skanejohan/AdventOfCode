using CSharpLib;
using CSharpLib.DataStructures;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2021_CS.Day22
{
    public static class Solver
    {
        public static long Part1()
        {
            var cubes = ReadInput("RealData.txt").Where(x => IsInside(x.Box)).ToList();
            return CountLitCubes(cubes);

            static bool IsInside(Box box)
            {
                return box.MinX >= -50 && box.MinX <= 50
                    && box.MaxX >= -50 && box.MaxX <= 50
                    && box.MinY >= -50 && box.MinY <= 50
                    && box.MaxY >= -50 && box.MaxY <= 50
                    && box.MinZ >= -50 && box.MinZ <= 50
                    && box.MaxZ >= -50 && box.MaxZ <= 50;
            }
        }

        public static long Part2()
        {
            var cubes = ReadInput("RealData.txt").ToList();
            return CountLitCubes(cubes);
        }

        static long CountLitCubes(IEnumerable<(bool On, Box Box)> cubes)
        {
            if (cubes.Count() < 1)
            {
                return 0;
            }

            var first = cubes.First();
            var rest = cubes.Skip(1);
            if (first.On)
            {
                var remainingBoxes = rest.Select(c => c.Box);
                var overlaps = first.Box.Intersections(remainingBoxes);
                return first.Box.Volume() + CountLitCubes(rest) - Box.TotalVolume(overlaps);
            }
            else
            {
                return CountLitCubes(rest);
            }
        }

        static IEnumerable<(bool On, Box Box)> ReadInput(string fileName)
        {
            return new DataLoader("2021_CS", 22).ReadStrings(fileName).Select(ParseLine);

            static (bool, Box) ParseLine(string s)
            {
                string[] result = new Regex("([^=]+) x=([^.]*)..([^,]*),y=([^.]*)..([^,]*),z=([^.]*)..(.*)").Split(s);
                var on = result[1] == "on";
                var box = new Box(
                    int.Parse(result[2]), int.Parse(result[4]), int.Parse(result[6]),
                    int.Parse(result[3]), int.Parse(result[5]), int.Parse(result[7]));
                return (on, box);
            }
        }
    }
}
