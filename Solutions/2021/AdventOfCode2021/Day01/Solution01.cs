namespace AdventOfCode2021.Day01;

using AdventOfCode2021.Day01.Models;

using Microsoft.Extensions.Logging;

internal class Solution01 : AbstractSolution<int, int>
{
    public Solution01(IInputReader inputReader, IInputProcessor<int> inputProcessor, ILoggerFactory loggerFactory)
        : base(inputReader, inputProcessor, loggerFactory)
    { }

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
