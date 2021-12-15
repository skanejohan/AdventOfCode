namespace Lib.Utils

module MapUtils =

    let incCount key map = match Map.tryFind key map with           
                           | Some i -> Map.add key (i+1) map
                           | None   -> Map.add key 1 map

    let incCountL key map = match Map.tryFind key map with           
                            | Some i -> Map.add key (i+1L) map
                            | None   -> Map.add key 1L map

    let addCount key n map = match Map.tryFind key map with           
                             | Some i -> Map.add key (i+n) map
                             | None   -> Map.add key n map

    let addCountL key (n : int64) map = match Map.tryFind key map with           
                                        | Some i -> Map.add key (i+n) map
                                        | None   -> Map.add key n map

    let getCount key map = match Map.tryFind key map with           
                           | Some i -> i
                           | None   -> 0

    let addList key value map = match Map.tryFind key map with           
                                | Some list -> Map.add key (value :: list) map
                                | None      -> Map.add key [value] map

    let getList key value map = match Map.tryFind key map with           
                                | Some list -> list
                                | None      -> []
