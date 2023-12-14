namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day02;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day02.Models;
using CodeChallenge.Core.IO;

internal static class Day02InputProviderBuilderExtensions
{
    public static IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<Game>> BuildDay02InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder
    ) => builder
        .ReadLines(StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
        .ParseUsing(line =>
        {
            var gameIdAndSets = line.Split(':', 2);
            var gameId = uint.Parse(gameIdAndSets.First()[5..]);

            var rawSets = gameIdAndSets
                .Last()
                .Split(";", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(rawSet => rawSet.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries));

            var sets = rawSets.Select(rawSet =>
            {
                var redCount = 0u;
                var greenCount = 0u;
                var blueCount = 0u;

                foreach (var diceResult in rawSet)
                {
                    var countAndColor = diceResult.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                    var count = uint.Parse(countAndColor.First());
                    switch (countAndColor.Last().ToLower())
                    {
                        case "red":
                            redCount = count;
                            break;
                        case "green":
                            greenCount = count;
                            break;
                        case "blue":
                            blueCount = count;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                return new DiceSet(redCount, greenCount, blueCount);
            });

            return new Game(gameId, sets.ToList());
        })
        .Build();
}