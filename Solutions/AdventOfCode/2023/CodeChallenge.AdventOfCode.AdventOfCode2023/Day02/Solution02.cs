namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day02;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day02.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2023, 2, 2)]
internal class Solution02(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
    : AdventOfCodeSolution<IEnumerable<Game>, int>(inputProviderBuilder.BuildDay02InputProvider())
{
    protected override int ComputeSolution(IEnumerable<Game> input)
    {
        return input
            .Select(game => game.DiceSets.Aggregate(new DiceSet(0, 0, 0),
                (acc, val) => new DiceSet(
                    uint.Max(acc.NumRed, val.NumRed),
                    uint.Max(acc.NumGreen, val.NumGreen),
                    uint.Max(acc.NumBlue, val.NumBlue)
                )))
            .Select(set => set.NumRed * set.NumGreen * set.NumBlue)
            .Select(power => (int)power)
            .Sum();
    }
}