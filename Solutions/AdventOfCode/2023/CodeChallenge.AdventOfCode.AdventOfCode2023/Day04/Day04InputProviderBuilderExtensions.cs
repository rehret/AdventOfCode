namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day04;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day04.Models;
using CodeChallenge.Core.IO;

internal static class Day04InputProviderBuilderExtensions
{
    public static IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<ScratchCard>> BuildDay04InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder
    )
    {
        return builder
            .ReadLines(StringSplitOptions)
            .ParseUsing(line =>
            {
                var cardNumberAndValues = line.Split(':', 2, StringSplitOptions);
                var cardNumber = int.Parse(cardNumberAndValues.First().Split(' ', 2, StringSplitOptions).Last());
                var winningNumbersAndScratchedNumbers = cardNumberAndValues.Last().Split('|', 2, StringSplitOptions);

                var winningNumbers = winningNumbersAndScratchedNumbers.First()
                    .Split(' ', StringSplitOptions)
                    .Select(int.Parse);

                var scratchedNumbers = winningNumbersAndScratchedNumbers.Last()
                    .Split(' ', StringSplitOptions)
                    .Select(int.Parse);

                return new ScratchCard(cardNumber, winningNumbers, scratchedNumbers);
            })
            .Build();
    }

    private const StringSplitOptions StringSplitOptions = System.StringSplitOptions.TrimEntries | System.StringSplitOptions.RemoveEmptyEntries;
}