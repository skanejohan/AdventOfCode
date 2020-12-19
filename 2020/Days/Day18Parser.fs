namespace AdventOfCode2020

open Microsoft.FSharp.Core
open FParsec

module Day18Parser =

    type Expression = 
        | Add of Expression * Expression
        | Mul of Expression * Expression
        | Num of int64
        
    type EParser = Parser<Expression,unit>
    let pNum : EParser = spaces >>. pint64 .>> spaces |>> Num
        
    let pExpr1, pExprRef1 : EParser * EParser ref  = createParserForwardedToRef<Expression, unit>()
    let parens1 = pstring "(" >>. pExpr1 .>> pstring ")"
    let opp1 = new OperatorPrecedenceParser<Expression,unit,unit>()
    opp1.AddOperator(InfixOperator("+", spaces, 1, Associativity.Left, fun x y -> Add(x, y)))
    opp1.AddOperator(InfixOperator("*", spaces, 1, Associativity.Left, fun x y -> Mul(x, y)))
    opp1.TermParser <- pNum <|> parens1
    do pExprRef1 := spaces >>. opp1.ExpressionParser

    let pExpr2, pExprRef2 : EParser * EParser ref  = createParserForwardedToRef<Expression, unit>()
    let parens2 = pstring "(" >>. pExpr2 .>> pstring ")"
    let opp2 = new OperatorPrecedenceParser<Expression,unit,unit>()
    opp2.AddOperator(InfixOperator("+", spaces, 2, Associativity.Left, fun x y -> Add(x, y)))
    opp2.AddOperator(InfixOperator("*", spaces, 1, Associativity.Left, fun x y -> Mul(x, y)))
    opp2.TermParser <- pNum <|> parens2
    do pExprRef2 := spaces >>. opp2.ExpressionParser
