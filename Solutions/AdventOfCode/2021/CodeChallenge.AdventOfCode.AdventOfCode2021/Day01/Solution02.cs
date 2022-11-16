namespace CodeChallenge.AdventOfCode.AdventOfCode2021.Day01;

using CodeChallenge;
using CodeChallenge.AdventOfCode.AdventOfCode2021.Day01.Models;

[AdventOfCodeSolution(2021, 1, 2)]
internal class Solution02 : AdventOfCodeSolution<int, int>
{
    private const ushort WindowSize = 3;

    public Solution02(IInputProvider<AdventOfCodeChallengeSelection, int> inputProvider) : base(inputProvider)  { }

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