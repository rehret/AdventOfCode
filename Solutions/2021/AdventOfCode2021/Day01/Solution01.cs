namespace AdventOfCode2021.Day01;

using AdventOfCode;

using AdventOfCode2021.Day01.Models;

internal class Solution01 : AbstractSolution<int>
{
    public Solution01(IInputProcessor<int> inputProcessor) : base(inputProcessor) { }

    public override Task<string> ComputeSolutionAsync(IEnumerable<int> input)
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

        return Task.FromResult(increaseCount.ToString());
    }
}
