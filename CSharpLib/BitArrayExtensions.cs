using System;
using System.Collections;

namespace CSharpLib
{
    public static class BitArrayExtensions
    {
        /// <summary>
        /// Returns the int value represented by the bit array, 
        /// assuming position 0 is most significant bit
        /// </summary>
        public static int Value(this BitArray bitArray)
        {
            int value = 0;
            for (int i = 0; i < bitArray.Count; i++)
            {
                if (bitArray[i])
                {
                    value += Convert.ToInt16(Math.Pow(2, bitArray.Count-i-1));
                }
            }
            return value;
        }
    }
}
