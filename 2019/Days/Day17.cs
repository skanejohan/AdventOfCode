using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Days
{
    public static class Day17
    {
        public static long Part1()
        {
            long result = 0;
            for (var y = 1; y < area.Count-1; y++)
            {
                for (var x = 1; x < area[y].Length-1; x++)
                {
                    if (At(x, y) == '#' && At(x-1, y) == '#' && At(x, y-1) == '#' && At(x + 1, y) == '#' && At(x, y+1) == '#')
                    {
                        result += x * y;
                    }
                }
            }
            return result;
        }

        public static long Part2()
        {
            // Calculate the turns that must ba taken to cover all the scaffolding
            var turns = new List<string>();
            (int x, int y) previousMovement = (0, -1);
            var pos = GetInitialRobotPosition();
            (int x, int y) movement;
            while(true)
            {
                int movementCount = 0;
                movement = GetScaffoldingDirection(pos, previousMovement);
                if (movement == (0, 0))
                {
                    break;
                }
                while (At(pos.x + movement.x, pos.y + movement.y) == '#')
                {
                    movementCount++;
                    pos = (pos.x + movement.x, pos.y + movement.y);
                }
                turns.Add($"{Turn(previousMovement, movement)},{movementCount}");
                previousMovement = movement;
            }

            // To see the turns, use: Console.WriteLine(string.Join(",", turns))
            //
            // This gives turns:
            // L,10,R,8,R,6,R,10,L,12,R,8,L,12,L,10,R,8,R,6,R,10,L,12,R,8,L,12,L,10,R,8,R,8,L,10,R,8,R,8,L,12,R,8,L,12,L,10,R,8,R,6,R,10,L,10,R,8,R,8,L,10,R,8,R,6,R,10
            //
            // I decide on A = L,10,R,8,R,6,R,10
            // A,L12,R8,L12,A,L12,R8,L12,L10,R8,R8,L10,R8,R8,L12,R8,L12,A,L10,R8,R8,A
            // 
            // I decide on B = L10,R8,R8
            // A,L12,R8,L12,A,L12,R8,L12,B,B,L12,R8,L12,A,B,A
            //
            // I decide on C = L12,R8,L12
            // A,C,A,C,B,B,C,A,B,A

            // Now run the program again, telling the vacuum droid how to move
            long result = 0;
            var data = GetData();
            data[0] = 2;
            var inputIndex = 0;
            var inputString = "A,C,A,C,B,B,C,A,B,A\nL,10,R,8,R,6,R,10\nL,10,R,8,R,8\nL,12,R,8,L,12\nn\n";
            var inputs = Encoding.ASCII.GetBytes(inputString).Select(b => (long)b).ToList();
            var icc = new IntCodeComputer(data, 
                () => inputs[inputIndex++],
                v => 
                {
                    result = v;
                    return true;
                }
            );
            icc.Run();

            return result;
        }

        private static string Turn((int x, int y) movement1, (int x, int y) movement2)
        {
            if (movement1 == (0, -1) && movement2 == (-1, 0) ||
                movement1 == (1, 0) && movement2 == (0, -1) ||
                movement1 == (0, 1) && movement2 == (1, 0) ||
                movement1 == (-1, 0) && movement2 == (0, 1))
            {
                return "L";
            }
            return "R";
        }

        private static (int x, int y) GetInitialRobotPosition()
        {
            for (var y = 0; y < area.Count; y++)
            {
                for (var x = 0; x < area[y].Length; x++)
                {
                    if (area[y][x] == '^')
                    {
                        return (x, y);
                    }
                }
            }
            return (0, 0);
        }

        private static (int x, int y) GetScaffoldingDirection((int x, int y)position, (int x, int y) incomingMovement)
        {
            if (At(position.x - 1, position.y) == '#' && incomingMovement != (1, 0)) return (-1, 0);
            if (At(position.x, position.y - 1) == '#' && incomingMovement != (0, 1)) return (0, -1);
            if (At(position.x + 1, position.y) == '#' && incomingMovement != (-1, 0)) return (1, 0);
            if (At(position.x, position.y + 1) == '#' && incomingMovement != (0, -1)) return (0, 1);
            return (0, 0);
        }

        private static char At(int x, int y)
        {
            try
            {
                return area[y][x];
            }
            catch
            {
                return ' ';
            }
        }

        private static List<string> area = CalculateArea();

        private static List<string> CalculateArea()
        {
            var line = "";
            var area = new List<string>();
            var icc = new IntCodeComputer(GetData(), null,
                v =>
                {
                    if (v == 10)
                    {
                        area.Add(line);
                        line = "";
                    }
                    else
                    {
                        line += (char)v;
                    }
                    return true;
                });
            icc.Run();
            return area;
        }

        private static List<long> GetData() => DataReader.ReadCommaSeparatedLongList("Day17Input.txt").ToList();
    }
}
