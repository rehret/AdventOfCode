namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day04;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day04.Extensions;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 4, 2)]
internal class Solution02 : AdventOfCodeSolution<IEnumerable<(Range, Range)>, int>
{
    public Solution02(IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<(Range, Range)>> inputProvider) : base(inputProvider) { }

    protected override int ComputeSolution(IEnumerable<(Range, Range)> input)
    {
        return input
            .Count(ranges => ranges.Item1.Overlaps(ranges.Item2));
    }
}
