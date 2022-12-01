using CSharpLib;
using CSharpLib.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _2021_CS
{
    public static class Day03
    {
        public static int Part1()
        {
            var diagnosticReport = RealData();
            var half = diagnosticReport.Count() / 2;
            var gammaRate = diagnosticReport.Aggregate((bitArrays, i) => bitArrays.WithBitSetAt(i).Count() >= half);
            var epsilonRate = new BitArray(gammaRate).Not();
            return gammaRate.Value() * epsilonRate.Value();
        }

        public static int Part2()
        {
            var diagnosticReport = RealData();
            var oxygenGeneratorRating = GetOxygenGeneratorRating(diagnosticReport);
            var co2ScrubberRating = GetCo2ScrubberRating(diagnosticReport);
            return oxygenGeneratorRating.Value() * co2ScrubberRating.Value();
        }

        private static BitArray GetOxygenGeneratorRating(IEnumerable<BitArray> bitArrays)
        {
            return GetRating(bitArrays, (arrays, index) => arrays.WithMostCommonValueAt(index));
        }

        private static BitArray GetCo2ScrubberRating(IEnumerable<BitArray> bitArrays)
        {
            return GetRating(bitArrays, (arrays, index) => arrays.WithLeastCommonValueAt(index));
        }

        private static BitArray GetRating(IEnumerable<BitArray> bitArrays, 
            Func<IEnumerable<BitArray>, int, IEnumerable<BitArray>> filterFunction)
        {
            var arrays = bitArrays;
            for (var i = 0; i < arrays.First().Length; i++)
            {
                arrays = filterFunction(arrays, i);
                if (arrays.Count() == 1)
                {
                    break;
                }
            }
            return arrays.First();
        }

        private static IEnumerable<BitArray> RealData() => new DataLoader("2021_CS", 3).ReadBitArrays("DataReal.txt");
        private static IEnumerable<BitArray> TestData() => new DataLoader("2021_CS", 3).ReadBitArrays("DataTest.txt");
    }
}
