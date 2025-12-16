using System.Collections.Generic;
using System.Linq;

namespace Y2025.Day10;

internal class Indicators(IEnumerable<bool> lights)
{
    public int AsInt()
    {
        var n = lights.Last() ? 1 : 0;
        foreach(var light in lights.Reverse().Skip(1))
        {
            n <<= 1;
            if (light)
            {
                n += 1;
            }
        }
        return n;
    }
}
