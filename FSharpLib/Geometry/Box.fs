namespace Lib.Geometry

module Box =

    // Represents a three-dimensional box in a world where boxes may overlap
    type Box = Box of int * int * int * int * int * int with
    
        // The box representing the overlap of b1 and b2
        static member overlap b1 b2 =
            let (minX1, maxX1, minY1, maxY1, minZ1, maxZ1) = b1
            let (minX2, maxX2, minY2, maxY2, minZ2, maxZ2) = b2
            let minX = max minX1 minX2
            let maxX = min maxX1 maxX2
            let minY = max minY1 minY2
            let maxY = min maxY1 maxY2
            let minZ = max minZ1 minZ2
            let maxZ = min maxZ1 maxZ2
            if minX > maxX || minY > maxY || minZ > maxZ
            then None
            else Some (minX, maxX, minY, maxY, minZ, maxZ)
    
        // All boxes representing overlap with the first box
        static member totalOverlaps box boxes = 
            let rec tOverlaps box boxes = 
                match boxes with 
                | b :: bs -> match Box.overlap box b with
                             | Some bb -> bb :: tOverlaps box bs
                             | _       -> tOverlaps box bs
                | _       -> []
            tOverlaps box boxes

        // The volume of a box
        static member volume b =
            let (minX, maxX, minY, maxY, minZ, maxZ) = b
            bigint (maxX + 1 - minX) * bigint (maxY + 1 - minY) * bigint (maxZ + 1 - minZ)

        // The total volume of all boxes, taking into account that they may overlap
        static member totalVolume boxes =
            let rec tVolume boxes =
                match boxes with
                | b :: bs -> let overlaps = Box.totalOverlaps b bs
                             Box.volume b + tVolume bs - tVolume overlaps
                | _       -> bigint 0
            tVolume boxes


