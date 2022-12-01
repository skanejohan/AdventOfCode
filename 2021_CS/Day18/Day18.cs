using CSharpLib;
using System;
using System.Linq;

namespace _2021_CS.Day18
{
    public static class Solver
    {
        public static long Part1()
        {
            var data = new DataLoader("2021_CS", 18).ReadStrings("RealData.txt").Select(SnailfishNumber.Parse);
            var result = data.First();
            foreach(var sn in data.Skip(1))
            {
                result = Add(result, sn);
            }
            return result.Magnitude();
        }

        public static long Part2()
        {
            var data = new DataLoader("2021_CS", 18).ReadStrings("RealData.txt").Select(SnailfishNumber.Parse);

            long largestMagnitude = 0;
            var snailfishNumbers = data.ToList();
            for (int i = 0; i < snailfishNumbers.Count - 1; i++)
            {
                for (var j = i + 1; j < snailfishNumbers.Count; j++)
                {
                    var mag = Add(snailfishNumbers[i], snailfishNumbers[j]).Magnitude();
                    largestMagnitude = Math.Max(largestMagnitude, Add(snailfishNumbers[i], snailfishNumbers[j]).Magnitude());
                    largestMagnitude = Math.Max(largestMagnitude, Add(snailfishNumbers[j], snailfishNumbers[i]).Magnitude());
                }
            }
            return largestMagnitude;
        }

        private static bool Explode(SnailfishNumber sn)
        {
            var firstPairAtLevel4 = sn.FirstOrDefault(sn => sn.Depth == 4 && !sn.N.Number.HasValue).N;
            if (firstPairAtLevel4 == null)
            {
                return false;
            }

            var leftNumber = sn.NumberToTheLeftOf(firstPairAtLevel4);
            if (leftNumber != null)
            {
                leftNumber.Number += firstPairAtLevel4.LeftChild.Number;
            }

            var rightNumber = sn.NumberToTheRightOf(firstPairAtLevel4);
            if (rightNumber != null)
            {
                rightNumber.Number += firstPairAtLevel4.RightChild.Number;
            }

            if (firstPairAtLevel4.Parent.LeftChild == firstPairAtLevel4)
            {
                firstPairAtLevel4.Parent.LeftChild = new SnailfishNumber(firstPairAtLevel4.Parent, 0);
            }
            else
            {
                firstPairAtLevel4.Parent.RightChild = new SnailfishNumber(firstPairAtLevel4.Parent, 0);
            }

            return leftNumber != null || rightNumber != null;
        }

        private static bool Split(SnailfishNumber sn)
        {
            var firstNumberToSplit = sn.FirstOrDefault(sn => sn.N.Number.HasValue && sn.N.Number > 9).N;
            if (firstNumberToSplit == null)
            {
                return false;
            }

            var leftValue = firstNumberToSplit.Number / 2;
            var rightValue = firstNumberToSplit.Number % 2 == 0 ? leftValue : leftValue + 1;
            firstNumberToSplit.Number = null;
            firstNumberToSplit.LeftChild = new SnailfishNumber(firstNumberToSplit, (int)leftValue);
            firstNumberToSplit.RightChild = new SnailfishNumber(firstNumberToSplit, (int)rightValue);
            return true;
        }

        private static SnailfishNumber Add (SnailfishNumber sn1, SnailfishNumber sn2)
        {
            var sum = new SnailfishNumber(null, sn1.Clone(), sn2.Clone());
            while(Explode(sum) || Split(sum))
            {
            }
            return sum;
        }
    }
}
