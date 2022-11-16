namespace AdventOfCode2021.Day01;

using AdventOfCode2021.Day01.Models;

[Solution(2021, 1, 1)]
public class Solution01 : AbstractSolution<int, int>
{
    public Solution01(IInputProvider<int> inputProvider) : base(inputProvider) { }

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
