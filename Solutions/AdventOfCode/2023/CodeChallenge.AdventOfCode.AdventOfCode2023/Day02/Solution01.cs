namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day02;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day02.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2023, 2, 1)]
internal class Solution01(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
    : AdventOfCodeSolution<IEnumerable<Game>, int>(inputProviderBuilder.BuildDay02InputProvider())
{
    protected override int ComputeSolution(IEnumerable<Game> input)
    {
        return input
            .Where(game => game.DiceSets.All(diceSet =>
                diceSet.NumRed <= InclusiveUpperLimit.NumRed &&
                diceSet.NumGreen <= InclusiveUpperLimit.NumGreen &&
                diceSet.NumBlue <= InclusiveUpperLimit.NumBlue)
            )
            .Select(game => game.GameId)
            .Select(gameId => (int) gameId)
            .Sum();
    }

    private static readonly DiceSet InclusiveUpperLimit = new(12, 13, 14);
}