namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day07;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day07.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 7, 1)]
internal class Solution01 : AdventOfCodeSolution<Directory, int>
{
    private const int ThresholdSize = 100000;

    public Solution01(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
        : base(inputProviderBuilder.BuildDay07InputProvider())
    { }

    protected override int ComputeSolution(Directory root)
    {
        var directoriesUnderThresholdSize = GetDirectoriesUnderThresholdSize(root);
        return directoriesUnderThresholdSize.Select(x => x.GetSize()).Sum();
    }

    private static IEnumerable<Directory> GetDirectoriesUnderThresholdSize(Directory directory)
    {
        var result = new List<Directory>();
        if (directory.GetSize() <= ThresholdSize)
        {
            result.Add(directory);
        }

        result.AddRange(directory.Entities.OfType<Directory>().SelectMany(GetDirectoriesUnderThresholdSize));

        return result;
    }
}
