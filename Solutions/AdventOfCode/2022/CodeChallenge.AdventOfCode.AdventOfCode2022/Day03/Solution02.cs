namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day03;

using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

using Microsoft.VisualBasic;

[AdventOfCodeSolution(2022, 3, 2)]
internal class Solution02 : AdventOfCodeSolution<IEnumerable<string>, int>
{
    public Solution02(IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<string>> inputProvider) : base(inputProvider) { }

    protected override int ComputeSolution(IEnumerable<string> input)
    {
        return input
            .Chunk(3)
            .Select(group => group[0].Intersect(group[1]).Intersect(group[2]).Single())
            .Select(ItemScoreHelpers.GetItemScore)
            .Sum();
    }
}
