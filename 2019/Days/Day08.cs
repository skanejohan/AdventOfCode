using AdventOfCode.Common;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public static class Day08
    {
        public static long Part1()
        {
            int zerosOnLayerWithFewestZeros = int.MaxValue;
            int onesOnLayerWithFewestZeros = 0;
            int twosOnLayerWithFewestZeros = 0;

            foreach(var layer in GetLayers())
            {
                var noOfZeros = layer.Where(b => b == 0).Count();
                if (noOfZeros < zerosOnLayerWithFewestZeros)
                {
                    zerosOnLayerWithFewestZeros = noOfZeros;
                    onesOnLayerWithFewestZeros = layer.Where(b => b == 1).Count();
                    twosOnLayerWithFewestZeros = layer.Where(b => b == 2).Count();
                }
            }

            return onesOnLayerWithFewestZeros * twosOnLayerWithFewestZeros;
        }

        public static string Part2() => new string(DecodeImage().Select(b => (char)(b + 48)).ToArray());

        private const int layerSize = 150;

        private static List<List<byte>> GetLayers()
        {
            var data = DataReader.ReadAllDigits("Day08Input.txt");
            var result = new List<List<byte>>();
            for (int i = 0; i < data.Count; i += layerSize)
            {
                result.Add(data.GetRange(i, layerSize).ToList());
            }
            return result;
        }

        private static List<byte> DecodeImage()
        {
            var result = new List<byte>(new byte[layerSize]);
            var layers = GetLayers();
            layers.Reverse();
            for (var i = 0; i < layerSize; i++)
            {
                foreach (var layer in layers)
                {
                    if (layer[i] != 2)
                    {
                        result[i] = layer[i];
                    }
                }
            }
            return result;
        }
    }
}
