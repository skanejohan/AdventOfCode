using System;

namespace _2021_CS
{
    public static class Day17
    {
        private static readonly int targetXmin = 150;
        private static readonly int targetXmax = 193;
        private static readonly int targetYmin = -136;
        private static readonly int targetYmax = -86;

        private static readonly int velocityYmax = -targetYmin;

        public static long Part1()
        {
            var highestSoFar = 0;
            for (var dx = 1; dx < targetXmax + 1; dx++)
            {
                for (var dy = targetYmin; dy < velocityYmax; dy++)
                {
                    var (Success, highestY) = Run(dx, dy);
                    if (Success && highestY > highestSoFar)
                    {
                        highestSoFar = highestY;
                    }
                }
            }
            return highestSoFar;
        }

        public static long Part2()
        {
            var count = 0;
            for (var dx = 1; dx < targetXmax + 1; dx++)
            {
                for (var dy = targetYmin; dy < velocityYmax; dy++)
                {
                    var (Success, _) = Run(dx, dy);
                    if (Success)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private static (bool Success, int highestY) Run(int dX, int dY)
        {
            var x = 0;
            var y = 0;
            var dx = dX;
            var dy = dY;
            var highestY = y;
            while(true)
            {
                (x, y, dx, dy) = Step(x, y, dx, dy);
                highestY = Math.Max(highestY, y);
                if (y < targetYmin)
                {
                    return (false, -1);
                }
                else if (x >= targetXmin && x <= targetXmax && y >= targetYmin && y <= targetYmax)
                {
                    return (true, highestY);
                }
            }
        }

        private static (int x, int y, int dx, int dy) Step(int x, int y, int dx, int dy)
        {
            var newDy = dy - 1;
            var newDx = dx > 0 ? dx - 1 : dx < 0 ? dx + 1 : dx;
            return (x + dx, y + dy, newDx, newDy);
        }
    }
}
