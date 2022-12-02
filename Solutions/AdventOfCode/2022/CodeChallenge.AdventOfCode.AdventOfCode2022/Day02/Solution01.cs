namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day02;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.InputProviders;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.Models;
using CodeChallenge.AdventOfCode.Attributes;

[AdventOfCodeSolution(2022, 2, 1)]
internal class Solution01 : AbstractDay02Solution
{
    public Solution01(StrategyGuideStepInputProviderFactory inputProviderFactory)
        : base(inputProviderFactory(StrategyGuideStepInputProviderType.SuggestedMove))
    { }
}
