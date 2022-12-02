namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day02;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 2, 1)]
internal class Solution01 : AdventOfCodeSolution<IEnumerable<StrategyGuideStep>, int>
{
    public Solution01(IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<StrategyGuideStep>> inputProvider) : base(inputProvider) { }

    protected override int ComputeSolution(IEnumerable<StrategyGuideStep> input)
    {
        return input.Sum(step => step.GetRoundScore());
    }
}
