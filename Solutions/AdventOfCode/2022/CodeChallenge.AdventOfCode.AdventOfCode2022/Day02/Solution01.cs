namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day02;

using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 2, 1)]
internal class Solution01 : AbstractDay02Solution
{
    public Solution01(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
        : base(inputProviderBuilder.BuildDay02InputProvider(StrategyGuideStepInputProviderType.SuggestedMove))
    { }
}
