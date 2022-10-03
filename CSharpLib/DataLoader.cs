using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSharpLib
{
    public class DataLoader
    {
        public DataLoader(int year, int day)
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), $"..\\..\\..\\..\\{year}_CS\\Day{day:D2}\\");
        }

        public IEnumerable<string> ReadStrings(string fileName)
        {
            return File.ReadLines($"{path}\\{fileName}");
        }

        public IEnumerable<int> ReadInts(string fileName)
        {
            return ReadStrings(fileName).Select(int.Parse);
        }

        public IEnumerable<int> ReadOneLineOfInts(string fileName)
        {
            return ReadStrings(fileName).First().Split(',').Select(int.Parse);
        }

        public IEnumerable<BitArray> ReadBitArrays(string fileName, char setChar = '1')
        {
            return ReadStrings(fileName).Select(s => new BitArray(s.Select(c => c == setChar).ToArray()));
        }

        private readonly string path;
    }
}