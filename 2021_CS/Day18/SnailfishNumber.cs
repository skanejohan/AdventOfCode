using System;
using System.Collections;
using System.Collections.Generic;

namespace _2021_CS.Day18
{
    internal class SnailfishNumber : IEnumerable<(SnailfishNumber N, int Depth)>
    {
        public SnailfishNumber? Parent { get; set; }
        public SnailfishNumber? LeftChild { get; set; }
        public SnailfishNumber? RightChild { get; set; }
        public int? Number { get; set; }

        public SnailfishNumber(SnailfishNumber? parent)
        {
            Parent = parent;
        }

        public SnailfishNumber(SnailfishNumber? parent, int number)
        {
            Parent = parent;
            Number = number;
        }

        public SnailfishNumber(SnailfishNumber? parent, SnailfishNumber leftChild, SnailfishNumber rightChild)
        {
            Parent = parent;
            LeftChild = leftChild;
            RightChild = rightChild;
            LeftChild.Parent = this;
            RightChild.Parent = this;
        }

        public long Magnitude()
        {
            if (Number.HasValue)
            {
                return Number.Value;
            }
            return 3 * LeftChild.Magnitude() + 2 * RightChild.Magnitude();
        }

        public SnailfishNumber? NumberToTheLeftOf(SnailfishNumber sn)
        {
            SnailfishNumber? number = null;
            foreach (var (N, Depth) in this)
            {
                if (N.Number.HasValue && N.Parent != sn)
                {
                    number = N;
                }
                if (N == sn)
                {
                    return number;
                }
            }
            return null;
        }

        public SnailfishNumber? NumberToTheRightOf(SnailfishNumber sn)
        {
            var searching = false;
            foreach (var (N, Depth) in this)
            {
                if (N == sn)
                {
                    searching = true;
                }
                if (searching && N.Number.HasValue && N.Parent != sn)
                {
                    return N;
                }
            }
            return null;
        }

        public SnailfishNumber Clone()
        {
            return Parse(this.ToString());
        }

        public static SnailfishNumber? Parse(string s)
        {
            var pos = 0;
            return RecursiveParse(null);

            SnailfishNumber RecursiveParse(SnailfishNumber? parent)
            {
                var sn = new SnailfishNumber(parent);
                ThrowAway('[');
                sn.LeftChild = ParseChild(sn);
                ThrowAway(',');
                sn.RightChild = ParseChild(sn);
                ThrowAway(']');
                return sn;
            }

            SnailfishNumber ParseChild(SnailfishNumber parent)
            {
                return PeekChar() == '[' ? RecursiveParse(parent) : new SnailfishNumber(parent, GetChar() - '0');
            }

            char GetChar()
            {
                PeekChar();
                return s[pos++];
            }

            void ThrowAway(char c)
            {
                if (PeekChar() != c)
                {
                    ReportExpected(c);
                }
                pos++;
            }

            char PeekChar()
            {   
                try 
                {
                    return s[pos];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new Exception("Failed to get character - input string was empty");
                }
            }

            void ReportExpected(char c)
            {
                throw new Exception($"Expected character '{c}' in {s.Substring(pos)}");
            }
        }

        public override string ToString()
        {
            return ToS(this);

            static string ToS(SnailfishNumber sn)
            {
                if (sn.Number.HasValue)
                {
                    return $"{sn.Number}";
                }
                else 
                {
                    return $"[{ToS(sn.LeftChild)},{ToS(sn.RightChild)}]";
                }
            }
        }

        public IEnumerator<(SnailfishNumber N, int Depth)> GetEnumerator()
        {
            var stack = new Stack<(SnailfishNumber, int)>();

            stack.Push((this, 0));
            while (stack.Count > 0)
            {
                var (n, depth) = stack.Pop();
                if (n.RightChild != null)
                {
                    stack.Push((n.RightChild, depth + 1));
                }
                if (n.LeftChild != null)
                {
                    stack.Push((n.LeftChild, depth + 1));
                }
                yield return (n, depth);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
