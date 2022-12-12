namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day12;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day12;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day12.Models;
using CodeChallenge.Core.Helpers.Math;
using CodeChallenge.Core.IO;

public class Solution02Tests
{
    private readonly Solution02 _solution;

    public Solution02Tests()
    {
        var inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        var inputProviderBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(inputReaderMock.Object);
        _solution = new Solution02(inputProviderBuilder);
    }

    [Fact]
    public async Task ComputeSolutionAsync_WithSampleInput_ProducesSampleOutput()
    {
        // Arrange
        var coordinates = new List<Coordinate>
        {
            new(0, 0, 'a' - 'a'),
            new(1, 0, 'a' - 'a'),
            new(2, 0, 'b' - 'a'),
            new(3, 0, 'q' - 'a'),
            new(4, 0, 'p' - 'a'),
            new(5, 0, 'o' - 'a'),
            new(6, 0, 'n' - 'a'),
            new(7, 0, 'm' - 'a'),
            new(0, 1, 'a' - 'a'),
            new(1, 1, 'b' - 'a'),
            new(2, 1, 'c' - 'a'),
            new(3, 1, 'r' - 'a'),
            new(4, 1, 'y' - 'a'),
            new(5, 1, 'x' - 'a'),
            new(6, 1, 'x' - 'a'),
            new(7, 1, 'l' - 'a'),
            new(0, 2, 'a' - 'a'),
            new(1, 2, 'c' - 'a'),
            new(2, 2, 'c' - 'a'),
            new(3, 2, 's' - 'a'),
            new(4, 2, 'z' - 'a'),
            new(5, 2, 'z' - 'a'),
            new(6, 2, 'x' - 'a'),
            new(7, 2, 'k' - 'a'),
            new(0, 3, 'a' - 'a'),
            new(1, 3, 'c' - 'a'),
            new(2, 3, 'c' - 'a'),
            new(3, 3, 't' - 'a'),
            new(4, 3, 'u' - 'a'),
            new(5, 3, 'v' - 'a'),
            new(6, 3, 'w' - 'a'),
            new(7, 3, 'j' - 'a'),
            new(0, 4, 'a' - 'a'),
            new(1, 4, 'b' - 'a'),
            new(2, 4, 'd' - 'a'),
            new(3, 4, 'e' - 'a'),
            new(4, 4, 'f' - 'a'),
            new(5, 4, 'g' - 'a'),
            new(6, 4, 'h' - 'a'),
            new(7, 4, 'i' - 'a')
        };
        var start = coordinates.Single(x => x is { X: 0, Y: 0 });
        var end = coordinates.Single(x => x is { X: 5, Y: 2 });

        var graph = new Graph<Coordinate>();
        foreach (var coordinate in coordinates)
        {
            graph.AddVertex(coordinate);
            foreach (var neighbor in GetNeighbors(coordinates, coordinate))
            {
                graph.AddEdge(coordinate, neighbor);
            }
        }

        var input = new HeightmapWithStartAndEnd(graph, start, end);

        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal(29, result);
    }

    private static IEnumerable<Coordinate>GetNeighbors(IEnumerable<Coordinate> coordinates, Coordinate coordinate)
    {
        return coordinates
            .Where(x =>
                (x.X == coordinate.X - 1 && x.Y == coordinate.Y)
                || (x.X == coordinate.X + 1 && x.Y == coordinate.Y)
                || (x.X == coordinate.X && x.Y == coordinate.Y - 1)
                || (x.X == coordinate.X && x.Y == coordinate.Y + 1))
            .Where(x => x.Height >= coordinate.Height - 1);
    }
}