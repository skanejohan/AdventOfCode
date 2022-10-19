using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpLib.Extensions
{
    public static class BitArrayEnumerableExtensions
    {
        /// <summary>
        /// Returns only bit arrays for which the bit at given index is set.
        /// </summary>
        public static IEnumerable<BitArray> WithBitSetAt(this IEnumerable<BitArray> bitArrays, int index)
            => bitArrays.Where(ba => ba[index]);

        /// <summary>
        /// Returns only bit arrays for which the bit at given index is not set.
        /// </summary>
        public static IEnumerable<BitArray> WithBitNotSetAt(this IEnumerable<BitArray> bitArrays, int index)
            => bitArrays.Where(ba => !ba[index]);

        /// <summary>
        /// Returns the most common value (as a bool) at given index among all the bit arrays.
        /// Returns true if the two possible values are equally common.
        /// </summary>
        public static bool MostCommonValueAt(this IEnumerable<BitArray> bitArrays, int index)
        {
            return bitArrays.WithBitSetAt(index).Count() >= bitArrays.WithBitNotSetAt(index).Count();
        }

        /// <summary>
        /// Returns only those bit arrays whose value at given index equals the most common value at that index.
        /// </summary>
        public static IEnumerable<BitArray> WithMostCommonValueAt(this IEnumerable<BitArray> bitArrays, int index)
        {
            var mostCommonValue = bitArrays.MostCommonValueAt(index);
            return bitArrays.Where(ba => ba[index] == mostCommonValue);
        }

        /// <summary>
        /// Returns only those bit arrays whose value at given index does not equal the most common value at that index.
        /// </summary>
        public static IEnumerable<BitArray> WithLeastCommonValueAt(this IEnumerable<BitArray> bitArrays, int index)
        {
            var mostCommonValue = bitArrays.MostCommonValueAt(index);
            return bitArrays.Where(ba => ba[index] != mostCommonValue);
        }

        /// <summary>
        /// Combines all bit arrays into one bit array, calculating the value at each position using the supplied aggregator.
        /// </summary>
        public static BitArray Aggregate(this IEnumerable<BitArray> bitArrays, Func<IEnumerable<BitArray>, int, bool> aggregator)
            => new BitArray(Enumerable.Range(0, bitArrays.First().Length).Select(i => aggregator(bitArrays, i)).ToArray());
    }
}
