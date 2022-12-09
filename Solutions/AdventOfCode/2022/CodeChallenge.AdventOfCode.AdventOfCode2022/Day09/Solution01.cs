namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day09;

using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 9, 1)]
internal class Solution01 : AbstractDay09Solution
{
    private const int DistanceThreshold = 1;

    public Solution01(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
        : base(inputProviderBuilder, 2)
    { }
}
