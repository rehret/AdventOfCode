namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day03;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day03.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2023, 3, 2)]
internal class Solution02(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
    : AdventOfCodeSolution<Schematic, int>(inputProviderBuilder.BuildDay03InputProvider())
{
    protected override int ComputeSolution(Schematic input)
    {
        return input.Parts
            .Where(part => part.Symbol == '*')
            .GroupBy(part => part.SymbolCoordinate)
            .Where(grouping => grouping.Count() == 2)
            .Select(grouping => grouping.First().PartNumber * grouping.Last().PartNumber)
            .Sum();
    }
}
