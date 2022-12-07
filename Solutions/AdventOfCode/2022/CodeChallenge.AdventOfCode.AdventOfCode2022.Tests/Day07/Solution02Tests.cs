namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day07;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day07;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day07.Models;
using CodeChallenge.Core.IO;

public class Solution02Tests
{
    private readonly Solution02 _solution;

    public Solution02Tests()
    {
        var inputProviderMock = new Mock<IInputProvider<AdventOfCodeChallengeSelection, Directory>>();
        _solution = new Solution02(inputProviderMock.Object);
    }

    [Fact]
    public async Task ComputeSolutionAsync_WithSampleInput_ProducesSampleOutput()
    {
        // Arrange
        var root = new Directory("/");
        var a = new Directory("a");
        root.Entities.Add(a);
        var e = new Directory("e");
        a.Entities.Add(e);
        e.Entities.Add(new File("i", 584));
        a.Entities.Add(new File("f", 29116));
        a.Entities.Add(new File("g", 2557));
        a.Entities.Add(new File("h.lst", 62596));
        root.Entities.Add(new File("b.txt", 14848514));
        root.Entities.Add(new File("c.dat", 8504156));
        var d = new Directory("d");
        root.Entities.Add(d);
        d.Entities.Add(new File("j", 4060174));
        d.Entities.Add(new File("d.log", 8033020));
        d.Entities.Add(new File("d.ext", 5626152));
        d.Entities.Add(new File("k", 7214296));

        // Act
        var result = await _solution.ComputeSolutionAsync(root).ConfigureAwait(false);

        // Assert
        Assert.Equal(24933642, result);
    }
}