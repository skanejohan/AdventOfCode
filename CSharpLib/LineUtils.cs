using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpLib
{
    /// <summary>
    /// Utility functions for treating a pair of coordinates as a line.
    /// </summary>
    public static class LineUtils
    {
        /// <summary>
        /// Is the line represented be these coordinates horizontal?
        /// </summary>
        public static bool IsHorizontal(int X1, int Y1, int X2, int Y2) 
            => Y1 == Y2;

        /// <summary>
        /// Is the line represented be these coordinates vertical?
        /// </summary>
        public static bool IsVertical(int X1, int Y1, int X2, int Y2) 
            => X1 == X2;

        /// <summary>
        /// Is the line represented be these coordinates diagonal (45 degrees)?
        /// </summary>
        public static bool IsDiagonal(int X1, int Y1, int X2, int Y2)
            => Math.Abs(X2 - X1) == Math.Abs(Y2 - Y1);

        /// <summary>
        /// Enumerate over all points on the line represented be these coordinates.
        /// Assumes that the line is horizontal, vertical or diagonal.
        /// </summary>
        public static IEnumerable<(int X, int Y)> AllPoints(int X1, int Y1, int X2, int Y2)
        {
            if (IsHorizontal(X1, Y1, X2, Y2))
            {
                var minX = Math.Min(X1, X2);
                var maxX = Math.Max(X1, X2);
                foreach (var x in Enumerable.Range(minX, maxX - minX + 1))
                {
                    yield return(x, Y1);
                }
            }
            else if (IsVertical(X1, Y1, X2, Y2))
            {
                var minY = Math.Min(Y1, Y2);
                var maxY = Math.Max(Y1, Y2);
                foreach (var y in Enumerable.Range(minY, maxY - minY + 1))
                {
                    yield return (X1, y);
                }

            }
            else if (IsDiagonal(X1, Y1, X2, Y2))
            {
                var (leftX, leftY) = X1 < X2 ? (X1, Y1) : (X2, Y2);
                var (rightX, rightY) = X1 < X2 ? (X2, Y2) : (X1, Y1);
                var dy = leftY < rightY ? 1 : -1;
                foreach (var i in Enumerable.Range(0, rightX - leftX + 1))
                {
                    yield return (leftX + i, leftY + i * dy);
                }
            }
        }
    }
}
