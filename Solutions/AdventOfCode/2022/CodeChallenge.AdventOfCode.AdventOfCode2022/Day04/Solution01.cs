namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day04;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day04.Extensions;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 4, 1)]
internal class Solution01 : AbstractDay04Solution
{
    public Solution01(IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<string>> inputProvider) : base(inputProvider) { }

    protected override int ComputeSolution(IEnumerable<string> input)
    {
        return BuildRangePairs(input)
            .Count(ranges => ranges.Item1.Contains(ranges.Item2) || ranges.Item2.Contains(ranges.Item1));
    }
}
