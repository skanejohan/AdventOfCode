namespace AdventOfCode2020

open Library
open Microsoft.FSharp.Collections
open Microsoft.FSharp.Core

module Day12 =

    let parseLine line = 
        match line with
        | Regex @"([A-Z])([0-9]+)" [ d; n ] -> (char d, int n)
        | _                                 -> ('E', 0)
    
    // State is (int * int * char) representing x and y, and direction
    let update1 state move =
        let left d deg = 
            match (d, deg) with 
            | ('N', 90) -> 'W'
            | ('N', 180) -> 'S'
            | ('N', 270) -> 'E'
            | ('W', 90) -> 'S'
            | ('W', 180) -> 'E'
            | ('W', 270) -> 'N'
            | ('S', 90) -> 'E'
            | ('S', 180) -> 'N'
            | ('S', 270) -> 'W'
            | ('E', 90) -> 'N'
            | ('E', 180) -> 'W'
            | (_, _)   -> 'S'
        let right d deg = 
            match (d, deg) with 
            | ('N', 90) -> 'E'
            | ('N', 180) -> 'S'
            | ('N', 270) -> 'W'
            | ('W', 90) -> 'N'
            | ('W', 180) -> 'E'
            | ('W', 270) -> 'S'
            | ('S', 90) -> 'W'
            | ('S', 180) -> 'N'
            | ('S', 270) -> 'E'
            | ('E', 90) -> 'S'
            | ('E', 180) -> 'W'
            | (_, _)   -> 'N'
        let forward (x, y, d) n =
            match d with 
            | 'N' -> (x, y+n, d)
            | 'W' -> (x-n, y, d)
            | 'S' -> (x, y-n, d)
            | _   -> (x+n, y, d)
        let (x, y, d) = state
        match move with
            | ('N', n) -> (x, y+n, d)
            | ('W', n) -> (x-n, y, d)
            | ('S', n) -> (x, y-n, d)
            | ('E', n) -> (x+n, y, d)
            | ('L', deg) -> (x, y, left d deg)
            | ('R', deg) -> (x, y, right d deg)
            | (_, n)   -> forward (x, y, d) n

    let Part1 _ = 
        let (x, y, _) = 
            GetDataAsStringList "Day12Input.txt" |> 
            List.map parseLine |> 
            List.fold update1 (0, 0, 'E')
        abs x + abs y

    // State is ((int * int) * (int * int)) representing sx, sy, wx and y where
    // sx and sy are the ship's coordinates and wx and wy are offset to the waypoint
    let update2 state move =
        let left (x, y) n =
            match n with
            | 90  -> (-y, x)
            | 180 -> (-x, -y)
            | _   -> (y, -x)
        let right (x, y) n =
            match n with
            | 90  -> (y, -x)
            | 180 -> (-x, -y)
            | _   -> (-y, x)
        let ((sx, sy), (wx, wy)) = state
        match move with
            | ('N', n) -> ((sx, sy), (wx, wy+n))
            | ('W', n) -> ((sx, sy), (wx-n, wy))
            | ('S', n) -> ((sx, sy), (wx, wy-n))
            | ('E', n) -> ((sx, sy), (wx+n, wy))
            | ('L', n) -> ((sx, sy), left (wx, wy) n)
            | ('R', n) -> ((sx, sy), right (wx, wy) n)
            | (_, n)   -> ((sx + n * wx, sy + n * wy), (wx, wy))

    let Part2 _ =
        let ((sx, sy), _) = 
            GetDataAsStringList "Day12Input.txt" |>
            List.map parseLine |> 
            List.fold update2 ((0, 0), (10, 1))
        abs sx + abs sy
