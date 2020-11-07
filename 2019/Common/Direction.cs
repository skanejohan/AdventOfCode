namespace AdventOfCode.Common
{
    internal enum Direction { Right, Down, Left, Up };

    internal static class DirectionHelpers
    {
        public static Direction TurnRight(this Direction direction)
        {
            switch(direction)
            {
                case Direction.Up: return Direction.Right;
                case Direction.Right: return Direction.Down;
                case Direction.Down: return Direction.Left;
                default: return Direction.Up;
            }
        }

        public static Direction TurnLeft(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return Direction.Left;
                case Direction.Right: return Direction.Up;
                case Direction.Down: return Direction.Right;
                default: return Direction.Down;
            }
        }
    }
}
