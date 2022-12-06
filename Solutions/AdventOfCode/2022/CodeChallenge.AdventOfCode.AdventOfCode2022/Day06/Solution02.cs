namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day06;

using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 6, 2)]
internal class Solution02 : AbstractDay06Solution
{
    private const int BufferSize = 14;

    public Solution02(IInputProvider<AdventOfCodeChallengeSelection, string> inputProvider)
        : base(inputProvider, BufferSize)
    { }
}
