using CSharpLib.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace CSharpLib
{
    public class DataLoader
    {
        public DataLoader(string year, int day)
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), $"..\\..\\..\\..\\{year}\\Day{day:D2}\\");
        }

        public DataLoader(int year, int day)
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), $"..\\..\\..\\..\\{year}\\Day{day:D2}\\");
        }

        public IEnumerable<string> ReadStrings(string fileName = "Data.txt")
        {
            return File.ReadLines($"{path}\\{fileName}");
        }

        public IEnumerable<IEnumerable<char>> ReadEnumerableChars(string fileName)
        {
            return File.ReadLines($"{path}\\{fileName}");
        }

        public IEnumerable<char[]> ReadCharArrays(string fileName)
        {
            return File.ReadLines($"{path}\\{fileName}").Select(a => a.ToArray());
        }

        public IEnumerable<IEnumerable<int>> ReadEnumerableInts(string fileName)
        {
            return File.ReadLines($"{path}\\{fileName}").Select(s => s.Select(c => c - '0'));
        }

        public IEnumerable<int> ReadInts(string fileName)
        {
            return ReadStrings(fileName).Select(int.Parse);
        }

        public IEnumerable<long> ReadLongs(string fileName)
        {
            return ReadStrings(fileName).Select(long.Parse);
        }

        public IEnumerable<ulong> ReadUlongs(string fileName)
        {
            return ReadStrings(fileName).Select(ulong.Parse);
        }

        public IEnumerable<BigInteger> ReadBigInts(string fileName)
        {
            return ReadStrings(fileName).Select(BigInteger.Parse);
        }

        public IEnumerable<int> ReadOneLineOfInts(string fileName)
        {
            return ReadStrings(fileName).First().Split(',').Select(int.Parse);
        }

        public IEnumerable<BitArray> ReadBitArrays(string fileName, char setChar = '1')
        {
            return ReadStrings(fileName).Select(s => new BitArray(s.Select(c => c == setChar).ToArray()));
        }

        public IEnumerable<byte[]> ReadHexStringsAsByteArrays(string fileName)
        {
            return ReadStrings(fileName).Select(s => s.ToHex());
        }

        public IEnumerable<BitArray> ReadHexStringsAsBitArrays(string fileName)
        {
            return ReadHexStringsAsByteArrays(fileName).Select(bs => new BitArray(bs.Select(b => b.ReverseBits()).ToArray()));
        }

        private readonly string path;
    }
}