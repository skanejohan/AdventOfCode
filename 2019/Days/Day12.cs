using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;

namespace AdventOfCode.Days
{
    class Day12
    {
        public static long Part1()
        {
            LoadData();
            for (int i = 0; i < 1000; i++)
            {
                UpdateVelocities();
                ApplyVelocities();
            }
            return TotalEnergy();
        }

        public static long Part2()
        {
            // Figure out when the (x and vx), (y and vy), and (z and vz) states repeat themselves 
            // individually. The result is the least common multiple of the number of steps taken 
            // for each one.
            LoadData();

            int counter = 0;
            int xRepeatedAfter = -1;
            int yRepeatedAfter = -1;
            int zRepeatedAfter = -1;
            string xStates = currentStatesX();
            string yStates = currentStatesY();
            string zStates = currentStatesZ();

            while (true)
            {
                UpdateVelocities();
                ApplyVelocities();
                counter++;
                if (xRepeatedAfter == -1 && xStates == currentStatesX())
                {
                    xRepeatedAfter = counter;
                }
                if (yRepeatedAfter == -1 && yStates == currentStatesY())
                {
                    yRepeatedAfter = counter;
                }
                if (zRepeatedAfter == -1 && zStates == currentStatesZ())
                {
                    zRepeatedAfter = counter;
                }
                if (xRepeatedAfter != -1 && yRepeatedAfter != -1 && zRepeatedAfter != -1)
                {
                    break;
                }
            }

            return Utils.Lcm(xRepeatedAfter, Utils.Lcm(yRepeatedAfter, zRepeatedAfter));
        }

        private static List<(int x, int y, int z, int vx, int vy, int vz)> moons;

        private static string currentStatesX() => string.Join(",", moons.Select(m => $"{m.x},{m.vx}"));
        private static string currentStatesY() => string.Join(",", moons.Select(m => $"{m.y},{m.vy}"));
        private static string currentStatesZ() => string.Join(",", moons.Select(m => $"{m.z},{m.vz}"));

        private static long TotalEnergy() => moons.Select(Energy).Sum();

        private static int Energy((int x, int y, int z, int vx, int vy, int vz) moon)
        {
            var pot = Math.Abs(moon.x) + Math.Abs(moon.y) + Math.Abs(moon.z);
            var kin = Math.Abs(moon.vx) + Math.Abs(moon.vy) + Math.Abs(moon.vz);
            return pot * kin;
        }

        private static void ApplyVelocities()
        {
            moons = moons.Select(m => (m.x + m.vx, m.y + m.vy, m.z + m.vz, m.vx, m.vy, m.vz)).ToList();
        }

        private static void UpdateVelocities()
        {
            var newMoons = new List<(int x, int y, int z, int vx, int vy, int vz)>();
            for (int i = 0; i < moons.Count; i++)
            {
                newMoons.Add(GetMoonWithUpdatedVelocities(i));
            }
            moons = newMoons;
        }

        private static (int,int,int,int,int,int) GetMoonWithUpdatedVelocities(int moonIndex)
        {
            var (x, y, z, vx, vy, vz) = moons[moonIndex];
            for (var i = 0; i < moons.Count; i++)
            {
                if (i != moonIndex)
                {
                    vx = vx + (x < moons[i].x ? 1 : (x > moons[i].x ? -1 : 0));
                    vy = vy + (y < moons[i].y ? 1 : (y > moons[i].y ? -1 : 0));
                    vz = vz + (z < moons[i].z ? 1 : (z > moons[i].z ? -1 : 0));
                }
            }
            return (x, y, z, vx, vy, vz);
        }

        private static void PresentMoons()
        {
            foreach (var moon in moons)
            {
                Console.WriteLine($"({moon.x},{moon.y},{moon.z}) - {moon.vx},{moon.vy},{moon.vz}");
            }
        }

        private static void LoadData()
        {
            moons = new List<(int, int, int, int, int, int)>
            {
                (-4, -14, 8, 0, 0, 0), 
                (1, -8, 10, 0, 0, 0), 
                (-15, 2, 1, 0, 0, 0), 
                (-17, -17, 16, 0, 0, 0)
            };
        }
    }
}
