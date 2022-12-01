using CSharpLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021_CS
{
    public static class Day02
    {
        public static int Part1()
        {
            var hPos = 0;
            var depth = 0;
            var functions = new Dictionary<Movement, Action<int>>
            {
                { Movement.Forward, n => hPos += n },
                { Movement.Down, n => depth += n },
                { Movement.Up, n => depth -= n },
                { Movement.None, _ => { } },
            };
            foreach (var step in Data())
            {
                functions[step.Movement](step.Distance);
            }
            return hPos * depth;
        }

        public static int Part2()
        {
            var aim = 0;
            var hPos = 0;
            var depth = 0;
            var functions = new Dictionary<Movement, Action<int>>
            {
                { Movement.Forward, n => {hPos += n; depth += aim * n; } },
                { Movement.Down, n => aim += n },
                { Movement.Up, n => aim -= n },
                { Movement.None, _ => { } },
            };
            foreach (var step in Data())
            {
                functions[step.Movement](step.Distance);
            }
            return hPos * depth;
        }

        enum Movement { Forward, Down, Up, None }

        private static IEnumerable<(Movement Movement, int Distance)> Data() => 
            new DataLoader("2021_CS", 2).ReadStrings("DataReal.txt").Select(ParseLine);

        private static (Movement, int) ParseLine(string s)
        {
            string[] parts = s.Split(" ");
            var n = int.Parse(parts[1]);
            return parts[0] switch
            {
                "forward" => (Movement.Forward, n),
                "down" => (Movement.Down, n),
                "up" => (Movement.Up, n),
                _ => (Movement.None, 0)
            };
        }
    }
}
