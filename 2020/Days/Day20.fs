namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Core

module Day20 =

    type Tile = Tile of int * char [] * char [] * char [] * char []
    
    let id (Tile (x, _, _, _, _)) = x
    let n  (Tile (_, x, _, _, _)) = x
    let e  (Tile (_, _, x, _, _)) = x
    let s  (Tile (_, _, _, x, _)) = x
    let w  (Tile (_, _, _, _, x)) = x

    let tile rots flip t = // Create a new tile, rotated rots times and possibly flipped
        let rotate t = Tile (id t, w t, n t, e t, s t)
        let rec rotaten n t = 
            match n with
            | 0 -> t
            | _ -> rotaten (n-1) (rotate t)
        let flipped t = Tile (id t, n t |> Array.rev, w t |> Array.rev, s t |> Array.rev, e t |> Array.rev)
        if flip then rotaten rots (flipped t) else rotaten rots t

    let allTiles ts = // Get all possible configurations (rotations and flip) for a tile
        let tiles t = [for f in [false; true] do for n in 0..3 do yield tile n f t]
        ts |> List.map tiles |> List.concat

    let valid (x, y) g t = // Will placing the tile at the given position in the grid result in a valid grid?
        let isEmpty pos = not (Map.containsKey pos g)
        let getTile pos = Map.find pos g
        isEmpty (x, y) 
            && (isEmpty (x, y-1) || s (getTile (x, y-1)) |> Array.rev = n t)
            && (isEmpty (x+1, y) || w (getTile (x+1, y)) |> Array.rev = e t)
            && (isEmpty (x, y+1) || n (getTile (x, y+1)) |> Array.rev  = s t)
            && (isEmpty (x-1, y) || e (getTile (x-1, y)) |> Array.rev  = w t)

    let rec build (x, y) grid tiles = // Build a valid grid
        let add p (g, ts) = match List.tryFind (valid p g) (allTiles ts) with
                            | Some t -> build p (Map.add p t g) (List.filter (fun t' -> id t' <> id t) ts)
                            | None   -> (g, ts)
        let addAll = add (x+1, y) >> add (x, y+1) >> add (x-1, y) >> add (x, y-1)
        addAll (grid, tiles)

    let parseTiles data = 
        let extractId s =
            match s with
            | Regex @"^Tile ([0-9][0-9][0-9][0-9]):$" [ id ] -> int id
            | _                                            -> 0
        let parseChunk c =
            let id = c |> List.head |> extractId
            let grid = c |> List.tail |> List.take 10 |> array2D
            Tile (id, grid.[0, *], grid.[*, 9], grid.[9, *] |> Array.rev, grid.[*, 0] |> Array.rev)
        data |> List.chunkBySize 12 |> List.map parseChunk

    // Debugging

    let printableTile t = (id t, new string(n t), new string(e t), new string(s t), new string(w t))
    let dumpTile = printableTile >> printfn "%A"
    let dumpTiles = List.map dumpTile
    let dumpGrid g =
        let dumpKeyValue (k, v) = printfn "%A : %A" k (printableTile v)
        g |> Map.toList |> List.map dumpKeyValue |> ignore

    let Part1 () = 
        
        let data = GetDataAsStringList "Day20Input.txt" |> parseTiles
        let hd = List.head data
        let tl = List.tail data
        let grid = Map.add (0, 0) hd Map.empty
        let grid' = build (0, 0) grid tl |> fst

        let minX = grid' |> Map.toList |> List.map fst |> List.map fst |> List.min
        let maxX = grid' |> Map.toList |> List.map fst |> List.map fst |> List.max
        let minY = grid' |> Map.toList |> List.map fst |> List.map snd |> List.min
        let maxY = grid' |> Map.toList |> List.map fst |> List.map snd |> List.max

        let id1 = id (Map.find (minX, minY) grid') |> int64 
        let id2 = id (Map.find (maxX, minY) grid') |> int64
        let id3 = id (Map.find (minX, maxY) grid') |> int64
        let id4 = id (Map.find (maxX, maxY) grid') |> int64
        
        id1 * id2 * id3 * id4
