namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day07.InputProviders;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day07.InputProviders;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day07.Models;
using CodeChallenge.Core;
using CodeChallenge.Core.IO;

public class FileSystemInputProviderTests
{
    private readonly Mock<IInputReader<AdventOfCodeChallengeSelection>> _inputReaderMock;
    private readonly FileSystemInputProvider _inputProvider;

    public FileSystemInputProviderTests()
    {
        _inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        _inputProvider = new FileSystemInputProvider(_inputReaderMock.Object);
    }

    [Fact]
    public async Task GetInputAsync_GivenSampleInput_ParsesIntoFileSystem()
    {
        // Arrange
        var input = new[]
        {
            "$ cd /",
            "$ ls",
            "dir a",
            "14848514 b.txt",
            "8504156 c.dat",
            "dir d",
            "$ cd a",
            "$ ls",
            "dir e",
            "29116 f",
            "2557 g",
            "62596 h.lst",
            "$ cd e",
            "$ ls",
            "584 i",
            "$ cd ..",
            "$ cd ..",
            "$ cd d",
            "$ ls",
            "4060174 j",
            "8033020 d.log",
            "5626152 d.ext",
            "7214296 k"
        };

        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<AdventOfCodeChallengeSelection>()))
            .ReturnsAsync(() => input);

        // Act
        var result = await _inputProvider.GetInputAsync(new AdventOfCodeChallengeSelection(0, 0, 0)).ConfigureAwait(false);

        // Assert
        Assert.Equal("/", result.Name);
        var a = result.Entities.Single(x => x is Directory && x.Name == "a") as Directory;
        Assert.NotNull(a);
        var e = a.Entities.Single(x => x is Directory && x.Name == "e") as Directory;
        Assert.NotNull(e);
        Assert.Collection(
            e.Entities,
            x =>
            {
                var file = x as File;
                Assert.NotNull(file);
                Assert.Equal("i", file.Name);
                Assert.Equal(584, file.Size);
            }
        );
        var f = a.Entities.Single(x => x is File && x.Name == "f") as File;
        Assert.NotNull(f);
        Assert.Equal(29116, f.Size);
        var g = a.Entities.Single(x => x is File && x.Name == "g") as File;
        Assert.NotNull(g);
        Assert.Equal(2557, g.Size);
        var hLst = a.Entities.Single(x => x is File && x.Name == "h.lst") as File;
        Assert.NotNull(hLst);
        Assert.Equal(62596, hLst.Size);
        var bTxt = result.Entities.Single(x => x is File && x.Name == "b.txt") as File;
        Assert.NotNull(bTxt);
        Assert.Equal(14848514, bTxt.Size);
        var cDat = result.Entities.Single(x => x is File && x.Name == "c.dat") as File;
        Assert.NotNull(cDat);
        Assert.Equal(8504156, cDat.Size);
        var d = result.Entities.Single(x => x is Directory && x.Name == "d") as Directory;
        Assert.NotNull(d);
        var j = d.Entities.Single(x => x is File && x.Name == "j") as File;
        Assert.NotNull(j);
        Assert.Equal(4060174, j.Size);
        var dLog = d.Entities.Single(x => x is File && x.Name == "d.log") as File;
        Assert.NotNull(dLog);
        Assert.Equal(8033020, dLog.Size);
        var dExt = d.Entities.Single(x => x is File && x.Name == "d.ext") as File;
        Assert.NotNull(dExt);
        Assert.Equal(5626152, dExt.Size);
        var k = d.Entities.Single(x => x is File && x.Name == "k") as File;
        Assert.NotNull(k);
        Assert.Equal(7214296, k.Size);
    }
}