namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day01;

using AdventOfCode;

using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 1, 1)]
internal class Solution01 : AdventOfCodeSolution<IEnumerable<IEnumerable<int>>, int>
{
    public Solution01(IGroupedInputProvider<AdventOfCodeChallengeSelection, int> inputProvider) : base(inputProvider) { }

    public override Task<int> ComputeSolutionAsync(IEnumerable<IEnumerable<int>> input)
    {
        return Task.FromResult(input.Max(x => x.Sum()));
    }
}