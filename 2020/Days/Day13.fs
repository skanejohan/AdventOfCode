namespace AdventOfCode2020

open Microsoft.FSharp.Collections
open Microsoft.FSharp.Core
open Library

module Day13 =

    let departsAt t b = t % b = 0

    let departingAt t bs = List.tryFind (departsAt t) bs

    let rec firstDeparture bs ts =
        match departingAt (Seq.head ts) bs with
        | Some b -> (Seq.head ts, b)
        | None   -> firstDeparture bs (Seq.tail ts)

    let Part1 _ = 
        let t = 1000052
        let bs = [23;37;863;19;13;17;29;571;41]
        let (time,bus) = firstDeparture bs (Seq.initInfinite (fun i -> i + t))
        (time - t) * bus

    let ok data offset t = List.fold (fun s (o,b) -> s && (t - offset + o) % b = 0L) true data

    //static long Part02()
    //{
    //    // 1106724616194525
    //    string[] bus = "23,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,37,x,x,x,x,x,863,x,x,x,x,x,x,x,x,x,x,x,19,13,x,x,x,17,x,x,x,x,x,x,x,x,x,x,x,29,x,571,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,41".Split(",");
    //    var time = 0L;
    //    var inc = long.Parse(bus[0]);
    //    for (var i = 1; i < bus.Length; i++)
    //    {
    //        if (!bus[i].Equals("x"))
    //        {
    //            var newTime = int.Parse(bus[i]);
    //            while (true)
    //            {
    //                time += inc;
    //                if ((time + i) % newTime == 0)
    //                {
    //                    inc *= newTime;
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //    return time;
    //}

    //            while (true)
    //            {
    //                time += inc;
    //                if ((time + i) % newTime == 0)
    //                {
    //                    inc *= newTime;
    //                    break;
    //                }
    //            }
    let rec update i newTime time inc = 
        if (time + i) % newTime = 0
        then (inc * newTime, time + inc)
        else update i newTime (time + inc) inc

    //let fn 

    let Part2 _ = 
        //let data = "23,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,37,x,x,x,x,x,863,x,x,x,x,x,x,x,x,x,x,x,19,13,x,x,x,17,x,x,x,x,x,x,x,x,x,x,x,29,x,571,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,41" 
        //let x = data.Split ',' |> Array.map strToInt

        //fn 0L (head x) tail x)

        //x |> Array.map (fun o -> match o with 
        //                         | Some i -> printfn "%d" i
        //                         | None   -> printfn "%s" "-") |> ignore 

        //let y = 0

        // [17,x,13,19] - 3417
        //let offset = 0L
        //let increment = 1L
        //let data = [(0L, 17L); (2L, 13L); (3L, 19L)]

        // [7,13,x,x,59,x,31,19] - 1068788
        //let offset = 7L
        //let increment = 19L
        //let data = [(0L, 7L); (1L, 13L); (4L, 59L); (6L, 31L); (7L, 19L)]

        // [67,7,59,61] - 754018
        //let offset = 0L
        //let increment = 1L
        //let data = [(0L, 67L); (1L, 7L); (2L, 59L); (3L, 61L)]

        // [67,x,7,59,61] - 779210
        //let offset = 0L
        //let increment = 1L
        //let data = [(0L, 67L); (2L, 7L); (3L, 59L); (4L, 61L)]

        // [67,7,x,59,61] - 1261476
        //let offset = 0L
        //let increment = 1L
        //let data = [(0L, 67L); (1L, 7L); (3L, 59L); (4L, 61L)]

        // [1789,37,47,1889] - 1202161486
        //let offset = 0L
        //let increment = 1L
        //let data = [(0L, 1789L); (1L, 37L); (2L, 47L); (3L, 1889L)]

        // 
        let offset = 23L
        let increment = 863L
        let data = [(0L, 23L); (17L, 37L); (23L, 863L); (35L, 19L); (36L, 13L); (40L, 17L); (52L, 29L); (54L, 571L); (95L, 41L)]
        
        Seq.initInfinite (fun i -> (int64 i) * increment) |> Seq.find (ok data offset)
