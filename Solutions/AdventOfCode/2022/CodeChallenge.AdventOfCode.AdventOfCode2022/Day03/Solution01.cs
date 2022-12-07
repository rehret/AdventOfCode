namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day03;

using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 3, 1)]
internal class Solution01 : AdventOfCodeSolution<IEnumerable<string>, int>
{
    public Solution01(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder) : base(inputProviderBuilder.BuildDay03InputProvider()) { }

    protected override int ComputeSolution(IEnumerable<string> input)
    {
        return input
            .Select(line => (line[..(line.Length / 2)], line[(line.Length / 2)..]))
            .Select(compartments => compartments.Item1.Intersect(compartments.Item2).Single())
            .Select(ItemScoreHelpers.GetItemScore)
            .Sum();
    }
}
