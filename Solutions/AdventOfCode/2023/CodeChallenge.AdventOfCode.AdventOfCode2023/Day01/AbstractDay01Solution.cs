namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day01;

using CodeChallenge.Core.IO;

internal abstract class AbstractDay01Solution(
    IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<IEnumerable<int>>> inputProvider
)
    : AdventOfCodeSolution<IEnumerable<IEnumerable<int>>, int>(inputProvider)
{
    protected override int ComputeSolution(IEnumerable<IEnumerable<int>> input)
    {
        return input
            .Select(line => line.ToArray())
            .Select(numbersOnLine => (numbersOnLine.First() * 10) + numbersOnLine.Last())
            .Sum();
    }
}