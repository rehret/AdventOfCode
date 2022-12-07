namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day07;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day07.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 7, 1)]
internal class Solution01 : AdventOfCodeSolution<Directory, int>
{
    private const int ThresholdSize = 100000;

    public Solution01(IInputProvider<AdventOfCodeChallengeSelection, Directory> inputProvider) : base(inputProvider) { }

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

        result.AddRange(directory.Entities.Where(x => x is Directory).Cast<Directory>().SelectMany(GetDirectoriesUnderThresholdSize));

        return result;
    }
}
