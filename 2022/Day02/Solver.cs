using CSharpLib;
using System.Collections.Generic;
using System.Linq;

namespace Y2022.Day02
{
    public static class Solver
    {
        public static long Part1()
        {
            return LoadData("data.txt").Select(score).Sum();

            static int score((char Opponent, char Myself) line)
            {
                return Scores[line.Myself][line.Opponent];
            }
        }

        public static long Part2()
        {
            Dictionary<char, char> ToLose = new Dictionary<char, char> { { 'A', 'Z' }, { 'B', 'X' }, { 'C', 'Y' } };
            Dictionary<char, char> ToDraw = new Dictionary<char, char> { { 'A', 'X' }, { 'B', 'Y' }, { 'C', 'Z' } };
            Dictionary<char, char> ToWin = new Dictionary<char, char> { { 'A', 'Y' }, { 'B', 'Z' }, { 'C', 'X' } };
            Dictionary<char, Dictionary<char, char>> MyMove = new Dictionary<char, Dictionary<char, char>>
            {
                { 'X', ToLose }, {'Y', ToDraw }, { 'Z', ToWin }
            };

            return LoadData("data.txt").Select(score).Sum();

            int score((char Opponent, char Myself) line)
            {
                var myChoice = MyMove[line.Myself][line.Opponent];
                return Scores[myChoice][line.Opponent];
            }

        }

        private static readonly Dictionary<char, Dictionary<char, int>> Scores = 
            new Dictionary<char, Dictionary<char, int>>
            {
                { 'X', new Dictionary<char, int> { { 'A', 1 + 3 }, { 'B', 1 + 0 }, { 'C', 1 + 6 } } },
                { 'Y', new Dictionary<char, int> { { 'A', 2 + 6 }, { 'B', 2 + 3 }, { 'C', 2 + 0 } } },
                { 'Z', new Dictionary<char, int> { { 'A', 3 + 0 }, { 'B', 3 + 6 }, { 'C', 3 + 3 } } },
            };

        private static IEnumerable<(char, char)> LoadData(string fileName)
        {
            return new DataLoader(2022, 2).ReadStrings(fileName).Select(s => (s[0], s[2]));
        }
    }
}
