namespace Lib.Utils

module CharUtils =

    let charToNum (c : char) = int c - int '0'

    let charIsNum (c : char) = charToNum c >= 0 && charToNum c <= 9
