using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpLib.DataStructures
{
    public class Box
    {
        public long MinX { get; }
        public long MinY { get; }
        public long MinZ { get; }
        public long MaxX { get; }
        public long MaxY { get; }
        public long MaxZ { get; }

        public Box(long minX, long minY, long minZ, long maxX, long maxY, long maxZ)
        {
            MinX = minX;
            MinY = minY;
            MinZ = minZ;
            MaxX = maxX;
            MaxY = maxY;
            MaxZ = maxZ;
        }

        /// <summary>
        /// Return the volume of this box
        /// </summary>
        public long Volume()
        {
            return (MaxX - MinX + 1) * (MaxY - MinY + 1) * (MaxZ - MinZ + 1);
        }

        /// <summary>
        /// Return a box representing the volume which both this and 
        /// the other box occupies. If no volume is occupied by both 
        /// boxes, returns null.
        /// </summary>
        public Box? Intersection(Box other)
        {
            var minX = Math.Max(MinX, other.MinX);
            var minY = Math.Max(MinY, other.MinY);
            var minZ = Math.Max(MinZ, other.MinZ);
            var maxX = Math.Min(MaxX, other.MaxX);
            var maxY = Math.Min(MaxY, other.MaxY);
            var maxZ = Math.Min(MaxZ, other.MaxZ);
            if (minX > maxX || minY > maxY || minZ > maxZ)
            {
                return null;
            }
            return new Box(minX, minY, minZ, maxX, maxY, maxZ);
        }

        /// <summary>
        /// Return all non-null intersections with this box.
        /// </summary>
        public IEnumerable<Box> Intersections(IEnumerable<Box> others)
        {
            foreach (var box in others)
            {
                var overlap = Intersection(box);
                if (overlap != null)
                {
                    yield return overlap;
                }
            }
        }

        /// <summary>
        /// Return the total volume of all boxes, taking 
        /// into account that they may overlap
        /// </summary>
        public static long TotalVolume(IEnumerable<Box> boxes)
        {
            if (boxes.Count() == 0)
            {
                return 0;
            }

            var thisBox = boxes.First();
            var otherBoxes = boxes.Skip(1);

            if (boxes.Count() == 1)
            {
                return thisBox.Volume();
            }

            return thisBox.Volume() + TotalVolume(otherBoxes) - TotalVolume(thisBox.Intersections(otherBoxes));
        }
    }
}
