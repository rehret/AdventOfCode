namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day01;

using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2023, 1, 2)]
internal class Solution02(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
    : AbstractDay01Solution(inputProviderBuilder.BuildDay01Puzzle02InputProvider());