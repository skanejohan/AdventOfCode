using CSharpLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2022.Day10
{
    public static class Solver
    {
        public static long Part1()
        {
            var sum = 0;
            foreach (var (PC, X) in Execute(LoadData("data.txt")))
            {
                if ((PC + 20) % 40 == 0)
                {
                    sum += PC * X;
                }
            }
            return sum;
        }

        public static string Part2()
        {
            var crt = new List<char>();
            var crtCtr = 0;
            foreach (var (PC, X) in Execute(LoadData("data.txt")))
            {
                crt.Add(Math.Abs(X - crtCtr) < 2 ? '#' : '.');
                crtCtr++;
                if (crtCtr == 40)
                { 
                    crtCtr = 0;
                }
            }
            var display = new string(crt.ToArray());

            // Uncomment the following lines to see "BPJAZGAP"
            //Console.WriteLine(display[0..40]);
            //Console.WriteLine(display[40..80]);
            //Console.WriteLine(display[80..120]);
            //Console.WriteLine(display[120..160]);
            //Console.WriteLine(display[160..200]);
            //Console.WriteLine(display[200..240]);
            return "BPJAZGAP";
        }

        private static IEnumerable<(int PC, int X)> Execute(List<(string Command, int Cycles, int Parameter)> commands)
        {
            var x = 1;
            var pc = 1;
            var currentCommand = 0;
            var lastCommandExecutedAt = 1;
            yield return (pc, x);
            while (currentCommand < commands.Count)
            {
                pc++;
                if (lastCommandExecutedAt + commands[currentCommand].Cycles == pc)
                {
                    if (commands[currentCommand].Command == "addx")
                    {
                        x += commands[currentCommand].Parameter;
                    }
                    lastCommandExecutedAt = pc;
                    currentCommand++;
                }
                yield return (pc, x);
            }
        }

        private static List<(string Command, int Cycles, int Parameter)> LoadData(string fileName)
        {
            return new DataLoader(2022, 10).ReadStrings(fileName)
                .Select(s => s == "noop" ? ("noop", 1, 0) : ("addx", 2, int.Parse(s[5..])))
                .ToList();
        }
    }
}
