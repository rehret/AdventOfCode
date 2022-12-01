namespace CodeChallenge.AdventOfCode.AdventOfCode2021.Day01;

using CodeChallenge.AdventOfCode.AdventOfCode2021.Day01.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2021, 1, 2)]
internal class Solution02 : AdventOfCodeSolution<IEnumerable<int>, int>
{
    private const ushort WindowSize = 3;

    public Solution02(IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<int>> inputProvider) : base(inputProvider)  { }

    protected override int ComputeSolution(IEnumerable<int> input)
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

        return increaseCount;
    }
}