namespace AdventOfCode2015

open System.IO

module Library =

    let private dataFile fileName = 
        let dir = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\2015\Data\")
        Path.Combine(dir, fileName)

    let GetDataAsStringList fileName = File.ReadAllLines(dataFile fileName) |> Array.toList

    let GetDataAsByteList fileName = File.ReadAllBytes(dataFile fileName) |> Array.toList

    let GetDataAsCharList fileName = File.ReadAllBytes(dataFile fileName) |> Array.toList |> List.map char

    let GetDataAsCharArray fileName = File.ReadAllBytes(dataFile fileName) |> Array.map char
