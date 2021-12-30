namespace Lib.Utils

module NumUtils =

    // wrap 1 3 = 1
    // wrap 3 3 = 3
    // wrap 4 3 = 1
    let wrap i n = (i-1) % n + 1


