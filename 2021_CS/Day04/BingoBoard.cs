using CSharpLib.DataStructures;
using System.Collections.Generic;
using System.Linq;

namespace _2021_CS
{
    internal class BingoBoard
    {
        public BingoBoard(IEnumerable<string> input)
        {
            board = new Grid<(int, bool)>(input.Select(s => s.Trim().Replace("  ", " ").Split(' ').Select(c => (int.Parse(c), false))));
        }

        public (bool Found, int Row, int Col) Find(int value)
        {
            var foundIn = board.Find((value, false));
            return foundIn.Any() ? (true, foundIn.First().Row, foundIn.First().Col) : (false, 0, 0);
        }

        public bool Play(int value)
        {
            var (Found, Row, Col) = Find(value);
            if (Found)
            {
                board.Set(Row, Col, (value, true));
            }
            return board.GetRow(Row).All(c => c.Item2) || board.GetCol(Col).All(c => c.Item2);
        }

        public int Score(int lastNumberPlayed)
        {
            return board.Where(c => !c.Value.Item2).Select(c => c.Value.Item1).Sum() * lastNumberPlayed;
        }

        private Grid<(int, bool)> board;
    }
}
