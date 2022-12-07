namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day07;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day07.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 7, 2)]
internal class Solution02 : AdventOfCodeSolution<Directory, int>
{
    private const int FilesystemSize = 70000000;
    private const int UpdateSize = 30000000;

    public Solution02(IInputProvider<AdventOfCodeChallengeSelection, Directory> inputProvider) : base(inputProvider) { }

    protected override int ComputeSolution(Directory root)
    {
        var currentlyFreeSpace = FilesystemSize - root.GetSize();
        var needToFree = UpdateSize - currentlyFreeSpace;

        return GetAllDirectories(root)
            .Select(x => x.GetSize())
            .Where(x => x >= needToFree)
            .Order()
            .First();
    }

    private static IEnumerable<Directory> GetAllDirectories(Directory directory)
    {
        var result = new List<Directory> { directory };

        var subDirectories = directory.Entities.Where(x => x is Directory)
            .Cast<Directory>()
            .SelectMany(GetAllDirectories);

        result.AddRange(subDirectories);

        return result;
    }
}
