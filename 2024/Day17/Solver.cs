using System;
using System.Collections.Generic;
using System.Linq;

namespace Y2024.Day17;

public static class Solver
{
    public static string Part1()
    {
        var output = Run(37293246, 0, 0, [2, 4, 1, 6, 7, 5, 4, 4, 1, 7, 0, 3, 5, 5, 3, 0]);
        return string.Join(',', output);
    }

    public static long Part2()
    {
        List<int> input = [2, 4, 1, 6, 7, 5, 4, 4, 1, 7, 0, 3, 5, 5, 3, 0];

        var a = 1L;
        for (var n = 1; n <= 16; n++)
        {
            FindNumber(ref a, n);
            if (n < 16)
            {
                a *= 8;
            }
        }
        return a;

        void FindNumber(ref long a, int noOfPositionsInOutput)
        {
            var target = input.TakeLast(noOfPositionsInOutput).ToList();
            while (true)
            {
                var output = Run(a, 0, 0, input);
                if (output.Count >= noOfPositionsInOutput)
                {
                    var found = true;
                    for (int i = 0; i < noOfPositionsInOutput; i++)
                    {
                        if (output[i] != target[i])
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found)
                    {
                        break;
                    }
                }
                a++;
            }
        }
    }

    static void RunTests()
    {
        printResults = true;
        Run(0, 0, 9, [2, 6]);
        Run(10, 0, 0, [5, 0, 5, 1, 5, 4]);
        Run(2024, 0, 0, [0, 1, 5, 4, 3, 0]);
        Run(0, 29, 0, [1, 7]);
        Run(0, 2024, 43690, [4, 0]);
        Run(729, 0, 0, [0, 1, 5, 4, 3, 0]);
        printResults = false;
    }

    static List<int> Run(long a, long b, long c, List<int> program)
    {
        var ic = 0;
        var output = new List<int>();

        var functions = new List<Action<int>> { Adv, Bxl, Bst, Jnz, Bxc, Out, Bdv, Cdv };

        while (ic < program.Count)
        {
            var operation = program[ic];
            var operand = program[ic+1];
            functions[operation](operand);
        }

        if (printResults)
        {
            Console.WriteLine($"A:      {a}");
            Console.WriteLine($"B:      {b}");
            Console.WriteLine($"C:      {c}");
            Console.WriteLine($"Output: {string.Join(',', output)}");
            Console.WriteLine();
        }

        return output;

        long ComboOperandValue(int operand)
        {
            return operand switch
            {
                4 => a,
                5 => b,
                6 => c,
                _ => operand
            };
        }

        void Adv(int operand)
        {
            var num = a;
            var den = (int)Math.Pow(2, ComboOperandValue(operand));
            a = num / den;
            ic += 2;
        }

        void Bxl(int operand)
        {
            b ^= operand;
            ic += 2;
        }

        void Bst(int operand)
        {
            b = (int)(ComboOperandValue(operand) % 8);
            ic += 2;
        }

        void Jnz(int operand)
        {
            ic = a == 0 ? ic + 2 : operand;
        }

        void Bxc(int operand)
        {
            b ^= c;
            ic += 2;
        }
        
        void Out(int operand)
        {
            output.Add((int)(ComboOperandValue(operand) % 8));
            ic += 2;
        }

        void Bdv(int operand)
        {
            var num = a;
            var den = (int)Math.Pow(2, ComboOperandValue(operand));
            b = num / den;
            ic += 2;
        }

        void Cdv(int operand)
        {
            var num = a;
            var den = (int)Math.Pow(2, ComboOperandValue(operand));
            c = num / den;
            ic += 2;
        }
    }

    static bool printResults;
}
