using CSharpLib;
using CSharpLib.DataStructures;
using CSharpLib.Extensions;
using System;
using System.Linq;

namespace _2021_CS.Day20
{
    public static class Solver
    {
        public static long Part1()
        {
            return EnhanceN(GetData("RealData.txt"), 2).Image.Where(c => c.Value).Count();
        }

        public static long Part2()
        {
            return EnhanceN(GetData("RealData.txt"), 50).Image.Where(c => c.Value).Count();
        }

        private static (InfiniteGrid<bool> Image, bool[] Algorithm) EnhanceN((InfiniteGrid<bool>, bool[]) input, int count)
        {
            var result = input;
            for (var i = 0; i < count; i++)
            {
                result = Enhance(result, i);
            }
            return result;
        }

        private static (InfiniteGrid<bool> Image, bool[] Algorithm) Enhance((InfiniteGrid<bool> Image, bool[] Algorithm) input, int step)
        {
            var image = new InfiniteGrid<bool>();
            var bgValue = input.Algorithm[0] && step % 2 == 1;
            for (var x = input.Image.MinX - 1; x <= input.Image.MaxX + 1; x++)
            {
                for (var y = input.Image.MinY - 1; y <= input.Image.MaxY + 1; y++)
                {
                    var index = GetIndexAt(input.Image, x, y, bgValue);
                    image.Add(x, y, input.Algorithm[index]);
                }
            }
            return (image, input.Algorithm);

            static int GetIndexAt(InfiniteGrid<bool> grid, int x, int y, bool bgValue)
            {
                var index = 0;
                index += grid.Get(x - 1, y - 1, bgValue) ? 256 : 0;
                index += grid.Get(x + 0, y - 1, bgValue) ? 128 : 0;
                index += grid.Get(x + 1, y - 1, bgValue) ? 64 : 0;
                index += grid.Get(x - 1, y + 0, bgValue) ? 32 : 0;
                index += grid.Get(x + 0, y + 0, bgValue) ? 16 : 0;
                index += grid.Get(x + 1, y + 0, bgValue) ? 8 : 0;
                index += grid.Get(x - 1, y + 1, bgValue) ? 4 : 0;
                index += grid.Get(x + 0, y + 1, bgValue) ? 2 : 0;
                index += grid.Get(x + 1, y + 1, bgValue) ? 1 : 0;
                return index;
            }
        }

        private static (InfiniteGrid<bool> Image, bool[] Algorithm) GetData(string fileName)
        {
            var chunks = new DataLoader(2021, 20).ReadStrings(fileName).ChunkBy(s => s == "").ToList();
            var algorithm = chunks[0][0].Select(c => c == '#').ToArray();
            var image = new InfiniteGrid<bool>();
            for (var x = 0; x < chunks[1][0].Length; x++)
            { 
                for (var y = 0; y < chunks[1].Count; y++)
                {
                    image.Add(x, y, chunks[1][y][x] == '#');
                }
            }
            return (image, algorithm);
        }

        private static void Dump(InfiniteGrid<bool> image)
        {
            Console.WriteLine(image.ToString().Replace("True", "#").Replace("False", "."));
        }
    }
}
