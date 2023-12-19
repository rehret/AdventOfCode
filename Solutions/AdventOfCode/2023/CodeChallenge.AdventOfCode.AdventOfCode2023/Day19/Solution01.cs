namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day19;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day19.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2023, 19, 1)]
internal class Solution01(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
    : AdventOfCodeSolution<(WorkflowEngine WorkflowEngine, IEnumerable<Part> Parts), int>(inputProviderBuilder.BuildDay19InputProvider())
{
    protected override int ComputeSolution((WorkflowEngine WorkflowEngine, IEnumerable<Part> Parts) input)
    {
        var (workflowEngine, parts) = input;
        return parts
            .Where(part => workflowEngine.ProcessPart(part) is AcceptedResult)
            .Select(part => part.X + part.M + part.A + part.S)
            .Sum();
    }
}
