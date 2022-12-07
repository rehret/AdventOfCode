namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day05;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day05.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 5, 2)]
internal class Solution02 : AdventOfCodeSolution<StacksAndInstructions, string>
{
    public Solution02(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
        : base(inputProviderBuilder.BuildDay05InputProvider())
    { }

    protected override string ComputeSolution(StacksAndInstructions input)
    {
        var (stacks, instructions) = input;

        foreach (var instruction in instructions)
        {
            var poppedItems = new char[instruction.Count];
            for (var i = 0; i < instruction.Count; i++)
            {
                poppedItems[instruction.Count - 1 - i] = stacks[instruction.Source].Pop();
            }

            foreach (var item in poppedItems)
            {
                stacks[instruction.Destination].Push(item);
            }
        }

        return string.Join("", stacks.Select(x => x.Pop()));
    }
}
