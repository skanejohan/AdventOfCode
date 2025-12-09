using CSharpLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2025.Day08;

public static class Solver
{
    public static long Part1()
    {
        var junctions = LoadJunctionBoxes("Data.txt");
        var distances = CalculateDistances(junctions);

        var circuitSizes = new Dictionary<int, int>();
        var highestCircuitIndex = 1;
        for (var i = 0; i < 1000; i++)
        {
            var (J1, J2, _) = distances[i];
            if (J1.Circuit == J2.Circuit)
            {
                if (J1.Circuit == 0) // Neither one was in a circuit
                {
                    J1.Circuit = highestCircuitIndex;
                    J2.Circuit = highestCircuitIndex;
                    circuitSizes[highestCircuitIndex] = 2;
                    highestCircuitIndex++;
                }
            }
            else if (J1.Circuit == 0 && J2.Circuit != 0) // J2 is in a circuit, J1 is not
            {
                J1.Circuit = J2.Circuit;
                circuitSizes[J2.Circuit]++;
            }
            else if (J1.Circuit != 0 && J2.Circuit == 0) // J1 is in a circuit, J2 is not
            {
                J2.Circuit = J1.Circuit;
                circuitSizes[J1.Circuit]++;
            }
            else // They are in two different circuits
            {
                var d2Circuit = J2.Circuit;
                foreach (var j in junctions)
                {
                    if (j.Circuit == d2Circuit)
                    {
                        j.Circuit = J1.Circuit;
                        circuitSizes[d2Circuit]--;
                        circuitSizes[j.Circuit]++;
                    }
                }
            }
        }
        var sizes = circuitSizes.Values.OrderDescending().ToList();
        return sizes[0] * sizes[1] * sizes[2];
    }

    public static long Part2()
    {
        var junctions = LoadJunctionBoxes("Data.txt");
        var distances = CalculateDistances(junctions);

        var circuitSizes = new Dictionary<int, int>();
        var noOfJunctionsInCircuit = 0;
        var highestCircuitIndex = 1;
        var i = 0;
        while (noOfJunctionsInCircuit < junctions.Count)
        {
            var (J1, J2, _) = distances[i];
            if (J1.Circuit == J2.Circuit)
            {
                if (J1.Circuit == 0) // Neither one was in a circuit
                {
                    J1.Circuit = highestCircuitIndex;
                    J2.Circuit = highestCircuitIndex;
                    circuitSizes[highestCircuitIndex] = 2;
                    highestCircuitIndex++;
                    noOfJunctionsInCircuit += 2;
                }
            }
            else if (J1.Circuit == 0 && J2.Circuit != 0) // J2 is in a circuit, J1 is not
            {
                J1.Circuit = J2.Circuit;
                circuitSizes[J2.Circuit]++;
                noOfJunctionsInCircuit++;
            }
            else if (J1.Circuit != 0 && J2.Circuit == 0) // J1 is in a circuit, J2 is not
            {
                J2.Circuit = J1.Circuit;
                circuitSizes[J1.Circuit]++;
                noOfJunctionsInCircuit++;
            }
            else // They are in two different circuits
            {
                var d2Circuit = J2.Circuit;
                foreach (var j in junctions)
                {
                    if (j.Circuit == d2Circuit)
                    {
                        j.Circuit = J1.Circuit;
                        circuitSizes[d2Circuit]--;
                        circuitSizes[j.Circuit]++;
                    }
                }
            }
            i++;
        }

        return distances[i - 1].J1.X * distances[i - 1].J2.X;
    }

    private static List<JunctionBox> LoadJunctionBoxes(string fileName)
    {
        var data = new DataLoader(2025, 8).ReadStrings(fileName).Select(s => s.Split(',').Select(int.Parse).ToList()).ToList();
        return [.. data.Select(d => new JunctionBox(d[0], d[1], d[2]))];
    }

    private static List<(JunctionBox J1, JunctionBox J2, long Distance)> CalculateDistances(List<JunctionBox> junctionBoxes)
    {
        var distances = new List<(JunctionBox, JunctionBox, long)>();
        for (var i = 0; i < junctionBoxes.Count - 1; i++)
        {
            for (var j = i + 1; j < junctionBoxes.Count; j++)
            {
                var j1 = junctionBoxes[i];
                var j2 = junctionBoxes[j];
                long dX = Math.Abs(j2.X - j1.X);
                long dY = Math.Abs(j2.Y - j1.Y);
                long dZ = Math.Abs(j2.Z - j1.Z);
                long dx2 = dX * dX;
                long dy2 = dY * dY;
                var dz2 = dZ * dZ;
                long len = dX * dX + dY * dY + dZ * dZ;
                distances.Add((j1, j2, len));
            }
        }
        return [.. distances.OrderBy(d => d.Item3)];
    }

    class JunctionBox(int x, int y, int z)
    {
        public int X { get; } = x;
        public int Y { get; } = y;
        public int Z { get; } = z;
        public int Circuit { get; set; }
        public override string ToString()
        {
            return $"({X},{Y},{Z},{Circuit})";
        }
    }
}
