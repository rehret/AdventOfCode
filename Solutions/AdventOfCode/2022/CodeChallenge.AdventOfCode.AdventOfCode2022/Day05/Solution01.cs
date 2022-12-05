namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day05;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day05.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 5, 1)]
internal class Solution01 : AdventOfCodeSolution<StacksAndInstructions, string>
{
    public Solution01(IInputProvider<AdventOfCodeChallengeSelection, StacksAndInstructions> inputProvider) : base(inputProvider) { }

    protected override string ComputeSolution(StacksAndInstructions input)
    {
        var (stacks, instructions) = input;

        foreach (var instruction in instructions)
        {
            for (var i = 0; i < instruction.Count; i++)
            {
                stacks[instruction.Destination].Push(stacks[instruction.Source].Pop());
            }
        }

        return string.Join("", stacks.Select(x => x.Pop()));
    }
}
