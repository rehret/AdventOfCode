namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day06;

using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 6, 1)]
internal class Solution01 : AbstractDay06Solution
{
    private const int BufferSize = 4;

    public Solution01(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
        : base(inputProviderBuilder, BufferSize)
    { }
}
