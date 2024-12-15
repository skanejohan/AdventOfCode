using CSharpLib;
using CSharpLib.DataStructures;
using CSharpLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Y2024.Day15;

public static class Solver
{
    public static long Part1()
    {
        var (Walls, Boxes, Pos, Moves) = LoadData("Data.txt", part2: false);
        foreach (var m in Moves)
        {
            Pos = Move1(Walls, Boxes, Pos, m);
        }
        return Boxes.Select(b => b.Row * 100 + b.Col).Sum();
    }

    public static long Part2()
    {
        var (Walls, Boxes, Pos, Moves) = LoadData("Data.txt", part2: true);
        foreach (var m in Moves)
        {
            Pos = Move2(Walls, Boxes, Pos, m);
        }
        return Boxes.Select(b => b.Row * 100 + b.Col).Sum();
    }

    static (int Row, int Col) Move1(HashSet<(int Row, int Col)> walls, HashSet<(int Row, int Col)> boxes, (int Row, int Col) pos, char move)
    {
        (int DR, int DC) = move switch
        {
            '<' => (+0, -1),
            '>' => (+0, +1),
            '^' => (-1, +0),
            _ => (+1, +0)
        };
        var target = (pos.Row + DR, pos.Col + DC);
        if (boxes.Contains(target))
        {
            MoveBox(target);
        }
        if (walls.Contains(target) || boxes.Contains(target))
        {
            return pos;
        }
        return (pos.Row + DR, pos.Col + DC);

        void MoveBox((int Row, int Col) box)
        {
            var target = (box.Row + DR, box.Col + DC);
            if (boxes.Contains(target))
            {
                MoveBox(target);
            }
            if (walls.Contains(target) || boxes.Contains(target))
            {
                return;
            }
            boxes.Remove(box);
            boxes.Add(target);
        }
    }

    static (int Row, int Col) Move2(HashSet<(int Row, int Col)> walls, HashSet<(int Row, int Col)> boxes, (int Row, int Col) pos, char move)
    {
        HashSet<(int Row, int Col)> blockingBoxes = [];

        foreach (var wallPosition in WallPositionsBlockingRobotAt(pos))
        {
            if (walls.Contains(wallPosition))
            {
                return pos;
            }
        }
        foreach (var boxPosition in BoxPositionsBlockingRobotAt(pos))
        {
            if (boxes.Contains(boxPosition))
            {
                blockingBoxes.Add(boxPosition);
                if (!MoveBox(boxPosition))
                {
                    return pos;
                }
            }
        }

        var boxesToAdd = blockingBoxes.Select(NewPosition).ToList();
        foreach (var box in blockingBoxes)
        {
            boxes.Remove(box);
        }
        foreach(var box in boxesToAdd)
        {
            boxes.Add(box);
        }
        return NewPosition(pos);

        bool MoveBox((int Row, int Col) box)
        {
            foreach (var wallPosition in WallPositionsBlockingBoxAt(box))
            {
                if (walls.Contains(wallPosition))
                {
                    return false;
                }
            }
            
            foreach (var boxPosition in BoxPositionsBlockingBoxAt(box))
            {
                if (boxes.Contains(boxPosition))
                {
                    blockingBoxes.Add(boxPosition);
                    if (!MoveBox(boxPosition))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        List<(int Row, int Col)> BoxPositionsBlockingRobotAt((int R, int C) p) => move switch
        {
            '<' => [(p.R, p.C - 2)],
            '>' => [(p.R, p.C + 1)],
            '^' => [(p.R - 1, p.C - 1), (p.R - 1, p.C)],
            _   => [(p.R + 1, p.C - 1), (p.R + 1, p.C)]
        };

        List<(int Row, int Col)> WallPositionsBlockingRobotAt((int R, int C) p) => move switch
        {
            '<' => [(p.R, p.C - 1)],
            '>' => [(p.R, p.C + 1)],
            '^' => [(p.R - 1, p.C)],
            _   => [(p.R + 1, p.C)]
        };

        List<(int Row, int Col)> BoxPositionsBlockingBoxAt((int R, int C) p) => move switch
        {
            '<' => [(p.R, p.C - 2)],
            '>' => [(p.R, p.C + 2)],
            '^' => [(p.R - 1, p.C - 1), (p.R - 1, p.C), (p.R - 1, p.C + 1)],
            _   => [(p.R + 1, p.C - 1), (p.R + 1, p.C), (p.R + 1, p.C + 1)]
        };

        List<(int Row, int Col)> WallPositionsBlockingBoxAt((int R, int C) p) => move switch
        {
            '<' => [(p.R, p.C - 1)],
            '>' => [(p.R, p.C + 2)],
            '^' => [(p.R - 1, p.C), (p.R - 1, p.C + 1)],
            _   => [(p.R + 1, p.C), (p.R + 1, p.C + 1)]
        };

        (int Row, int Col) NewPosition((int R, int C) p) => move switch
        {
            '<' => (p.R, p.C - 1),
            '>' => (p.R, p.C + 1),
            '^' => (p.R - 1, p.C),
            _   => (p.R + 1, p.C)
        };
    }

    static (HashSet<(int Row, int Col)> Walls, HashSet<(int Row, int Col)> Boxes, (int Row, int Col) Pos, string Moves) LoadData(string fileName, bool part2)
    {
        (int Row, int Col) pos = (0, 0);
        HashSet<(int Row, int Col)> walls = [];
        HashSet<(int Row, int Col)> boxes = [];

        var data = new DataLoader("2024", 15).ReadStrings(fileName).ChunkBy(s => s.Trim() == "").ToList();

        var boxWidth = part2 ? 2 : 1;
        foreach (var (Row, Col, Value) in new Grid<char>(data[0]))
        {
            switch (Value)
            {
                case '#':
                    for (var c = 0; c < boxWidth; c++)
                    {
                        walls.Add((Row, boxWidth * Col + c));
                    }
                    break;
                case 'O':
                    boxes.Add((Row, boxWidth * Col));
                    break;
                case '@':
                    pos = (Row, boxWidth * Col);
                    break;
                default:
                    break;
            }
        }

        return (walls, boxes, pos, data[1][0]);
    }

    static void PrintWarehouse(HashSet<(int Row, int Col)> walls, HashSet<(int Row, int Col)> boxes, (int Row, int Col) pos, bool part2)
    {
        var maxRow = walls.Select(w => w.Row).Max();
        var maxCol = walls.Select(w => w.Col).Max();
        for (var r = 0; r <= maxRow; r++)
        {
            for (var c = 0; c <= maxCol; c++)
            {
                if (walls.Contains((r, c)))
                {
                    Console.Write("#");
                }
                else if (boxes.Contains((r, c)))
                {
                    if (part2)
                    {
                        Console.Write("[]");
                        c++;
                    }
                    else 
                    {
                        Console.Write("O");
                    }
                }
                else if (pos == (r, c))
                {
                    Console.Write("@");
                }
                else
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
