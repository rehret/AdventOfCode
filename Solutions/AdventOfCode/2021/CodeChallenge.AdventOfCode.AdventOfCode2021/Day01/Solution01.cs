namespace CodeChallenge.AdventOfCode.AdventOfCode2021.Day01;

using AdventOfCode2021.Day01.Models;

using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2021, 1, 1)]
internal class Solution01 : AdventOfCodeSolution<IEnumerable<int>, int>
{
    public Solution01(IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<int>> inputProvider) : base(inputProvider) { }

    protected override int ComputeSolution(IEnumerable<int> input)
    {
        var increaseCount = input
            .Aggregate(
                new IncreaseCountAccumulator(0, int.MaxValue),
                (accumulator, currentValue) => new IncreaseCountAccumulator(
                    IncreaseCount: accumulator.IncreaseCount +
                        (currentValue > accumulator.LastValue ? 1 : 0),
                    LastValue: currentValue
                )
            )
            .IncreaseCount;

        return increaseCount;
    }
}
