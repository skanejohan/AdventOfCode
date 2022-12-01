using CSharpLib;
using CSharpLib.DataStructures;
using CSharpLib.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace _2021_CS
{
    public static class Day13
    {
        public static long Part1()
        {
            var (paper, folds) = GetPaperAndFolds("DataReal.txt");
            var foldedPaper = Fold(paper, folds.First());
            return foldedPaper.Where(x => x.Value == 1).Count();
        }

        public static string Part2()
        {
            var (paper, folds) = GetPaperAndFolds("DataReal.txt");
            foreach (var fold in folds)
            {
                paper = Fold(paper, fold);
            }
            //System.Console.WriteLine(new string(paper.ToString().Select(i => i == '1' ? '#' : i == '0' ? '.' : i).ToArray())); // Will print PFKLKCFP
            return "PFKLKCFP";
        }

        private static Grid<int> Fold(Grid<int> paper, (char Direction, int Line) fold)
        {
            var rows = fold.Direction == 'y' ? fold.Line : paper.NoOfRows();
            var cols = fold.Direction == 'y' ? paper.NoOfCols() : fold.Line;

            var foldedPaper = new Grid<int>(rows, cols);
            foreach (var (Row, Col, Value) in paper.Where(p => p.Value == 1))
            {
                var row = Row >= rows ? 2 * rows - Row : Row;
                var col = Col >= cols ? 2 * cols - Col : Col;
                if (row >= 0 && col >= 0 && row < rows && col < cols)
                {
                    foldedPaper.Set(row, col, Value);
                }
            }
            return foldedPaper;
        }

        private static (Grid<int> Paper, IEnumerable<(char, int)> Folds) GetPaperAndFolds(string fileName)
        {
            var input = (new DataLoader("2021_CS", 13).ReadStrings(fileName)).ToList().ChunkBy(s => s == "").ToList();
            var dots = input[0].Select(s => (int.Parse(s.Split(',')[0]), int.Parse(s.Split(',')[1]))).Select(d => (d.Item2, d.Item1, 1)).ToList();
            var folds = input[1].Select(s => (s.Split('=')[0].Last(), int.Parse(s.Split('=')[1])));
            return (new Grid<int>(dots), folds);
        }
    }
}
