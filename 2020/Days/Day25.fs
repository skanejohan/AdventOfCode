namespace AdventOfCode2020

open Microsoft.FSharp.Core

module Day25 =

    let transformStep v sn = (v * sn) % 20201227L

    let findLoopSize sn key =
        let rec fls v n =
            let v2 = transformStep v sn
            if v2 = key 
            then n
            else fls v2 (n+1L)
        fls 1L 1L

    let transform sn n = 
        let rec t v sn n = 
            if n = 0L
            then v
            else t (transformStep v sn) sn (n-1L)
        t 1L sn n

    let Part1 () = 
        let pk1 = 8252394L
        let pk2 = 6269621L
        let loopSize1 = findLoopSize 7L pk1
        let loopSize2 = findLoopSize 7L pk2
        transform pk1 loopSize2
