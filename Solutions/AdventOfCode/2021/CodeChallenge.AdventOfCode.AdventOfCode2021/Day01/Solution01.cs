namespace CodeChallenge.AdventOfCode.AdventOfCode2021.Day01;

using AdventOfCode2021.Day01.Models;

using CodeChallenge;

[AdventOfCodeSolution(2021, 1, 1)]
internal class Solution01 : AdventOfCodeSolution<int, int>
{
    public Solution01(IInputProvider<AdventOfCodeChallengeSelection, int> inputProvider) : base(inputProvider) { }

    public override Task<int> ComputeSolutionAsync(IEnumerable<int> input)
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

        return Task.FromResult(increaseCount);
    }
}
