using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpLib.DataStructures
{
    /// <summary>
    /// Represents a grid of values, with columns going from left to right (starting at index 0)
    /// and rows going from top to bottom (starting at index zero).
    /// </summary>
    public class Grid<T> : IEnumerable<(int Row, int Col, T Value)> where T : IEquatable<T>
    {
        public Grid(IEnumerable<IEnumerable<T>> input)
        {
            grid = CreateGrid(input);
        }

        public Grid(IEnumerable<(int Row, int Col, T Value)> values)
        {
            var noOfRows = values.Select(d => d.Row).Max() + 1;
            var noOfCols = values.Select(d => d.Col).Max() + 1;
            grid = new T[noOfRows, noOfCols];
            foreach (var (Row, Col, Value) in values)
            {
                grid[Row, Col] = Value;
            }
        }

        public Grid(int noOfRows, int noOfCols)
        {
            grid = new T[noOfRows, noOfCols];
        }

        /// <summary>
        /// Get the number of rows in the grid
        /// </summary>
        public int NoOfRows()
        {
            return grid.GetLength(0);
        }

        /// <summary>
        /// Get the number of columns in the grid
        /// </summary>
        public int NoOfCols()
        {
            return grid.GetLength(1);
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
            for (var c = 0; c < NoOfCols(); c++)
            {
                yield return grid[row, c];
            }
        }

        /// <summary>
        /// Get all values in a column
        /// </summary>
        public IEnumerable<T> GetCol(int col)
        {
            for (var r = 0; r < NoOfRows(); r++)
            {
                yield return grid[r, col];
            }
        }

        /// <summary>
        /// Get up to 4 neighbors of a cell
        /// </summary>
        public IEnumerable<(int Row, int Col, T Value)> GetNeighbors4(int row, int col)
        {
            if (row > 0)
            {
                yield return (row - 1, col, grid[row - 1, col]);
            }
            if (col < NoOfCols() - 1)
            {
                yield return (row, col + 1, grid[row, col + 1]);
            }
            if (row < NoOfRows() - 1)
            {
                yield return (row + 1, col, grid[row + 1, col]);
            }
            if (col > 0)
            {
                yield return (row, col - 1, grid[row, col - 1]);
            }
        }

        public IEnumerable<(int Row, int Col, T Value)> GetNeighbors4((int Row, int Col) pos)
        {
            return GetNeighbors4(pos.Row, pos.Col);
        }

        /// <summary>
        /// Get up to 8 neighbors of a cell
        /// </summary>
        public IEnumerable<(int Row, int Col, T Value)> GetNeighbors8(int row, int col)
        {
            var minRow = row > 0 ? row - 1 : 0;
            var minCol = col > 0 ? col - 1 : 0;
            var maxRow = row < NoOfRows() - 1 ? row + 1 : NoOfRows() - 1;
            var maxCol = col < NoOfCols() - 1 ? col + 1 : NoOfCols() - 1;
            for (var r = minRow; r <= maxRow; r++)
            {
                for (var c = minCol; c <= maxCol; c++)
                {
                    if (r != row || c != col)
                    {
                        yield return (r, c, grid[r, c]);
                    }
                }
            }
        }

        /// <summary>
        /// Find a value, and return the cell positions of all cells that contained it.
        /// </summary>
        public IEnumerable<(int Row, int Col)> Find(T value)
        {
            return this.Where(cell => cell.Value.Equals(value)).Select(cell => (cell.Row, cell.Col));
        }

        /// <summary>
        /// Allows you to iterate over all cells.
        /// </summary>
        public IEnumerator<(int Row, int Col, T Value)> GetEnumerator()
        {
            for (int row = 0; row < NoOfRows(); row++)
            {
                for (int col = 0; col < NoOfCols(); col++)
                {
                    yield return (row, col, grid[row, col]);
                }
            }
        }

        public override string ToString()
        {
            var s = "";
            for (var r = 0; r < NoOfRows(); r++)
            {
                for (var c = 0; c < NoOfCols(); c++)
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

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private T[,] grid;
    }
}
