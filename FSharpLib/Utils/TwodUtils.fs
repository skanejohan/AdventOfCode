namespace Lib.Utils

module TwodUtils =

    let areaAround (x, y) = [(x-1, y-1); (x, y-1); (x+1, y-1); (x-1, y); (x, y); (x+1, y); (x-1, y+1); (x, y+1); (x+1, y+1)]

    // listListToPosList [['a'; 'b']; ['c'; 'd']; ['e'; 'f']] = 
    //     [((0, 0), 'a'); ((1, 0), 'b'); 
    //      ((0, 1), 'c'); ((1, 1), 'd'); 
    //      ((0, 2), 'e'); ((1, 2), 'f')]
    let listListToPosList listList =
        let rec addLine x y line ack = 
            match line with
            | v :: vs -> addLine (x+1) y vs (((x, y), v) :: ack)
            | _       -> ack
        let rec addLines y lines ack = 
            match lines with
            | l :: ls -> addLines (y+1) ls (addLine 0 y l ack)
            | _       -> ack
        addLines 0 listList []

    let posListGet x y posList = 
        match List.tryFind (fun ((x1, y1), _) -> x = x1 && y = y1) posList with 
        | Some ((x, y), v) -> Some v
        | None             -> None

    let posListGetSafe x y def posList = match posListGet x y posList with | Some v -> v | None -> def

    let posListMap mapping list = list |> List.map (fun ((x, y), v) -> ((x, y), mapping v))

    let posListFilter predicate list = list |> List.filter (fun ((_, _), v) -> predicate v)

    let posListXs posList = List.map (fun ((x, _), _) -> x) posList

    let posListYs posList = List.map (fun ((_, y), _) -> y) posList

    let posListToListList posList =
        let minX = posListXs posList |> List.min 
        let minY = posListYs posList |> List.min
        let maxX = posListXs posList |> List.max
        let maxY = posListYs posList |> List.max
        Array2D.init (maxX-minX+1) (maxY-minY+1) (fun x y -> posListGetSafe x y false posList)
