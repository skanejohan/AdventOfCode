namespace Lib.Utils

module CharUtils =

    let charToNum (c : char) = int c - int '0'

    let charIsNum (c : char) = charToNum c >= 0 && charToNum c <= 9

    let charToBigint (c : char) = charToNum c |> bigint

    let numToChar n = char (int '0' + n)