namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day01;

using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2023, 1, 1)]
internal class Solution01(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
    : AbstractDay01Solution(inputProviderBuilder.BuildDay01Puzzle01InputProvider());