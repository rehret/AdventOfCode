namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day16;

using System.Text.RegularExpressions;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day16.Models;
using CodeChallenge.Core.Extensions;
using CodeChallenge.Core.Helpers.Math;
using CodeChallenge.Core.IO;

internal static partial class Day16InputProviderBuilderExtensions
{
    public static IInputProvider<AdventOfCodeChallengeSelection, Graph<Valve>> BuildDay16InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder
    )
    {
        return builder
            .ReadLines(StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ParseUsing((IEnumerable<string> lines) =>
            {
                var (valves, connections) = lines.Select(line =>
                {
                    var match = GetInputLineRegex().Match(line);
                    if (!match.Success)
                    {
                        throw new FormatException($"Could not parse {nameof(Valve)} from '{line}'");
                    }

                    var valve = new Valve(match.Groups["Label"].Value, int.Parse(match.Groups["FlowRate"].Value));
                    var valveConnections = match.Groups["Connections"]
                        .Value
                        .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                        .Select(otherLabel => (valve.Label, OtherLabel: otherLabel));

                    return (valve, connections: valveConnections);
                });

                var valveList = valves.ToList();
                var valveLookup = valveList.ToDictionary(valve => valve.Label);
                var graph = new Graph<Valve>();
                foreach (var valve in valveList)
                {
                    graph.AddVertex(valve);
                }

                foreach (var connection in connections.SelectMany(x => x))
                {
                    graph.AddEdge(valveLookup[connection.Label], valveLookup[connection.OtherLabel]);
                }

                return graph;
            })
            .Build();
    }

    [GeneratedRegex(@"^Valve (?<Label>[A-Z]{2}) has flow rate=(?<FlowRate>\d+); tunnel(s)? lead(s)? to valve(s)? (?<Connections>([A-Z]{2},\s+)*[A-Z]{2})$")]
    private static partial Regex GetInputLineRegex();
}