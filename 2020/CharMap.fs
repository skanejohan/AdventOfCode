namespace AdventOfCode2020

module CharMap =

    type CharMap = { rows: int; cols: int; data : Map<(int * int), char> }

    let CreateFromCharArray a =
        let rows = Array.length a
        let cols = Array.length a.[0]
        let rec proc col row (ack : Map<(int * int), char>) (data : char [] []) =
            if col = cols 
            then proc 0 (row+1) ack data
            else if row = rows
                 then ack
                 else let newAck = ack.Add ((row,col), data.[row].[col])
                      proc (col+1) row newAck data  
        { rows = rows; cols = cols; data = proc 0 0 Map.empty a }

    let Get row col (map : CharMap) = map.data.[(row, col)]

    let TryFind row col (map : CharMap) = Map.tryFind (row, col) map.data

    let Set row col c (map : CharMap) = { rows = map.rows; cols = map.cols; data = Map.add (row,col) c map.data }

    let Count c (map : CharMap) = 
        let mutable count = 0
        for row in 0..map.rows-1 do
            for col in 0..map.cols-1 do
                if Get row col map = c then count <- count + 1
        count

    let Dump (map : CharMap) = 
       for row in 0..map.rows-1 do
           for col in 0..map.cols-1 do
               Get row col map |> printf "%c" 
           printfn ""
