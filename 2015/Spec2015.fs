namespace AdventOfCode2015

open System.IO

module Spec2015 =

    let withPath fileName = 
        let dir = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\2015\Data\")
        Path.Combine(dir, fileName)
