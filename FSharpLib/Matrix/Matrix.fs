namespace Lib.Matrix

module Matrix =

    type Matrix<'a> = Matrix of 'a list list with

        static member addRow (row : 'a list) (Matrix m) = Matrix (row :: m)

        static member rows (m : Matrix<'a>) = match m with Matrix rows -> rows
        
        static member cols (m : Matrix<'a>) = match m with Matrix rows -> List.transpose rows

        static member map f (m : Matrix<'a>) = match m with Matrix rows -> List.map (fun row -> List.map f row) rows |> Matrix

