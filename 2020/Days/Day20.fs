namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Core

module Day20 =
    
    type Tile = Tile of int * char[,]

    let id (Tile (i, _)) = i
    let g (Tile (_, g)) = g
    let n t = (g t).[0, *]
    let e t = (g t).[*, 9]
    let s t = (g t).[9, *] |> Array.rev
    let w t = (g t).[*, 0] |> Array.rev
    let ch x y t = (g t).[x + 1, y + 1]

    let tile rots flip t = // Create a new tile, rotated rots times and possibly flipped
        let rotate g = let h, w = Array2D.length1 g, Array2D.length2 g in Array2D.init w h (fun r c -> Array2D.get g (h - c - 1) r)
        let rec rotaten n g = 
            match n with
            | 0 -> g
            | _ -> rotaten (n-1) (rotate g)
        let transpose (g : _ [,]) = Array2D.init (g.GetLength 1) (g.GetLength 0) (fun x y -> g.[y,x])
        let g' = if flip then rotaten rots (transpose (g t)) else rotaten rots (g t)
        Tile (id t, g')

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

    let parseTiles data = 
        let extractId s =
            match s with
            | Regex @"^Tile ([0-9][0-9][0-9][0-9]):$" [ id ] -> int id
            | _                                            -> 0
        let parseChunk c =
            let id = c |> List.head |> extractId
            let grid = c |> List.tail |> List.take 10 |> array2D
            Tile (id, grid)
        data |> List.chunkBySize 12 |> List.map parseChunk

    let buildGrid tiles =  // Build a valid grid
        let rec build (x, y) grid tiles =
            let add p (g, ts) = match List.tryFind (valid p g) (allTiles ts) with
                                | Some t -> build p (Map.add p t g) (List.filter (fun t' -> id t' <> id t) ts)
                                | None   -> (g, ts)
            let addAll = add (x+1, y) >> add (x, y+1) >> add (x-1, y) >> add (x, y-1)
            addAll (grid, tiles)
        let hd = List.head tiles
        let tl = List.tail tiles
        let grid = Map.add (0, 0) hd Map.empty
        build (0, 0) grid tl |> fst

    let corners grid =
        let keys = Map.toList >> List.map fst
        let minX = keys >> List.map fst >> List.min
        let maxX = keys >> List.map fst >> List.max
        let minY = keys >> List.map snd >> List.min
        let maxY = keys >> List.map snd >> List.max
        (minX grid, minY grid, maxX grid, maxY grid)

    let Part1 () =         
        let g = GetDataAsStringList "Day20Input.txt" |> parseTiles |> buildGrid

        let (minX, minY, maxX, maxY) = corners g
        let id1 = id (Map.find (minX, minY) g) |> int64 
        let id2 = id (Map.find (maxX, minY) g) |> int64
        let id3 = id (Map.find (minX, maxY) g) |> int64
        let id4 = id (Map.find (maxX, maxY) g) |> int64
        
        id1 * id2 * id3 * id4

    let allCoords w h = [for x in 0..w-1 do for y in 0..h-1 do yield (x, y)]

    let assemblePicture grid = // Generate a picture of type Map<(int * int), char> given a map of tiles
        let addTile xOffset yOffset tile (pic : Map<(int * int), char>) =
            let addOne (x, y) map = Map.add (x+xOffset, y+yOffset) (ch x y tile) map
            let rec add coords map = 
                match coords with 
                | c :: cs -> add cs (addOne c map)
                | _       -> map
            add [for xc in 0..7 do for yc in 0..7 do yield (xc, yc)] pic
        let (minX, minY, maxX, maxY) = corners grid
        let w = maxX - minX + 1
        let h = maxY - minY + 1
        let xOffset = -minX
        let yOffset = -minY
        let rec loop coords pic =
            match coords with
            | (x, y) :: cs -> pic |> addTile (x*8) (y*8) (Map.find (y-xOffset, x-yOffset) grid) |> loop cs
            | _            -> pic
        let emptyPic = Map.ofList [for x in 0..w*8-1 do for y in 0..h*8-1 do yield ((x, y), '.')]
        let pic = loop (allCoords w h) emptyPic
        (pic, w*8, h*8)
         
    let isHash pic (x, y) =
        match Map.tryFind (x, y) pic with
        | Some c -> c = '#'
        | None   -> false

    let monsterAt pic (x, y) =
        List.forall (isHash pic) [(x+18, y); (x, y+1); (x+5, y+1); (x+6, y+1); (x+11, y+1); (x+12, y+1); (x+17, y+1); (x+18, y+1); (x+19, y+1); (x+1, y+2); (x+4, y+2); (x+7, y+2); (x+10, y+2); (x+13, y+2); (x+16, y+2)]

    let monsterCount w h pic = allCoords w h |> List.filter (monsterAt pic) |> List.length

    let hashCount w h pic = allCoords w h |> List.filter (isHash pic) |> List.length

    let allPics w h pic = // Get all possible configurations (rotations and flip) for a pic
        let flipPic w h p =
            let rec loop coords ack =
                match coords with
                | (x, y) :: cs -> loop cs (Map.add (y, x) (Map.find (x, y) p) ack)
                | _            -> ack
            loop (allCoords w h) Map.empty
        let rotatePic w h p =
            let rec loop coords ack =
                match coords with
                | (x, y) :: cs -> loop cs (Map.add (y, w-x-1) (Map.find (x, y) p) ack)
                | _            -> ack
            loop (allCoords w h) Map.empty
        let transformPic rots flip w h p =
            let rec rotaten n p = 
                match n with
                | 0 -> p
                | _ -> rotaten (n-1) (rotatePic w h p)
            if flip then rotaten rots (flipPic w h p) else rotaten rots p
        let transforms p = [for f in [false; true] do for n in 0..3 do yield transformPic n f w h p]
        transforms pic

    let Part2 () =
        let grid = GetDataAsStringList "Day20Input.txt" |> parseTiles |> buildGrid
        let (pic, w, h) = assemblePicture grid
        let noOfHashes = hashCount w h pic
        let noOfMonsters = allPics w h pic |> List.map (monsterCount w h) |> List.max
        noOfHashes - 15 * noOfMonsters
