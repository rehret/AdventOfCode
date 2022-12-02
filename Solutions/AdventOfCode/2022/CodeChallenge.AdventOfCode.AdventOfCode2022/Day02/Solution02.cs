namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day02;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.InputProviders;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.Models;
using CodeChallenge.AdventOfCode.Attributes;

[AdventOfCodeSolution(2022, 2, 2)]
internal class Solution02 : AdventOfCodeSolution<IEnumerable<StrategyGuideStep>, int>
{
    public Solution02(IStrategyGuideStepInputProvider inputProvider) : base(inputProvider)
    {
        inputProvider.UseTargetResultInput = true;
    }

    protected override int ComputeSolution(IEnumerable<StrategyGuideStep> input)
    {
        return input.Sum(step => step.GetRoundScore());
    }
}
