using AdventOfCode.Common;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days2019
{
    public static class Day07
    {
        public static long Part1() => RunSystemWithInitialPhaseSettingsBetween(0, 4, GetData());
        public static long Part2() => RunSystemWithInitialPhaseSettingsBetween(5, 9, GetData());

        private static List<long> GetData() => DataReader.ReadCommaSeparatedLongList("Day07Input.txt").ToList();

        private static long RunSystemWithInitialPhaseSettingsBetween(long low, long high, List<long> data)
        {
            long signal = 0;
            for (var i = low; i <= high; i++)
            {
                for (var j = low; j <= high; j++)
                {
                    for (var k = low; k <= high; k++)
                    {
                        for (var l = low; l <= high; l++)
                        {
                            for (var m = low; m <= high; m++)
                            {
                                var all = new List<long> { i, j, k, l, m };
                                if (all.Contains(low) && all.Contains(low + 1) && all.Contains(low + 2) &&
                                    all.Contains(low + 3) && all.Contains(low + 4))
                                {
                                    var s = RunSystemWithInitialPhaseSettings(i, j, k, l, m, data);
                                    if (s > signal)
                                    {
                                        signal = s;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return signal;
        }

        private static long RunSystemWithInitialPhaseSettings(long p1, long p2, long p3, long p4, long p5, List<long> data)
        {
            long result = 0;

            var inputsA = new List<long> { p1, 0 };
            var inputsB = new List<long> { p2 };
            var inputsC = new List<long> { p3 };
            var inputsD = new List<long> { p4 };
            var inputsE = new List<long> { p5 };

            var iccA = new IntCodeComputer(new List<long>(data),
                () =>
                {
                    var v = inputsA[0];
                    inputsA.RemoveAt(0);
                    return v;
                },
                v =>
                {
                    inputsB.Add(v);
                    return false;
                });
            var iccB = new IntCodeComputer(new List<long>(data),
                () =>
                {
                    var v = inputsB[0];
                    inputsB.RemoveAt(0);
                    return v;
                },
                v =>
                {
                    inputsC.Add(v);
                    return false;
                });
            var iccC = new IntCodeComputer(new List<long>(data),
                () =>
                {
                    var v = inputsC[0];
                    inputsC.RemoveAt(0);
                    return v;
                },
                v =>
                {
                    inputsD.Add(v);
                    return false;
                });
            var iccD = new IntCodeComputer(new List<long>(data),
                () =>
                {
                    var v = inputsD[0];
                    inputsD.RemoveAt(0);
                    return v;
                },
                v =>
                {
                    inputsE.Add(v);
                    return false;
                });
            var iccE = new IntCodeComputer(new List<long>(data),
                () =>
                {
                    var v = inputsE[0];
                    inputsE.RemoveAt(0);
                    return v;
                },
                v =>
                {
                    result = v;
                    inputsA.Add(v);
                    return false;
                });

            while (!iccE.IsDone)
            {
                iccA.Run();
                iccB.Run();
                iccC.Run();
                iccD.Run();
                iccE.Run();
            }

            return result;
        }

    }
}
