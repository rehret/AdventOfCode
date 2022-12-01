namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day01;

using AdventOfCode;

using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 1, 2)]
internal class Solution02 : AdventOfCodeSolution<IEnumerable<IEnumerable<int>>, int>
{
    public Solution02(IGroupedInputProvider<AdventOfCodeChallengeSelection, int> inputProvider) : base(inputProvider) { }

    public override Task<int> ComputeSolutionAsync(IEnumerable<IEnumerable<int>> input)
    {
        return Task.FromResult(input.Select(x => x.Sum())
            .OrderDescending()
            .Take(3)
            .Sum());
    }
}