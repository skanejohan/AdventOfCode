namespace AdventOfCode2021

open Lib.DataLoader

module Day25 =

    type State = State of int * int * char[,] with
        static member FromState s = match s with State (w, h, a) -> State (w, h, Array2D.copy a)
        static member FromArray2D a = State (Array2D.length1 a, Array2D.length2 a, a)
        member this.width = match this with State (w, _, _) -> w
        member this.height = match this with State (_, h, _) -> h
        member this.get x y = match this with State (_, _, a) -> a.[x, y]
        member this.rightof x = (x + 1) % this.width
        member this.below y = (y + 1) % this.height
        static member set x y c s = match s with State (w, h, a) -> a.[x, y] <- c
                                                                    State (w, h, a)
        static member canStepRight (s : State) x y = s.get x y = '>' && s.get (s.rightof x) y = '.'
        static member stepRight (s : State) x y = s |> State.set x y '.' |> State.set (s.rightof x) y '>'
        static member canStepDown (s : State) x y = s.get x y = 'v' && s.get x (s.below y) = '.'
        static member stepDown (s : State) x y = s |> State.set x y '.' |> State.set x (s.below y) 'v'
        member this.dump =
            for y in [0..this.height-1] do
                for x in [0..this.width-1] do
                    printf "%c" (this.get x y)
                printfn ""

    let performStep canStep doStep (s : State) = 
        let mutable toMove = []
        for x in List.rev [0..s.width-1] do
            for y in List.rev [0..s.height-1] do
                if canStep s x y then toMove <- (x, y) :: toMove
        let mutable state = State.FromState s
        for (x, y) in toMove do
            state <- doStep state x y 
        state

    let step s = s |> performStep State.canStepRight State.stepRight |> performStep State.canStepDown State.stepDown

    let run state = 
        let rec runrec n s =
            let s2 = step s
            if s2 = s
            then n
            else runrec (n+1) s2
        runrec 1 state

    let parseInput fileName = getDataAsArray2D (Spec2021.withPath fileName) |> State.FromArray2D

    let Part1 () = parseInput "Day25Input.txt" |> run

    let Part2 () = 0