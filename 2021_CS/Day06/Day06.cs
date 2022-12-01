using CSharpLib;
using System.Linq;

namespace _2021_CS
{
    public class Day06
    {
        public static long Part1()
        {
            return Step(RealData(), 80).Sum();
        }

        public static long Part2()
        {
            return Step(RealData(), 256).Sum();
        }

        private static long[] RealData()
        {
            long[] data = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (var fish in new DataLoader("2021_CS", 6).ReadOneLineOfInts("DataReal.txt"))
            {
                data[fish] = data[fish] + 1;
            }
            return data;
        }

        private static long[] TestData()
        {
            long[] data = { 0, 1, 1, 2, 1, 0, 0, 0, 0 };
            return data;
        }

        private static long[] Step(long[] data)
        {
            long[] newData = { data[1], data[2], data[3], data[4], data[5], data[6], data[0] + data[7], data[8], data[0] };
            return newData;
        }

        private static long[] Step(long[] data, int count)
        {
            var d = data;
            for (var i = 0; i < count; i++)
            {
                d = Step(d);
            }
            return d;
        }
    }
}
