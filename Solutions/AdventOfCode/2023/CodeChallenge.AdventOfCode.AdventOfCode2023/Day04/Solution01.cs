namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day04;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day04.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2023, 4, 1)]
internal class Solution01(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
    : AdventOfCodeSolution<IEnumerable<ScratchCard>, int>(inputProviderBuilder.BuildDay04InputProvider())
{
    protected override int ComputeSolution(IEnumerable<ScratchCard> scratchCards)
    {
        return scratchCards.Select(ComputeCardScore).Sum();
    }

    private static int ComputeCardScore(ScratchCard card)
    {
        var numberOfMatchingValues = card.ScratchedNumbers.Count(num => card.WinningNumbers.Contains(num));
        if (numberOfMatchingValues == 0)
            return 0;
        return (int)Math.Pow(2, numberOfMatchingValues - 1);
    }
}
