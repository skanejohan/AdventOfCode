using CSharpLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _2021_CS
{
    public static class Day16
    {
        public static int Part1()
        {
            var parser = new BitsParser(GetBits("RealData.txt"));
            parser.Parse();
            return parser.TotalVersionNumbers;
        }

        public static long Part2()
        {
            var parser = new BitsParser(GetBits("RealData.txt"));
            parser.Parse();
            return parser.Value;
        }

        private static BitArray GetBits(string fileName) => new DataLoader(2021, 16).ReadHexStringsAsBitArrays(fileName).ToList().First();

        private class BitsParser
        {
            public BitsParser(BitArray bits)
            {
                this.bits = bits;
            }

            public long Value { get; private set; }

            public int TotalVersionNumbers { get; private set; }

            public void Parse()
            {
                pos = 0;
                TotalVersionNumbers = 0;
                Value = ParsePacket();
            }

            public long ParsePacket()
            {
                TotalVersionNumbers += ReadVersion();
                var packetType = ReadPacketType();
                if (packetType == 4) // Literal value
                {
                    return ReadBinaryNumber();
                }
                else // Operator packet
                {
                    var subValues = new List<long>();
                                                         
                    if (ReadBit() == 0) // Next 15 bits represent total length of subpackets
                    {
                        var len = ReadBits(15);
                        var readUntil = pos + len;
                        while (pos < readUntil)
                        {
                            subValues.Add(ParsePacket());
                        }
                    }
                    else // Next 11 bits represent number of sub-packets immediately contained by this packet
                    {
                        var packets = ReadBits(11);
                        for (var i = 0; i < packets; i++)
                        {
                            subValues.Add(ParsePacket());
                        }
                    }

                    switch(packetType)
                    {
                        case 0: 
                            return subValues.Sum();
                        case 1:
                            return subValues.Product();
                        case 2:
                            return subValues.Min();
                        case 3:
                            return subValues.Max();
                        case 5:
                            return subValues.First() > subValues.Skip(1).First() ? 1 : 0;
                        case 6:
                            return subValues.First() < subValues.Skip(1).First() ? 1 : 0;
                        case 7:
                            return subValues.First() == subValues.Skip(1).First() ? 1 : 0;
                        default:
                            throw new Exception("Unexpected packet type");
                    }
                }
            }

            private int ReadVersion() => ReadBits(3);

            private int ReadPacketType() => ReadBits(3);

            private long ReadBinaryNumber()
            {
                bool more;
                long result = 0;
                do
                {
                    more = ReadBool();
                    result <<= 4;
                    result += ReadBits(4);
                } while (more);
                return result;
            }

            private int ReadBits(int count)
            {
                var result = 0;
                for (var i = 0; i < count; i++)
                {
                    result <<= 1;
                    result += ReadBit();
                }
                return result;
            }

            private int ReadBit()
            {
                return ReadBool() ? 1 : 0;
            }

            private bool ReadBool()
            {
                return bits[pos++];
            }

            private readonly BitArray bits;
            private int pos;
        }
    }

    public static class EnumerableLongExtensions
    {
        public static long Product(this IEnumerable<long> elements)
        {
            long result = 1;
            foreach (var e in elements)
            {
                result *= e;
            }
            return result;
        }
    }
}
