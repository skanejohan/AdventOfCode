﻿namespace AdventOfCode2021

open System.IO

module Spec2021 =

    let withPath fileName = 
        let dir = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\2021\Data\")
        Path.Combine(dir, fileName)
