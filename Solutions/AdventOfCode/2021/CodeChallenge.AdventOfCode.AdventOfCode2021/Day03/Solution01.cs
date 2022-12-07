namespace CodeChallenge.AdventOfCode.AdventOfCode2021.Day03;

using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2021, 3, 1)]
internal class Solution01 : AdventOfCodeSolution<IEnumerable<string>, string>
{
    public Solution01(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder) : base(inputProviderBuilder.ReadLines().ParseUsing((string x) => x).Build()) { }

    protected override string ComputeSolution(IEnumerable<string> input)
    {
        throw new NotImplementedException();
    }
}