namespace CSharpLib
{
    public static class ByteExtensions
    {
        // Reverse the bits in the byte (MSB <-> LSB). See http://graphics.stanford.edu/~seander/bithacks.html#ReverseByteWith64BitsDiv
        public static byte ReverseBits(this byte b)
            => (byte)((b * 0x0202020202 & 0x010884422010) % 1023);
    }
}
