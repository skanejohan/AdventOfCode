using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpLib.DataStructures
{
    public class InfiniteGrid<T> : IEnumerable<(int Row, int Col, T Value)>
    {
        public int MinX => GetMinX();
        public int MinY => GetMinY();
        public int MaxX => GetMaxX();
        public int MaxY => GetMaxY();
        public int NoOfCellsSet => grid.Count;

        public InfiniteGrid(T def = default)
        {
            grid = new Dictionary<(int, int), T>();
            this.def = def;
        }

        public void Add(int x, int y, T value)
        {
            grid.Add((x, y), value);
        }

        public void Remove(int x, int y)
        {
            grid.Remove((x, y));
            minX = null;
            minY = null;
            maxX = null;
            maxY = null;
        }

        public T Get(int x, int y, T defaultValue)
        {
            return Has(x, y) ? grid[(x, y)] : defaultValue;
        }

        public bool TryGet(int x, int y, out T value)
        {
            return grid.TryGetValue((x, y), out value);
        }

        public bool Has(int x, int y)
        {
            return grid.ContainsKey((x, y));
        }

        public override string ToString()
        {
            var s = "";
            for (var y = MinY; y <= MaxY; y++)
            {
                for (var x = MinX; x <= MaxX; x++)
                {
                    s += Get(x, y, def);
                }
                s += '\n';
            }
            return s;
        }

        private int GetMinX()
        {
            if (!minX.HasValue)
            {
                minX = grid.Count == 0 ? 0 : grid.Select(kv => kv.Key.Item1).Min();
            }
            return minX.Value;
        }

        private int GetMinY()
        {
            if (!minY.HasValue)
            {
                minY = grid.Count == 0 ? 0 : grid.Select(kv => kv.Key.Item2).Min();
            }
            return minY.Value;
        }

        private int GetMaxX()
        {
            if (!maxX.HasValue)
            {
                maxX = grid.Count == 0 ? 0 : grid.Select(kv => kv.Key.Item1).Max();
            }
            return maxX.Value;
        }

        private int GetMaxY()
        {
            if (!maxY.HasValue)
            {
                maxY = grid.Count == 0 ? 0 : grid.Select(kv => kv.Key.Item2).Max();
            }
            return maxY.Value;
        }

        public IEnumerator<(int Row, int Col, T Value)> GetEnumerator()
        {
            foreach (var kv in grid)
            {
                yield return (kv.Key.Item1, kv.Key.Item2, kv.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int? minX;
        private int? minY;
        private int? maxX;
        private int? maxY;
        private readonly Dictionary<(int, int), T> grid;
        private readonly T def;
    }
}
