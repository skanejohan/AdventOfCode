using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days2019
{
    public static class Day16
    {
        public static string Part1()
        {
            IEnumerable<byte> data = LoadData();
            for (int i = 0; i < 100; i++)
            {
                data = ProcessPhase(data.ToList());
            }
            return string.Join("", data.Take(8));
        }

        public static long Part2()
        {
            // see https://www.reddit.com/r/adventofcode/comments/ebf5cy/2019_day_16_part_2_understanding_how_to_come_up/

            IEnumerable<byte> data = LoadData(); //650 bytes
            var messageOffset = BytesToInt(data.Take(7).ToList()); // 5974057
            var result = Enumerable.Repeat(data, 10000).SelectMany(x => x).Skip(messageOffset).ToList();
            for (int steps = 0; steps < 100; steps++)
            {
                byte previous = 0;
                for (int i = result.Count - 1; i >= 0; i--)
                {
                    result[i] = (byte)((result[i] + previous) % 10);
                    previous = result[i];
                }
            }
            return BytesToInt(result.Take(8).ToList());
        }

        private static int BytesToInt(List<byte> bytes)
        {
            int result = 0;
            byte count = (byte)(bytes.Count - 1);
            for (byte i = 0; i <= count; i++)
            {
                result += (int)(bytes[i] * Math.Pow(10, count-i));
            }
            return result;
        }

        private static IEnumerable<long> GetPattern(int repeatCount, int minTotalLength)
        {
            var basePattern = new List<long> { 0, 1, 0, -1 };
            var pattern = basePattern.SelectMany(n => Enumerable.Repeat((long)n, repeatCount));
            var patternRepeats = (int)Math.Ceiling((double)minTotalLength / (repeatCount * basePattern.Count));
            return Enumerable.Repeat(pattern, patternRepeats).SelectMany(x => x);
        }

        private static IEnumerable<byte> ProcessPhase(List<byte> input)
        {
            foreach(var index in Enumerable.Range(1, input.Count))
            {
                long sum = 0;
                var pattern = GetPattern(index, input.Count).Skip(1).ToList();
                foreach (var j in Enumerable.Range(0, input.Count-1))
                {
                    sum += input[j] * pattern[j];
                }
                var lastDigit = byte.Parse(sum.ToString().Last().ToString());
                yield return lastDigit;
            }
        }

        private static List<byte> LoadData() => DataReader.ReadAllDigits("Day16Input.txt").ToList();
    }
}
