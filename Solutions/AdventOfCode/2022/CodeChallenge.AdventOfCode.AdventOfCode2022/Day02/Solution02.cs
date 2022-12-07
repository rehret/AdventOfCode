namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day02;

using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 2, 2)]
internal class Solution02 : AbstractDay02Solution
{
    public Solution02(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
        : base(inputProviderBuilder.BuildDay02InputProvider(StrategyGuideStepInputProviderType.TargetResult))
    { }
}
