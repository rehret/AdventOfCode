namespace AdventOfCode2021.Day01;

using AdventOfCode;

using AdventOfCode2021.Day01.Models;

internal class Solution02 : AbstractSolution<int>
{
    private const ushort WindowSize = 3;

    public Solution02(IInputProcessor<int> inputProcessor) : base(inputProcessor) { }

    public override Task<string> ComputeSolutionAsync(IEnumerable<int> input)
    {
        var inputArray = input.ToArray();
        var increaseCount = inputArray
            .Select((_, index) =>
                index <= inputArray.Length - WindowSize
                    ? inputArray.Skip(index).Take(WindowSize).ToArray()
                    : Array.Empty<int>()
            )
            .Where(values => values.Any())
            .Aggregate(
                new IncreaseCountAccumulator(0, int.MaxValue),
                (accumulator, currentValues) => new IncreaseCountAccumulator(
                    IncreaseCount: accumulator.IncreaseCount +
                    (currentValues.Sum() > accumulator.LastValue ? 1 : 0),
                    LastValue: currentValues.Sum()
                )
            ).IncreaseCount;

        return Task.FromResult(increaseCount.ToString());
    }
}