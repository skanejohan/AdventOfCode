namespace CSharpLib.Extensions
{
    public static class IntExtensions
    {
        public static int Wrap(this int n, int around)
        {
            return (n - 1) % around + 1;
        }

        public static int WithBitSet(this int n, int bit)
        {
            return n |= 1 << bit;
        }

        public static bool IsBitSet(this int n, int bit)
        {
            return ((n >> bit) & 1) != 0;
        }

        public static int WithBitCleared(this int n, int bit)
        {
            return n & ~(1 << bit);
        }

        public static int CountBits(this int value)
        {
            int count = 0;
            while (value != 0)
            {
                count++;
                value &= value - 1;
            }
            return count;
        }
    }
}
