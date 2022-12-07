namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day07.InputProviders;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day07.InputProviders;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day07.Models;
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
        var a = result.Entities.OfType<Directory>().Single(x => x.Name == "a");
        var e = a.Entities.OfType<Directory>().Single(x => x.Name == "e");
        Assert.Empty(e.Entities.OfType<Directory>());
        var i = e.Entities.OfType<File>().Single();
        Assert.Equal("i", i.Name);
        Assert.Equal(584, i.Size);
        var f = a.Entities.OfType<File>().Single(x =>x.Name == "f");
        Assert.Equal(29116, f.Size);
        var g = a.Entities.OfType<File>().Single(x => x.Name == "g");
        Assert.Equal(2557, g.Size);
        var hLst = a.Entities.OfType<File>().Single(x => x.Name == "h.lst");
        Assert.Equal(62596, hLst.Size);
        var bTxt = result.Entities.OfType<File>().Single(x => x.Name == "b.txt");
        Assert.Equal(14848514, bTxt.Size);
        var cDat = result.Entities.OfType<File>().Single(x => x.Name == "c.dat");
        Assert.Equal(8504156, cDat.Size);
        var d = result.Entities.OfType<Directory>().Single(x => x.Name == "d");
        var j = d.Entities.OfType<File>().Single(x => x.Name == "j");
        Assert.Equal(4060174, j.Size);
        var dLog = d.Entities.OfType<File>().Single(x => x.Name == "d.log");
        Assert.Equal(8033020, dLog.Size);
        var dExt = d.Entities.OfType<File>().Single(x => x.Name == "d.ext");
        Assert.Equal(5626152, dExt.Size);
        var k = d.Entities.OfType<File>().Single(x => x.Name == "k");
        Assert.Equal(7214296, k.Size);
    }
}