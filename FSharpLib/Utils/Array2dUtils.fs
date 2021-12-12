namespace Lib.Utils

module Array2dUtils =

    let validCoord m (x, y) =
        let w = Array2D.length1 m
        let h = Array2D.length2 m
        0 <= x && x < w && 0 <= y && y < h

    let allNeighbors4 (x, y) = [(x, y-1); (x-1, y); (x+1, y); (x, y+1)]

    let validNeighbors4 m (x, y) = allNeighbors4 (x, y) |> List.filter (validCoord m)

    let allNeighbors8 (x, y) = [(x-1, y-1); (x, y-1); (x+1, y-1); (x-1, y); (x+1, y); (x-1, y+1); (x, y+1); (x+1, y+1)]
    
    let validNeighbors8 m (x, y) = allNeighbors8 (x, y) |> List.filter (validCoord m)

    let toListOfValues (m : 'a[,])= m |> Array2D.mapi (fun _ _ c -> c) |> Seq.cast<'a> |> Seq.toList

    let toList (m : 'a[,])= m |> Array2D.mapi (fun x y c -> (x, y, c)) |> Seq.cast<(int * int * 'a)> |> Seq.toList

    let count m = Array2D.length1 m * Array2D.length2 m
