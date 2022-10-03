using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpLib
{
    /// <summary>
    /// Represents a grid of values, with columns going from left to right (starting at index 0)
    /// and rows going from top to bottom (starting at index zero).
    /// </summary>
    public class Grid<T> where T : IEquatable<T>
    {
        public Grid(IEnumerable<IEnumerable<T>> input)
        {
            grid = CreateGrid(input);
        }

        /// <summary>
        /// Get a cell value
        /// </summary>
        public T Get(int row, int col)
        {
            return grid[row, col];
        }

        /// <summary>
        /// Set a cell value
        /// </summary>
        public void Set(int row, int col, T value)
        {
            grid[row, col] = value;
        }

        /// <summary>
        /// Get all values in a row
        /// </summary>
        public IEnumerable<T> GetRow(int row)
        {
            for (var c = 0; c < grid.GetLength(1); c++)
            {
                yield return grid[row, c];
            }
        }

        /// <summary>
        /// Get all values in a column
        /// </summary>
        public IEnumerable<T> GetCol(int col)
        {
            for (var r = 0; r < grid.GetLength(0); r++)
            {
                yield return grid[r, col];
            }
        }

        /// <summary>
        /// Find a value, and return the cell positions of all cells that contained it.
        /// </summary>
        public IEnumerable<(int Row, int Col)> Find(T value)
        {
            return Flattened().Where(cell => cell.Value.Equals(value)).Select(cell => (cell.Row, cell.Col));
        }

        /// <summary>
        /// Allows you to iterate over all cells.
        /// </summary>
        public IEnumerable<(T Value, int Row, int Col)> Flattened()
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    yield return (grid[row, col], row, col);
                }
            }
        }

        public override string ToString()
        {
            var s = "";
            for (var r = 0; r < grid.GetLength(0); r++)
            {
                for(var c = 0; c < grid.GetLength(1); c++)
                {
                    s += grid[r, c];
                }
                s += '\n';
            }
            return s;
        }

        private T[,] CreateGrid(IEnumerable<IEnumerable<T>> input)
        {
            var noOfRows = input.Count();
            var noOfColumns = input.First().Count();

            var rowIndex = 0;
            var colIndex = 0;
            var grid = new T[noOfRows, noOfColumns];
            foreach (var row in input)
            {
                foreach (var value in row)
                {
                    grid[rowIndex, colIndex] = value;
                    colIndex++;
                }
                colIndex = 0;
                rowIndex++;
            }
            return grid;
        }

        private T[,] grid;
    }
}
