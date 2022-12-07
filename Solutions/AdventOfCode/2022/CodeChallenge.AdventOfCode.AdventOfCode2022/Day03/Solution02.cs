namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day03;

using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 3, 2)]
internal class Solution02 : AdventOfCodeSolution<IEnumerable<string>, int>
{
    public Solution02(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder) : base(inputProviderBuilder.BuildDay03InputProvider()) { }

    protected override int ComputeSolution(IEnumerable<string> input)
    {
        return input
            .Chunk(3)
            .Select(group => group[0].Intersect(group[1]).Intersect(group[2]).Single())
            .Select(ItemScoreHelpers.GetItemScore)
            .Sum();
    }
}
