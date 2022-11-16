namespace AdventOfCode2021.Day01;

using AdventOfCode2021.Day01.Models;

[Solution(2021, 1, 2)]
public class Solution02 : AbstractSolution<int, int>
{
    private const ushort WindowSize = 3;

    public Solution02(IInputProvider<int> inputProvider) : base(inputProvider)  { }

    public override Task<int> ComputeSolutionAsync(IEnumerable<int> input)
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

        return Task.FromResult(increaseCount);
    }
}