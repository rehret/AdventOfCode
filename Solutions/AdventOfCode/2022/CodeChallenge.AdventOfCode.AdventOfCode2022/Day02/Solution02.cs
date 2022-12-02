namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day02;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.InputProviders;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.Models;
using CodeChallenge.AdventOfCode.Attributes;

[AdventOfCodeSolution(2022, 2, 2)]
internal class Solution02 : AbstractDay02Solution
{
    public Solution02(StrategyGuideStepInputProviderFactory inputProviderFactory)
        : base(inputProviderFactory(StrategyGuideStepInputProviderType.TargetResult))
    { }
}
