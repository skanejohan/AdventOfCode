using CSharpLib;
using System.Collections.Generic;
using System.Linq;

namespace Y2023.Day02;

public static class Solver
{
    public static long Part1()
    {
        return LoadData().Where(GameOk).Sum(g => g.Id);
    }

    public static long Part2()
    {
        return LoadData().Select(Required).Select(rgb => rgb.Red * rgb.Green * rgb.Blue).Sum();
    }

    private static IEnumerable<Game> LoadData(string fileName = "Data.txt")
    {
        foreach (var line in new DataLoader(2023, 2).ReadStrings(fileName))
        {
            var parts = line.Split(':').Select(l => l.Trim()).ToList();
            var gameId = int.Parse(parts[0].Replace("Game ", ""));

            var reveals = new List<Reveal>();
            var draws = parts[1].Split(';').Select(l => l.Trim()).ToList();
            foreach(var draw in draws)
            {
                var allReveals = draw.Split(',').Select(l => l.Trim()).ToList();
                var redReveal = allReveals.FirstOrDefault(s => s.Contains("red"));
                var red = redReveal == null ? 0 : int.Parse(redReveal.Replace(" red", ""));
                var greenReveal = allReveals.FirstOrDefault(s => s.Contains("green"));
                var green = greenReveal == null ? 0 : int.Parse(greenReveal.Replace(" green", ""));
                var blueReveal = allReveals.FirstOrDefault(s => s.Contains("blue"));
                var blue = blueReveal == null ? 0 : int.Parse(blueReveal.Replace(" blue", ""));
                reveals.Add(new Reveal(red, green, blue));
            }

            yield return new Game(gameId, reveals.ToArray());
        }
    }

    private static bool GameOk(Game game)
    {
        return game.Reveals.All(r => r.Red <= 12 && r.Green <= 13 && r.Blue <= 14);
    }

    private static (int Red, int Green, int Blue) Required(Game game)
    {
        var red = game.Reveals.Select(r => r.Red).Max();
        var green = game.Reveals.Select(r => r.Green).Max();
        var blue = game.Reveals.Select(r => r.Blue).Max();
        return (red, green, blue);
    }

    private record Reveal(int Red, int Green, int Blue);
    private record Game(int Id, Reveal[] Reveals);
}
