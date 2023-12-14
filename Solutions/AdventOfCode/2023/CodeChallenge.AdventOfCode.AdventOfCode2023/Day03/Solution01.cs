namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day03;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day03.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2023, 3, 1)]
internal class Solution01(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
    : AdventOfCodeSolution<Schematic, int>(inputProviderBuilder.BuildDay03InputProvider())
{
    protected override int ComputeSolution(Schematic input) => input.Parts.Select(part => part.PartNumber).Sum();
}
