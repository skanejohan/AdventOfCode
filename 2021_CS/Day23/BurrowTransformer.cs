using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2021_CS.Day23
{
    /// <summary>
    /// Externally, a burrow is represented by a string on the following format:
    /// ".......AAAABBBBCCCCDDDD". The first seven positions represent the seven 
    /// positions in the hall where an amphipod may land, i.e. not including the 
    /// four cells above the rooms. The next four positions (AAAA) represent the 
    /// amphipods in room A. The first position represents the top position in 
    /// room A, the last one represents the position at the bottom. Rooms B 
    /// through D are represented the same way. 
    /// 
    /// The example above represents the target configuration in part one of the task. 
    /// The string ".......AA..BB..CC..DD.." represents the target configuration in 
    /// part two. The strings ".......BA..CD..BC..DA.." and ".......BDDACCBDBBACDACA"
    /// represent examples of starting (or intermediate) configurations in each part
    /// respectively.
    /// 
    /// Internally, an cell in the burrow is represented by a (int X, int Y, char c) 
    /// "Cell". As an example, the burrow ".......BA..CD..BC..DA.." has the following 
    /// amphipods: (3, 2, 'B'), (3, 3, 'A'), (5, 2, 'C'), (5, 3, 'D'), (7, 2, 'B'), 
    /// (7, 3, 'C'), (9, 2, 'D') and (9, 3, 'A') but it has a number of additional 
    /// empty cells, whose character is '.'.
    /// </summary>
    internal class BurrowTransformer
    {
        public BurrowTransformer(bool largeRooms)
        {
            roomStartIndices = new Dictionary<char, int>
            {
                { 'A', 7 },
                { 'B', 11 },
                { 'C', 15 },
                { 'D', 19 }
            };
            roomEndIndices = new Dictionary<char, int>
            {
                { 'A', largeRooms ? 10 : 8 },
                { 'B', largeRooms ? 14 : 12 },
                { 'C', largeRooms ? 18 : 16 },
                { 'D', largeRooms ? 22 : 20 }
            };
            roomStartIndices.Add('.', 1);
            roomEndIndices.Add('.', 0);
        }

        /// <summary>
        /// Return all possible neighbors (i.e. all burrows resulting from making each 
        /// of the possible moves in the original burrow. With each returned new burrow 
        /// is also included the cost of making the move that resulted in it.
        /// </summary>
        public IEnumerable<(string Burrow, long Cost)> Neighbors(string burrow)
        {
            foreach (var from in GetAmphipods(burrow))
            {
                foreach (var to in PossibleMovesFor(burrow, from))
                {
                    var s = new StringBuilder(burrow);
                    s[IndexGivenPosition[(from.X, from.Y)]] = '.';
                    s[IndexGivenPosition[(to.X, to.Y)]] = from.C;
                    var cost = (int)Math.Pow(10, from.C - 'A');
                    var dist = from.Y - 1 + Math.Abs(to.X - from.X) + to.Y - 1;
                    yield return (s.ToString(), cost * dist);
                }
            }
        }

        /// <summary>
        /// Given a burrow and an amphipod, what are the possible moves that can be made?
        /// </summary>
        internal IEnumerable<Cell> PossibleMovesFor(string burrow, Cell amphipod)
        {
            if (!IsDone(burrow, amphipod))
            {
                List<Cell> candidates = new List<Cell>();
                if (TryGetTargetCell(burrow, amphipod.C, out var cell))
                {
                    candidates.Add(cell);
                }
                if (InRoom(burrow, amphipod))
                {
                    candidates = Hall(burrow).Where(c => c.C == '.').ToList();
                }
                foreach (var candidate in candidates.Where(c => MoveIsAllowed(burrow, amphipod, c)))
                {
                    yield return candidate;
                }
            }
        }

        /// <summary>
        /// Is the amphipod in a room (regardless of which)?
        /// </summary>
        internal bool InRoom(string burrow, Cell amphipod)
        {
            return Rooms(burrow).Contains(amphipod);
        }

        /// <summary>
        /// Is the amphipod done? This is true if it is in its target room and there
        /// are no amphipods of another type below it in that room.
        /// </summary>
        internal bool IsDone(string burrow, Cell amphipod)
        {
            var room = TargetRoom(burrow, amphipod.C);
            var cellsBelowAmphipod = room.Where(c => c.Y > amphipod.Y);
            return room.Contains(amphipod) && CellsOnlyContain(cellsBelowAmphipod, amphipod.C);
        }

        /// <summary>
        /// If the target room does not contain any amphipods of incorrect type and there is 
        /// still available space, return the available cell furthest down in the room.
        /// </summary>
        internal bool TryGetTargetCell(string burrow, char amphipodType, out Cell cell)
        {
            cell = new Cell(0, 0, '.');
            var targetRoom = TargetRoom(burrow, amphipodType);
            if (!CellsOnlyContain(targetRoom, amphipodType))
            {
                return false;
            }
            var cells = targetRoom.Where(c => c.C == '.');
            if (cells.Any())
            {
                cell = cells.Last();
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Does the list of cells contain only amphipods of the given type (or are empty)?
        /// </summary>
        internal bool CellsOnlyContain(IEnumerable<Cell> cells, char amphipodType)
        {
            return cells.All(c => c.C == amphipodType || c.C == '.');
        }

        /// <summary>
        /// Verifies that all cells between from and to are empty.
        /// </summary>
        internal bool MoveIsAllowed(string burrow, Cell from, Cell to)
        {
            var minX = Math.Min(from.X, to.X);
            var maxX = Math.Max(from.X, to.X);
            var cellsBetween = Hall(burrow).Where(c => c.X > minX && c.X < maxX)
                .Concat(RoomAt(burrow, from.X).Where(c => c.Y < from.Y))
                .Concat(TargetRoom(burrow, to.C).Where(c => c.Y < to.Y));
            return cellsBetween.All(c => c.C == '.');
        }

        /// <summary>
        /// Return the cells making up the hall.
        /// </summary>
        internal IEnumerable<Cell> Hall(string burrow)
        {
            for (var i = 0; i < 7; i++)
            {
                var (X, Y) = PositionGivenIndex[i];
                yield return new Cell(X, Y, burrow[i]);
            }
        }

        /// <summary>
        /// Return the cells making up all the rooms.
        /// </summary>
        internal IEnumerable<Cell> Rooms(string burrow)
        {
            return TargetRoom(burrow, 'A').Concat(TargetRoom(burrow, 'B')).Concat(TargetRoom(burrow, 'C')).Concat(TargetRoom(burrow, 'D'));
        }

        /// <summary>
        /// Return the cells making up the room with given X coordinate.
        /// </summary>
        internal IEnumerable<Cell> RoomAt(string burrow, int x)
        {
            return Rooms(burrow).Where(c => c.X == x);
        }

        /// <summary>
        /// Return the cells making up the target room for the selected amphipod type.
        /// </summary>
        internal IEnumerable<Cell> TargetRoom(string burrow, char amphipodType)
        {
            for (var i = roomStartIndices[amphipodType]; i <= roomEndIndices[amphipodType]; i++)
            {
                var (X, Y) = PositionGivenIndex[i];
                yield return new Cell(X, Y, burrow[i]);
            }
        }

        /// <summary>
        /// Return all amphipods in the given burrow.
        /// </summary>
        internal IEnumerable<Cell> GetAmphipods(string burrow)
        {
            for (int i = 0; i < burrow.Length; i++)
            {
                if (burrow[i] != '.')
                {
                    var (X, Y) = PositionGivenIndex[i];
                    yield return new Cell(X, Y, burrow[i]);
                }
            }
        }

        private static readonly (int X, int Y)[] PositionGivenIndex = 
        { 
            (1, 1), (2, 1), (4, 1), (6, 1), (8, 1), (10, 1), (11, 1),
            (3, 2), (3, 3), (3, 4), (3, 5),
            (5, 2), (5, 3), (5, 4), (5, 5),
            (7, 2), (7, 3), (7, 4), (7, 5),
            (9, 2), (9, 3), (9, 4), (9, 5)
        };

        private static readonly Dictionary<(int X, int Y), int> IndexGivenPosition = new Dictionary<(int, int), int>
        {
            { (1, 1), 0 }, { (2, 1), 1 }, { (4, 1), 2 }, { (6, 1), 3 }, { (8, 1), 4 }, { (10, 1), 5 }, { (11, 1), 6 },
            { (3, 2), 7 }, { (3, 3), 8 }, { (3, 4), 9 },{ (3, 5), 10 },
            { (5, 2), 11 }, { (5, 3), 12 }, { (5, 4), 13 },{ (5, 5), 14 },
            { (7, 2), 15 }, { (7, 3), 16 }, { (7, 4), 17 },{ (7, 5), 18 },
            { (9, 2), 19 }, { (9, 3), 20 }, { (9, 4), 21 },{ (9, 5), 22 },
        };

        private Dictionary<char, int> roomStartIndices;
        private Dictionary<char, int> roomEndIndices;

        internal class Cell
        {
            public int X { get; }
            public int Y { get; }
            public char C { get; }

            public Cell(int x, int y, char c)
            {
                X = x;
                Y = y;
                C = c;
            }
            
            public override bool Equals(object obj)
            {
                return obj != null && obj is Cell other && this.X == other.X && this.Y == other.Y && this.C == other.C;
            }

            public override int GetHashCode()
            {
                return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.C.GetHashCode();
            }

            public static bool operator ==(Cell c1, Cell c2)
            {
                return Object.Equals(c1, c2);
            }

            public static bool operator !=(Cell c1, Cell c2)
            {
                return !Object.Equals(c1, c2);
            }
        }
    }
}
