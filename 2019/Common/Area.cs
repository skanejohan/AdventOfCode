using System;
using System.Collections.Generic;

namespace AdventOfCode.Common
{
    internal class Area<T>
    {

        public int MinX { get; private set; } = 0;
        public int MinY { get; private set; } = 0;
        public int MaxX { get; private set; } = 0;
        public int MaxY { get; private set; } = 0;

        public Area(T defaultValue)
        {
            this.defaultValue = defaultValue;
        }

        public void Set((int x, int y) pos, T value)
        {
            area[pos] = value;
            MinX = Math.Min(MinX, pos.x);
            MinY = Math.Min(MinY, pos.y);
            MaxX = Math.Max(MaxX, pos.x);
            MaxY = Math.Max(MaxY, pos.y);
        }

        public T Get((int x, int y) pos)
        {
            if (!area.TryGetValue(pos, out T value))
            {
                return defaultValue;
            }
            return value;
        }

        public IEnumerable<(int x, int y)> NeighborsOf((int x, int y) pos, Func<T, bool> acceptNeighbor)
        {
            if (acceptNeighbor(Get((pos.x - 1, pos.y)))) { yield return (pos.x - 1, pos.y); }
            if (acceptNeighbor(Get((pos.x, pos.y - 1)))) { yield return (pos.x, pos.y - 1); }
            if (acceptNeighbor(Get((pos.x + 1, pos.y)))) { yield return (pos.x + 1, pos.y); }
            if (acceptNeighbor(Get((pos.x, pos.y + 1)))) { yield return (pos.x, pos.y + 1); }
        }

        public IEnumerable<(int x, int y)> AllSpaces(Func<T, bool> acceptSpace)
        {
            foreach (var kv in area)
            {
                if (acceptSpace(kv.Value))
                {
                    yield return kv.Key;
                }
            }
        }

        private T defaultValue;

        private readonly Dictionary<(int x, int y), T> area = new Dictionary<(int x, int y), T>();
    }
}
