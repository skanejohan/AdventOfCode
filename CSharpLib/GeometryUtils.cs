using System;
using System.Collections.Generic;

namespace CSharpLib
{
    public static class GeometryUtils
    {
        public static int ManhattanDistance((int X, int Y, int Z) p1, (int X, int Y, int Z) p2)
        {
            return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y) + Math.Abs(p1.Z - p2.Z);
        }

        public static IEnumerable<Func<int, int, int, (int X, int Y, int Z)>> OrientationFunctions()
        {
            yield return (x, y, z) => (x, y, z);
            yield return (x, y, z) => (y, z, x);
            yield return (x, y, z) => (-y, x, z);
            yield return (x, y, z) => (-x, -y, z);
            yield return (x, y, z) => (y, -x, z);
            yield return (x, y, z) => (z, y, -x);
            yield return (x, y, z) => (z, x, y);
            yield return (x, y, z) => (z, -y, x);
            yield return (x, y, z) => (z, -x, -y);
            yield return (x, y, z) => (-x, y, -z);
            yield return (x, y, z) => (y, x, -z);
            yield return (x, y, z) => (x, -y, -z);
            yield return (x, y, z) => (-y, -x, -z);
            yield return (x, y, z) => (-z, y, x);
            yield return (x, y, z) => (-z, x, -y);
            yield return (x, y, z) => (-z, -y, -x);
            yield return (x, y, z) => (-z, -x, y);
            yield return (x, y, z) => (x, -z, y);
            yield return (x, y, z) => (-y, -z, x);
            yield return (x, y, z) => (-x, -z, -y);
            yield return (x, y, z) => (y, -z, -x);
            yield return (x, y, z) => (x, z, -y);
            yield return (x, y, z) => (-y, z, -x);
            yield return (x, y, z) => (-x, z, y);
        }
    }
}
