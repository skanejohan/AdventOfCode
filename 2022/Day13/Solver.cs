using CSharpLib;
using CSharpLib.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Y2022.Day13
{
    public static class Solver
    {
        public static long Part1()
        {
            var sum = 0;
            var index = 1;
            foreach (var sl in LoadData("data.txt").ChunkBy(s => s == ""))
            {
                var v1 = new ValueParser(sl[0]).Parse();
                var v2 = new ValueParser(sl[1]).Parse();
                var res = v1.CompareTo(v2);
                if (res <= 0)
                {
                    sum += index;
                }
                index++;
            }
            return sum;
        }

        public static long Part2()
        {
            const string DIV1 = "[[2]]";
            const string DIV2 = "[[6]]";

            var lines = LoadData("data.txt").Where(s => s != "").ToList();
            lines.Add(DIV1);
            lines.Add(DIV2);

            var packets = lines
                .Select(l => new ValueParser(l).Parse())
                .OrderBy(p => p)
                .Select(p => p.ToString())
                .ToList();

            return (packets.IndexOf(DIV1) + 1) * (packets.IndexOf(DIV2) + 1);
        }
       
        private static IEnumerable<string> LoadData(string fileName)
        {
            return new DataLoader(2022, 13).ReadStrings(fileName);
        }
    }
}
