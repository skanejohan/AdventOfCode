using CSharpLib;
using CSharpLib.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace _2021_CS
{
    public static class Day04
    {
        public static int Part1()
        {
            var data = RealData().ToList();
            var numbers = data.Pop().Split(',').Select(int.Parse);
            var boards = data.ChunkBy(s => s == "").Select(lines => new BingoBoard(lines)).ToList();

            foreach (var number in numbers)
            {
                foreach(var board in boards)
                {
                    if (board.Play(number))
                    {
                        return board.Score(number);
                    }
                }
            }
            return 0;
        }

        public static int Part2()
        {
            var data = RealData().ToList();
            var numbers = data.Pop().Split(',').Select(int.Parse);
            var boards = data.ChunkBy(s => s == "").Select(lines => new BingoBoard(lines)).ToList();

            var lastScore = 0;
            var finishedBoards = new HashSet<BingoBoard>();
            foreach (var number in numbers)
            {
                foreach (var board in boards.Where(b => !finishedBoards.Contains(b)))
                {
                    if (board.Play(number))
                    {
                        lastScore = board.Score(number);
                        finishedBoards.Add(board);
                    }
                }
            }
            return lastScore;
        }

        private static IEnumerable<string> RealData() => new DataLoader(2021, 4).ReadStrings("DataReal.txt");
        private static IEnumerable<string> TestData() => new DataLoader(2021, 4).ReadStrings("DataTest.txt");
    }
}
