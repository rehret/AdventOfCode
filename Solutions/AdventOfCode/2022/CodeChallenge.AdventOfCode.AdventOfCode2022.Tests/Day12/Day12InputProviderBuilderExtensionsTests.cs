namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day12;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day12;
using CodeChallenge.Core.IO;

public class Day12InputProviderBuilderExtensionsTests
{
    private readonly Mock<IInputReader<AdventOfCodeChallengeSelection>> _inputReaderMock;
    private readonly IInputProviderBuilder<AdventOfCodeChallengeSelection> _inputProviderBuilder;

    public Day12InputProviderBuilderExtensionsTests()
    {
        _inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        _inputProviderBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(_inputReaderMock.Object);
    }

    [Fact]
    public async Task GetInputAsync_GivenSampleInput_AndUsingStartAsSource_ParsesCoordinates()
    {
        // Arrange
        var input = new[]
        {
            "Sabqponm",
            "abcryxxl",
            "accszExk",
            "acctuvwj",
            "abdefghi"
        };
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<AdventOfCodeChallengeSelection>()))
            .ReturnsAsync(string.Join("\n", input));

        // Act
        var result = await _inputProviderBuilder
            .BuildDay12InputProvider(Day12InputProviderBuilderExtensions.DijkstraType.StartIsSource)
            .GetInputAsync(new AdventOfCodeChallengeSelection(0, 0, 0))
            .ConfigureAwait(false);

        // Assert
        var expected = Day12TestHelpers.BuildSampleInput(Day12InputProviderBuilderExtensions.DijkstraType.StartIsSource);
        Assert.Equal(expected.Start, result.Start);
        Assert.Equal(expected.End, result.End);
        Assert.Equivalent(expected.Heightmap.GetVertices(), result.Heightmap.GetVertices());
        Assert.All(expected.Heightmap.GetVertices(), v => Assert.Equivalent(expected.Heightmap.GetEdges(v), result.Heightmap.GetEdges(v)));
        Assert.All(expected.Heightmap.GetVertices(), v => Assert.Equivalent(expected.Heightmap.GetNeighbors(v), result.Heightmap.GetNeighbors(v)));
    }

    [Fact]
    public async Task GetInputAsync_GivenSampleInput_AndUsingEndAsSource_ParsesCoordinates()
    {
        // Arrange
        var input = new[]
        {
            "Sabqponm",
            "abcryxxl",
            "accszExk",
            "acctuvwj",
            "abdefghi"
        };
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<AdventOfCodeChallengeSelection>()))
            .ReturnsAsync(string.Join("\n", input));

        // Act
        var result = await _inputProviderBuilder
            .BuildDay12InputProvider(Day12InputProviderBuilderExtensions.DijkstraType.EndIsSource)
            .GetInputAsync(new AdventOfCodeChallengeSelection(0, 0, 0))
            .ConfigureAwait(false);

        // Assert
        var expected = Day12TestHelpers.BuildSampleInput(Day12InputProviderBuilderExtensions.DijkstraType.EndIsSource);
        Assert.Equal(expected.Start, result.Start);
        Assert.Equal(expected.End, result.End);
        Assert.Equivalent(expected.Heightmap.GetVertices(), result.Heightmap.GetVertices());
        Assert.All(expected.Heightmap.GetVertices(), v => Assert.Equivalent(expected.Heightmap.GetEdges(v), result.Heightmap.GetEdges(v)));
        Assert.All(expected.Heightmap.GetVertices(), v => Assert.Equivalent(expected.Heightmap.GetNeighbors(v), result.Heightmap.GetNeighbors(v)));
    }
}