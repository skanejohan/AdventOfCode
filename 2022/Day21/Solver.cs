using CSharpLib;
using System.Collections.Generic;

namespace Y2022.Day21
{
    public static class Solver
    {
        public static ulong Part1()
        {
            LoadData("data.txt");
            return (ulong)(Evaluate("root"));
        }

        public static ulong Part2()
        {
            LoadData("data.txt");
            values["humn"] = null;
 
            var (RootOp1, _, RootOp2) = functions["root"];
            var op1 = Evaluate(RootOp1);
            var op2 = Evaluate(RootOp2);
            var target = op1 == null ? (ulong)op2 : (ulong)op1;
            var currentMonkey = op1 == null ? RootOp1 : RootOp2;
                        
            while (true)
            {
                var (Op1, Operand, Op2) = functions[currentMonkey];
                op1 = Evaluate(Op1);
                op2 = Evaluate(Op2);
                target = NewTarget(target, op1, op2, Operand);
                currentMonkey = op1 == null ? Op1 : Op2;
                if (currentMonkey == "humn")
                {
                    return target;
                }
            }

            static ulong NewTarget(ulong oldTarget, ulong? op1, ulong? op2, char operand)
            {
                if (op2 is not null)
                {
                    return operand switch
                    {
                        '+' => oldTarget - (ulong)op2,
                        '-' => oldTarget + (ulong)op2,
                        '*' => oldTarget / (ulong)op2,
                        _ => oldTarget * (ulong)op2
                    };
                }
                else if (op1 is not null)
                {
                    return operand switch
                    {
                        '+' => oldTarget - (ulong)op1,
                        '-' => (ulong)op1 - oldTarget,
                        '*' => oldTarget / (ulong)op1,
                        _ => (ulong)op1 / oldTarget
                    };
                }
                throw new System.ArgumentException();
            }
        }

        private static ulong? Evaluate(string key)
        {
            if (!values.ContainsKey(key))
            {
                var (Op1, Operand, Op2) = functions[key];
                var result1 = Evaluate(Op1);
                var result2 = Evaluate(Op2);
                if (result1 == null || result2 == null)
                {
                    return null;
                }
                else
                {
                    values[key] = Operand switch
                    {
                        '+' => Evaluate(Op1) + Evaluate(Op2),
                        '-' => Evaluate(Op1) - Evaluate(Op2),
                        '*' => Evaluate(Op1) * Evaluate(Op2),
                        _ => Evaluate(Op1) / Evaluate(Op2),
                    };
                }
            }
            return values[key];
        }

        private static Dictionary<string, ulong?> values = new();
        private static Dictionary<string, (string Op1, char Operand, string Op2)> functions = new();

        private static void LoadData(string fileName)
        {
            values.Clear();
            functions.Clear();
            foreach (var line in new DataLoader(2022, 21).ReadStrings(fileName))
            {
                var parts = line.Split(": ");
                var key = parts[0];
                if (char.IsDigit(parts[1][0]))
                {
                    values[key] = ulong.Parse(parts[1]);
                }
                else
                {
                    parts = parts[1].Split(" ");
                    functions[key] = (parts[0], parts[1][0], parts[2]);
                }
            }
        }
    }
}
