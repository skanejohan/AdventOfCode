namespace Lib.Geometry

module Point =

    type Point = Point of int * int with

        static member get (Point (x, y)) = (x, y)

        member this.x = match this with Point (x, _) -> x 

        member this.y = match this with Point (_, y) -> y 
