using CSharpLib;
using System;
using System.Collections.Generic;

namespace _2021_CS.Day21
{
    public static class Solver
    {
        public static long Part1()
        {
            var noOfDieRolls = 0;
            var currentDieValue = 0;
            var player1position = 10;
            var player2position = 4;
            var player1score = 0;
            var player2score = 0;

            while(true)
            {
                player1position = (player1position + RollThree()).Wrap(10);
                player1score += player1position;
                if (player1score > 999)
                {
                    break;
                }

                player2position = (player2position + RollThree()).Wrap(10);
                player2score += player2position;
                if (player1score > 999)
                {
                    break;
                }
            }
            
            int RollThree()
            {
                var sum = 0;
                for (int i = 0; i < 3; i++)
                {
                    currentDieValue++;
                    currentDieValue = currentDieValue.Wrap(100);
                    sum += currentDieValue;
                }
                noOfDieRolls += 3;
                return sum;
            }

            return noOfDieRolls * Math.Min(player1score, player2score);
        }

        public static long Part2()
        {
            var stack = new Stack<Universe>();
            var universesWon = new long[] { 0, 0 };

            stack.Push(new Universe(10, 4));
            while (stack.Count > 0)
            {
                var oldUniverse = stack.Pop();
                var newUniverses = Play(oldUniverse);
                foreach (var u in newUniverses)
                {
                    if (u.Scores[0] >= 21)
                    {
                        universesWon[0] += u.Duplicates;
                    }
                    else if (u.Scores[1] >= 21)
                    {
                        universesWon[1] += u.Duplicates;
                    }
                    else
                    {
                        stack.Push(u);
                    }
                }
            }

            return Math.Max(universesWon[0], universesWon[1]);
        }

        class Universe
        {
            public int NextPlayerIndex { get; }
            public int[] Positions { get; }
            public int[] Scores { get; }
            public long Duplicates { get; }

            public Universe(int player1position, int player2Position, int player1score = 0, 
                int player2score = 0, long duplicates = 1, int nextPlayerIndex = 0)
            {
                Positions = new int[] { player1position, player2Position };
                Scores = new int[] { player1score, player2score };
                Duplicates = duplicates;
                NextPlayerIndex = nextPlayerIndex;
            }

            public Universe Move(int steps, int duplicates)
            {
                var universe = new Universe(Positions[0], Positions[1], Scores[0], Scores[1], Duplicates * duplicates, 1 - NextPlayerIndex);
                universe.Positions[NextPlayerIndex] = (universe.Positions[NextPlayerIndex] + steps).Wrap(10);
                universe.Scores[NextPlayerIndex] = universe.Scores[NextPlayerIndex] + universe.Positions[NextPlayerIndex];
                return universe;
            }
        }

        private static IEnumerable<Universe> Play(Universe universe)
        {
            // Rolling three dice in a universe generates 27 new universes but only 7 distinct.
            // This dictionary holds the different values and how many universes in total they
            // generate (3 can only be rolled one way - 1, 1, 1 - and therefore only generates
            // one new universe, whereas 4 can be rolled in three ways - 1,1,2 / 1,2,1 / 2,1,1
            // and therefore generates 3 new universes).
            yield return universe.Move(3, 1);
            yield return universe.Move(4, 3);
            yield return universe.Move(5, 6);
            yield return universe.Move(6, 7);
            yield return universe.Move(7, 6);
            yield return universe.Move(8, 3);
            yield return universe.Move(9, 1);
        }
    }
}
