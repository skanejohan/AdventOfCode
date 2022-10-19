using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021_CS.Day23
{
    internal class BurrowTransformerTests
    {
        public static void Run()
        {
            AssertGetAmphipods();
            AssertHall();
            AssertRoomsPart1();
            AssertRoomPart1();
            AssertIsDonePart1();
            AssertTryGetTargetCellPart1();
            AssertNeighborsPart1();

            AssertRoomsPart2();
            AssertRoomPart2();
            AssertIsDonePart2();
            AssertTryGetTargetCellPart2();
        }

        private static void AssertGetAmphipods()
        {
            var cells = new BurrowTransformer(false).GetAmphipods(".......BA..CD..BC..DA..");
            Assert(8, cells.Count());
            AssertContains(new BurrowTransformer.Cell(3, 2, 'B'), cells);
            AssertContains(new BurrowTransformer.Cell(3, 3, 'A'), cells);
            AssertContains(new BurrowTransformer.Cell(5, 2, 'C'), cells);
            AssertContains(new BurrowTransformer.Cell(5, 3, 'D'), cells);
            AssertContains(new BurrowTransformer.Cell(7, 2, 'B'), cells);
            AssertContains(new BurrowTransformer.Cell(7, 3, 'C'), cells);
            AssertContains(new BurrowTransformer.Cell(9, 2, 'D'), cells);
            AssertContains(new BurrowTransformer.Cell(9, 3, 'A'), cells);
        }

        private static void AssertHall()
        {
            var cells = new BurrowTransformer(false).Hall("ADCBABD................");
            Assert(7, cells.Count());
            AssertContains(new BurrowTransformer.Cell(1, 1, 'A'), cells);
            AssertContains(new BurrowTransformer.Cell(2, 1, 'D'), cells);
            AssertContains(new BurrowTransformer.Cell(4, 1, 'C'), cells);
            AssertContains(new BurrowTransformer.Cell(6, 1, 'B'), cells);
            AssertContains(new BurrowTransformer.Cell(8, 1, 'A'), cells);
            AssertContains(new BurrowTransformer.Cell(10, 1, 'B'), cells);
            AssertContains(new BurrowTransformer.Cell(11, 1, 'D'), cells);
        }

        private static void AssertRoomsPart1()
        {
            var cells = new BurrowTransformer(false).Rooms(".......BA..CD..BC..DA..");
            Assert(8, cells.Count());
            AssertContains(new BurrowTransformer.Cell(3, 2, 'B'), cells);
            AssertContains(new BurrowTransformer.Cell(3, 3, 'A'), cells);
            AssertContains(new BurrowTransformer.Cell(5, 2, 'C'), cells);
            AssertContains(new BurrowTransformer.Cell(5, 3, 'D'), cells);
            AssertContains(new BurrowTransformer.Cell(7, 2, 'B'), cells);
            AssertContains(new BurrowTransformer.Cell(7, 3, 'C'), cells);
            AssertContains(new BurrowTransformer.Cell(9, 2, 'D'), cells);
            AssertContains(new BurrowTransformer.Cell(9, 3, 'A'), cells);
        }

        private static void AssertRoomPart1()
        {
            var cells = new BurrowTransformer(false).TargetRoom(".......BA..CD..BC..DA..", 'A');
            Assert(2, cells.Count());
            AssertContains(new BurrowTransformer.Cell(3, 2, 'B'), cells);
            AssertContains(new BurrowTransformer.Cell(3, 3, 'A'), cells);

            cells = new BurrowTransformer(false).TargetRoom(".......BA..CD..BC..DA..", 'B');
            Assert(2, cells.Count());
            AssertContains(new BurrowTransformer.Cell(5, 2, 'C'), cells);
            AssertContains(new BurrowTransformer.Cell(5, 3, 'D'), cells);

            cells = new BurrowTransformer(false).TargetRoom(".......BA..CD..BC..DA..", 'C');
            Assert(2, cells.Count());
            AssertContains(new BurrowTransformer.Cell(7, 2, 'B'), cells);
            AssertContains(new BurrowTransformer.Cell(7, 3, 'C'), cells);

            cells = new BurrowTransformer(false).TargetRoom(".......BA..CD..BC..DA..", 'D');
            Assert(2, cells.Count());
            AssertContains(new BurrowTransformer.Cell(9, 2, 'D'), cells);
            AssertContains(new BurrowTransformer.Cell(9, 3, 'A'), cells);
        }

        private static void AssertIsDonePart1()
        {
            Assert(false, new BurrowTransformer(false).IsDone("...................DC..", new BurrowTransformer.Cell(9, 2, 'D')));
            Assert(false, new BurrowTransformer(false).IsDone("...................CC..", new BurrowTransformer.Cell(9, 3, 'D')));
            Assert(false, new BurrowTransformer(false).IsDone(".......................", new BurrowTransformer.Cell(9, 3, 'D')));
            Assert(true, new BurrowTransformer(false).IsDone("....................D..", new BurrowTransformer.Cell(9, 3, 'D')));
            Assert(true, new BurrowTransformer(false).IsDone("...................DD..", new BurrowTransformer.Cell(9, 3, 'D')));
            Assert(true, new BurrowTransformer(false).IsDone("...................CD..", new BurrowTransformer.Cell(9, 3, 'D')));
        }

        private static void AssertTryGetTargetCellPart1()
        {
            Assert(false, new BurrowTransformer(false).TryGetTargetCell("...................CD..", 'D', out var c));
            Assert(false, new BurrowTransformer(false).TryGetTargetCell("...................CC..", 'D', out c));
            Assert(false, new BurrowTransformer(false).TryGetTargetCell("....................C..", 'D', out c));
            Assert(true, new BurrowTransformer(false).TryGetTargetCell("....................D..", 'D', out c));
            Assert(new BurrowTransformer.Cell(9, 2, '.'), c);
            Assert(true, new BurrowTransformer(false).TryGetTargetCell(".......................", 'D', out c));
            Assert(new BurrowTransformer.Cell(9, 3, '.'), c);
        }

        private static void AssertNeighborsPart1()
        {
            var cells = new BurrowTransformer(false).Neighbors(".......BA..CD..BC..DA..").ToList();

            Assert(28, cells.Count());
            AssertContains(("B.......A..CD..BC..DA..", 30), cells);
            AssertContains((".B......A..CD..BC..DA..", 20), cells);
            AssertContains(("..B.....A..CD..BC..DA..", 20), cells);
            AssertContains(("...B....A..CD..BC..DA..", 40), cells);
            AssertContains(("....B...A..CD..BC..DA..", 60), cells);
            AssertContains((".....B..A..CD..BC..DA..", 80), cells);
            AssertContains(("......B.A..CD..BC..DA..", 90), cells);
            AssertContains(("C......BA...D..BC..DA..", 500), cells);
            AssertContains((".C.....BA...D..BC..DA..", 400), cells);
            AssertContains(("..C....BA...D..BC..DA..", 200), cells);
            AssertContains(("...C...BA...D..BC..DA..", 200), cells);
            AssertContains(("....C..BA...D..BC..DA..", 400), cells);
            AssertContains((".....C.BA...D..BC..DA..", 600), cells);
            AssertContains(("......CBA...D..BC..DA..", 700), cells);
            AssertContains(("B......BA..CD...C..DA..", 70), cells);
            AssertContains((".B.....BA..CD...C..DA..", 60), cells);
            AssertContains(("..B....BA..CD...C..DA..", 40), cells);
            AssertContains(("...B...BA..CD...C..DA..", 20), cells);
            AssertContains(("....B..BA..CD...C..DA..", 20), cells);
            AssertContains((".....B.BA..CD...C..DA..", 40), cells);
            AssertContains(("......BBA..CD...C..DA..", 50), cells);
            AssertContains(("D......BA..CD..BC...A..", 9000), cells);
            AssertContains((".D.....BA..CD..BC...A..", 8000), cells);
            AssertContains(("..D....BA..CD..BC...A..", 6000), cells);
            AssertContains(("...D...BA..CD..BC...A..", 4000), cells);
            AssertContains(("....D..BA..CD..BC...A..", 2000), cells);
            AssertContains((".....D.BA..CD..BC...A..", 2000), cells);
            AssertContains(("......DBA..CD..BC...A..", 3000), cells);
        }

        private static void AssertRoomsPart2()
        {
            var cells = new BurrowTransformer(true).Rooms(".......BDDACCBDBBACDACA");
            Assert(16, cells.Count());
            AssertContains(new BurrowTransformer.Cell(3, 2, 'B'), cells);
            AssertContains(new BurrowTransformer.Cell(3, 3, 'D'), cells);
            AssertContains(new BurrowTransformer.Cell(3, 4, 'D'), cells);
            AssertContains(new BurrowTransformer.Cell(3, 5, 'A'), cells);
            AssertContains(new BurrowTransformer.Cell(5, 2, 'C'), cells);
            AssertContains(new BurrowTransformer.Cell(5, 3, 'C'), cells);
            AssertContains(new BurrowTransformer.Cell(5, 4, 'B'), cells);
            AssertContains(new BurrowTransformer.Cell(5, 5, 'D'), cells);
            AssertContains(new BurrowTransformer.Cell(7, 2, 'B'), cells);
            AssertContains(new BurrowTransformer.Cell(7, 3, 'B'), cells);
            AssertContains(new BurrowTransformer.Cell(7, 4, 'A'), cells);
            AssertContains(new BurrowTransformer.Cell(7, 5, 'C'), cells);
            AssertContains(new BurrowTransformer.Cell(9, 2, 'D'), cells);
            AssertContains(new BurrowTransformer.Cell(9, 3, 'A'), cells);
            AssertContains(new BurrowTransformer.Cell(9, 4, 'C'), cells);
            AssertContains(new BurrowTransformer.Cell(9, 5, 'A'), cells);
        }

        private static void AssertRoomPart2()
        {
            var cells = new BurrowTransformer(true).TargetRoom(".......BDDACCBDBBACDACA", 'A');
            Assert(4, cells.Count());
            AssertContains(new BurrowTransformer.Cell(3, 2, 'B'), cells);
            AssertContains(new BurrowTransformer.Cell(3, 3, 'D'), cells);
            AssertContains(new BurrowTransformer.Cell(3, 4, 'D'), cells);
            AssertContains(new BurrowTransformer.Cell(3, 5, 'A'), cells);

            cells = new BurrowTransformer(true).TargetRoom(".......BDDACCBDBBACDACA", 'B');
            Assert(4, cells.Count());
            AssertContains(new BurrowTransformer.Cell(5, 2, 'C'), cells);
            AssertContains(new BurrowTransformer.Cell(5, 3, 'C'), cells);
            AssertContains(new BurrowTransformer.Cell(5, 4, 'B'), cells);
            AssertContains(new BurrowTransformer.Cell(5, 5, 'D'), cells);

            cells = new BurrowTransformer(true).TargetRoom(".......BDDACCBDBBACDACA", 'C');
            Assert(4, cells.Count());
            AssertContains(new BurrowTransformer.Cell(7, 2, 'B'), cells);
            AssertContains(new BurrowTransformer.Cell(7, 3, 'B'), cells);
            AssertContains(new BurrowTransformer.Cell(7, 4, 'A'), cells);
            AssertContains(new BurrowTransformer.Cell(7, 5, 'C'), cells);

            cells = new BurrowTransformer(true).TargetRoom(".......BDDACCBDBBACDACA", 'D');
            Assert(4, cells.Count());
            AssertContains(new BurrowTransformer.Cell(9, 2, 'D'), cells);
            AssertContains(new BurrowTransformer.Cell(9, 3, 'A'), cells);
            AssertContains(new BurrowTransformer.Cell(9, 4, 'C'), cells);
            AssertContains(new BurrowTransformer.Cell(9, 5, 'A'), cells);
        }

        private static void AssertIsDonePart2()
        {
            Assert(false, new BurrowTransformer(true).IsDone("...................DDDC", new BurrowTransformer.Cell(9, 4, 'D')));
            Assert(false, new BurrowTransformer(true).IsDone("...................DDDC", new BurrowTransformer.Cell(9, 3, 'D')));
            Assert(false, new BurrowTransformer(true).IsDone("...................DDDC", new BurrowTransformer.Cell(9, 2, 'D')));
            Assert(true, new BurrowTransformer(true).IsDone("...................DDCD", new BurrowTransformer.Cell(9, 5, 'D')));
            Assert(false, new BurrowTransformer(true).IsDone("...................DDCD", new BurrowTransformer.Cell(9, 3, 'D')));
            Assert(false, new BurrowTransformer(true).IsDone("...................DDCD", new BurrowTransformer.Cell(9, 2, 'D')));
            Assert(false, new BurrowTransformer(true).IsDone("...................CCCC", new BurrowTransformer.Cell(9, 5, 'D')));
            Assert(false, new BurrowTransformer(true).IsDone(".......................", new BurrowTransformer.Cell(9, 5, 'D')));
            Assert(true, new BurrowTransformer(true).IsDone("...................DDDD", new BurrowTransformer.Cell(9, 2, 'D')));
            Assert(true, new BurrowTransformer(true).IsDone("...................DDDD", new BurrowTransformer.Cell(9, 3, 'D')));
            Assert(true, new BurrowTransformer(true).IsDone("...................DDDD", new BurrowTransformer.Cell(9, 4, 'D')));
            Assert(true, new BurrowTransformer(true).IsDone("...................DDDD", new BurrowTransformer.Cell(9, 5, 'D')));
            Assert(true, new BurrowTransformer(true).IsDone("....................DDD", new BurrowTransformer.Cell(9, 5, 'D')));
            Assert(true, new BurrowTransformer(true).IsDone(".....................DD", new BurrowTransformer.Cell(9, 5, 'D')));
            Assert(true, new BurrowTransformer(true).IsDone("......................D", new BurrowTransformer.Cell(9, 5, 'D')));
            Assert(true, new BurrowTransformer(true).IsDone("...................CCCD", new BurrowTransformer.Cell(9, 5, 'D')));
            Assert(true, new BurrowTransformer(true).IsDone("...................CCDD", new BurrowTransformer.Cell(9, 5, 'D')));
            Assert(true, new BurrowTransformer(true).IsDone("...................CDDD", new BurrowTransformer.Cell(9, 5, 'D')));
        }

        private static void AssertTryGetTargetCellPart2()
        {
            Assert(false, new BurrowTransformer(true).TryGetTargetCell("...................CDDD", 'D', out var c));
            Assert(false, new BurrowTransformer(true).TryGetTargetCell("...................CCCD", 'D', out c));
            Assert(false, new BurrowTransformer(true).TryGetTargetCell(".....................CD", 'D', out c));
            Assert(false, new BurrowTransformer(true).TryGetTargetCell("......................C", 'D', out c));
            Assert(true, new BurrowTransformer(true).TryGetTargetCell(".......................", 'D', out c));
            Assert(new BurrowTransformer.Cell(9, 5, '.'), c);
            Assert(true, new BurrowTransformer(true).TryGetTargetCell("......................D", 'D', out c));
            Assert(new BurrowTransformer.Cell(9, 4, '.'), c);
            Assert(true, new BurrowTransformer(true).TryGetTargetCell(".....................DD", 'D', out c));
            Assert(new BurrowTransformer.Cell(9, 3, '.'), c);
            Assert(true, new BurrowTransformer(true).TryGetTargetCell("....................DDD", 'D', out c));
            Assert(new BurrowTransformer.Cell(9, 2, '.'), c);
        }


        private static void Assert<T>(T expected, T actual) where T : notnull
        {
            if (!expected.Equals(actual))
            {
                throw new Exception($"Expected {expected} but was {actual}");
            }
        }

        private static void AssertContains<T>(T expected, IEnumerable<T> actual) where T : notnull
        {
            if (!actual.Contains(expected))
            {
                throw new Exception($"Expected to find {expected} in list");
            }
        }
    }
}
