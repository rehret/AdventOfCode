namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day12;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day12.Models;
using CodeChallenge.Core.Helpers.Math;
using CodeChallenge.Core.IO;

internal static class Day12InputProviderBuilderExtensions
{
    public enum DijkstraType
    {
        StartIsSource,
        EndIsSource
    }

    public static IInputProvider<AdventOfCodeChallengeSelection, HeightmapWithStartAndEnd> BuildDay12InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder,
        DijkstraType type
    )
    {
        return builder
            .ReadLines(StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ParseUsing<HeightmapWithStartAndEnd>(lines => Parse(lines, type))
            .Build();
    }

    private static HeightmapWithStartAndEnd Parse(IEnumerable<string> lines, DijkstraType type)
    {
        var coordinates = new List<Coordinate>();
        var start = new Coordinate(0, 0, 0);
        var end = new Coordinate(0, 0, 0);

        var linesArray = lines.ToArray();
        for (var y = 0; y < linesArray.Length; y++)
        {
            var line = linesArray[y].ToCharArray();
            for (var x = 0; x < line.Length; x++)
            {
                var @char = line[x];
                switch (@char)
                {
                    case 'S':
                        start = new Coordinate(x, y, 0);
                        coordinates.Add(start);
                        break;
                    case 'E':
                        end = new Coordinate(x, y, 'z' - 'a');
                        coordinates.Add(end);
                        break;
                    default:
                        coordinates.Add(new Coordinate(x, y, @char - 'a'));
                        break;
                }
            }
        }

        var graph = new Graph<Coordinate>();
        foreach (var coordinate in coordinates)
        {
            graph.AddVertex(coordinate);
            foreach (var neighbor in GetNeighbors(coordinates, coordinate, type))
            {
                graph.AddEdge(coordinate, neighbor);
            }
        }

        return new HeightmapWithStartAndEnd(graph, start, end);
    }

    private static IEnumerable<Coordinate>GetNeighbors(IEnumerable<Coordinate> coordinates, Coordinate coordinate, DijkstraType type)
    {
        return coordinates
            .Where(x =>
                (x.X == coordinate.X - 1 && x.Y == coordinate.Y)
                || (x.X == coordinate.X + 1 && x.Y == coordinate.Y)
                || (x.X == coordinate.X && x.Y == coordinate.Y - 1)
                || (x.X == coordinate.X && x.Y == coordinate.Y + 1))
            .Where(x => type == DijkstraType.StartIsSource
                ? x.Height <= coordinate.Height + 1 // When Start is source, we find coordinates whose heights are at most 1 higher than the current coordinate
                : x.Height >= coordinate.Height - 1); // When End is source, we find coordinates whose heights are at most 1 less than the current coordinate
    }
}