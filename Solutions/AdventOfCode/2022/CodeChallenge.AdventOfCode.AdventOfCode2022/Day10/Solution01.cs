namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day10;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day10.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 10, 1)]
internal class Solution01 : AdventOfCodeSolution<IEnumerable<Instruction>, int>
{
    public Solution01(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
        : base(inputProviderBuilder.BuildDay10InputProvider())
    { }

    protected override int ComputeSolution(IEnumerable<Instruction> input)
    {
        var x = 1;
        var clock = 1;

        var sum = 0;

        foreach (var instruction in input)
        {
            for (var i = 1; i <= instruction.Cycles; i++)
            {
                if (instruction is AddXInstruction addXInstruction && i == addXInstruction.Cycles)
                {
                    x += addXInstruction.AddAmount;
                }

                clock++;

                if ((clock + 20) % 40 == 0)
                {
                    sum += clock * x;
                }
            }
        }

        return sum;
    }
}
