namespace Lib

module CountMap =

    type CountMap<'a when 'a : comparison> = CountMap of Map<'a, int> with

        static member get (cm : CountMap<'a>) = match cm with CountMap m -> m

        static member add (cm : CountMap<'a>) a =
            match cm with 
            | CountMap m -> match m.TryFind a with
                            | Some n -> CountMap (m.Add (a, (n+1)))
                            | None   -> CountMap (m.Add (a, 1))

        static member createFrom elements = List.fold CountMap.add (CountMap Map.empty) elements

        