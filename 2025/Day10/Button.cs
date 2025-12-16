using System;
using System.Collections.Generic;

namespace Y2025.Day10;

internal class Button(List<int> affectedIndicators)
{
    public int AsInt()
    {
        var n = 0;
        foreach (var indicator in affectedIndicators)
        {
            n += (int)Math.Pow(2, indicator);
        }
        return n;
    }
}
