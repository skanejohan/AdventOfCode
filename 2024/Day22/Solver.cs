using CSharpLib;
using CSharpLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace Y2024.Day22;

public static class Solver
{
    public static long Part1()
    {
        var secrets = new DataLoader(2024, 22).ReadLongs("data.txt");
        return secrets.Select(s => NextN(s, 2000)).Sum();
    }

    public static long Part2()
    {
        var visited = new HashSet<Window>();
        var totalPrice = new Dictionary<Window, long>();

        foreach (var secret in new DataLoader(2024, 22).ReadLongs("data.txt"))
        {
            visited.Clear();
            foreach (var win in GetPricesAndChanges(secret).SlidingWindow(4))
            {
                var price = win.Select(w => w.Price).Last();
                var changes = win.Select(w => w.Change).ToList();
                var window = new Window(changes[0], changes[1], changes[2], changes[3]);
                if (!visited.Contains(window))
                {
                    if (!totalPrice.TryGetValue(window, out var priceSoFar)) { priceSoFar = 0; }
                    totalPrice[window] = priceSoFar + price;
                    visited.Add(window);
                }
            }
        }

        return totalPrice.Values.Max();
    }

    private static List<(long Price, long Change)> GetPricesAndChanges(long secret)
    {
        var lastDigit = secret % 10;
        List<(long, long)> result = new();
        for (int i = 0; i < 2000; i++)
        {
            secret = Next(secret);
            var change = secret % 10 - lastDigit;
            lastDigit = secret % 10;
            result.Add((lastDigit, change));
        }
        return result;
    }

    private static long NextN(long secret, int iterations)
    {
        for (int i = 0; i < iterations; i++)
        {
            secret = Next(secret);
        }
        return secret;
    }

    private static long Next(long secret)
    {
        var result1 = ((secret * 64) ^ secret) % 16777216;
        var result2 = ((result1 / 32) ^ result1) % 16777216;
        var result3 = ((result2 * 2048) ^ result2) % 16777216;
        return result3;
    }

    private record Window(long Change1, long Change2, long Change3, long Change4);
}
