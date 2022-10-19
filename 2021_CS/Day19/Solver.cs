using CSharpLib;
using CSharpLib.Extensions;
using CSharpLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021_CS.Day19
{
    public static class Solver
    {
        public static long Part1()
        {
            var scanners = GetScanners("RealData.txt").ToList();
            return GetAlignedBeacons(scanners).Count();
        }

        public static long Part2()
        {
            var scanners = GetScanners("RealData.txt").ToList();
            var beacons = GetAlignedBeacons(scanners).ToList();
            var relativePositions = scanners.Select(s => s.RelativePosition).ToList();

            var result = 0;
            for (var i = 0; i < relativePositions.Count - 1; i++)
            {
                for (var j = i + 1; j < relativePositions.Count; j++)
                {
                    result = Math.Max(result, GeometryUtils.ManhattanDistance(relativePositions[i], relativePositions[j]));
                }
            }
            return result;
        }

        private static IEnumerable<(int X, int Y, int Z)> GetAlignedBeacons(List<Scanner> scanners)
        {
            scanners[0].AlignedBeaconSet = scanners[0].AllBeaconSets[0];
            var beacons = new HashSet<(int, int, int)>(scanners[0].AlignedBeaconSet);
            var stack = new Stack<Scanner>();
            stack.Push(scanners[0]);
            while (stack.Count > 0)
            {
                var scanner = stack.Pop();
                foreach (var s in scanners)
                {
                    if (s.TryToAlignWith(scanner))
                    {
                        beacons.UnionWith(s.AlignedBeaconSet);
                        stack.Push(s);
                    }
                }

            }
            return beacons;
        }

        private static IEnumerable<Scanner> GetScanners(string fileName)
        {
            var scannerId = 0; ;
            return new DataLoader(2021, 19)
                .ReadStrings(fileName).ChunkBy(s => s == "")
                .Select(lines => lines.Skip(1).Select(ParseCoordinates))
                .Select(beacons => new Scanner(scannerId++, beacons));

            static (int, int, int) ParseCoordinates(string s)
            {
                var coord = s.Split(',').Select(int.Parse).ToList();
                return (coord[0], coord[1], coord[2]);
            }
        }
    }
}
