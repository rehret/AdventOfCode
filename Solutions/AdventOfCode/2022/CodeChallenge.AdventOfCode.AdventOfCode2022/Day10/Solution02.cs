namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day10;

using System.Text;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day10.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 10, 2)]
internal class Solution02 : AdventOfCodeSolution<IEnumerable<Instruction>, string>
{
    public Solution02(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
        : base(inputProviderBuilder.BuildDay10InputProvider())
    { }

    protected override string ComputeSolution(IEnumerable<Instruction> input)
    {
        var x = 1;
        var clock = 0;

        var output = new string[6];
        var currentLine = 0;
        var outputLine = new StringBuilder();

        foreach (var instruction in input)
        {
            for (var i = 1; i <= instruction.Cycles; i++)
            {
                var currentPixelXPosition = clock % 40;
                if (x >= currentPixelXPosition - 1 && x <= currentPixelXPosition + 1)
                {
                    outputLine.Append('#');
                }
                else
                {
                    outputLine.Append('.');
                }

                if (instruction is AddXInstruction addXInstruction && i == addXInstruction.Cycles)
                {
                    x += addXInstruction.AddAmount;
                }

                clock++;

                if (clock != 0 && clock % 40 == 0)
                {
                    output[currentLine] = outputLine.ToString();
                    currentLine++;
                    outputLine = new StringBuilder();
                }
            }
        }

        return string.Join("\n", output);
    }
}
