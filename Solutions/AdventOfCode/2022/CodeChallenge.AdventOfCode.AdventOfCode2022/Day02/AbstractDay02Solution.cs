namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day02;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.Models;
using CodeChallenge.Core.IO;

internal abstract class AbstractDay02Solution : AdventOfCodeSolution<IEnumerable<StrategyGuideStep>, int>
{
    protected AbstractDay02Solution(IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<StrategyGuideStep>> inputProvider)
        : base(inputProvider)
    { }

    protected override int ComputeSolution(IEnumerable<StrategyGuideStep> input)
    {
        return input.Sum(step => step.PlayersMove.Score + step.PlayersMove.GetResultAgainstMove(step.OpponentsMove).Score);
    }
}