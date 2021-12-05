namespace Lib.Geometry

open Point

module Line =

    type Line = Line of Point * Point with

        static member create x1 y1 x2 y2 = Line (Point (x1, y1), Point (x2, y2))

        static member empty = Line (Point (0, 0), Point (0, 0))

        member this.p1 = match this with Line (p1, _) -> p1 

        member this.p2 = match this with Line (_, p2) -> p2 

        static member isHorizontal (l : Line) = l.p1.y = l.p2.y

        static member isVertical (l : Line) = l.p1.x = l.p2.x

        static member isHorizontalOrVertical l = Line.isHorizontal l || Line.isVertical l

        static member isDiagonal (l : Line)  = abs(l.p1.x - l.p2.x) = abs (l.p1.y - l.p2.y)

        static member minX (l : Line) = min l.p1.x l.p2.x

        static member minY (l : Line) = min l.p1.y l.p2.y

        static member maxX (l : Line) = max l.p1.x l.p2.x

        static member maxY (l : Line) = max l.p1.y l.p2.y

        static member left (l : Line) = if l.p1.x < l.p2.x then l.p1 else l.p2

        static member right (l : Line) = if l.p1.x < l.p2.x then l.p2 else l.p1

        static member dX (l : Line) = (Line.right l).x - (Line.left l).x

        static member dY (l : Line) = (Line.right l).y - (Line.left l).y

        static member slope (l : Line) =
            if Line.isHorizontal l
            then 0f
            else if Line.isVertical l
                 then failwith "Can't calculate slope for vertical line"
                 else float32 (Line.dY l) / float32 (Line.dX l)

        static member allPoints (l : Line) =
            if Line.isHorizontalOrVertical l
            then [for x in Line.minX l..Line.maxX l do for y in Line.minY l..Line.maxY l do yield Point (x, y)]
            else if Line.isDiagonal l
                 then let dy = if (Line.left l).y < (Line.right l).y then 1 else -1
                      [for i in 0..Line.dX l do yield Point((Line.left l).x + i, (Line.left l).y + i * dy)]
            else failwith "allPoints only implemented for horizontal, vertcal and diagonal lines"
    