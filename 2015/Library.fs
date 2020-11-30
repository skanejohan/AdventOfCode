namespace AdventOfCode2015

open System.IO

module Library =

    let private dataFile fileName = 
        let dir = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\2015\Data\")
        Path.Combine(dir, fileName)

    let GetDataAsByteList fileName = File.ReadAllBytes(dataFile fileName) |> Array.toList
