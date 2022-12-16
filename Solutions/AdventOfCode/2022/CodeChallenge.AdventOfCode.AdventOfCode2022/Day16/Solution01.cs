namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day16;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day16.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.Helpers.Math;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 16, 1)]
internal class Solution01 : AdventOfCodeSolution<Graph<Valve>, int>
{
    private const int TotalTimeInMinutes = 30;
    private const string StartingValve = "AA";
    private static readonly string[] StuckValves = { StartingValve };

    public Solution01(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
        : base(inputProviderBuilder.BuildDay16InputProvider())
    { }

    protected override int ComputeSolution(Graph<Valve> input)
    {
        var pressureReleased = 0;
        var pressurePerMinute = 0;
        var minute = 0;

        var current = input.GetVertices().Single(v => v.Label == StartingValve);
        var (target, path) = GetNextTarget(input, current, minute);

        while (minute < TotalTimeInMinutes)
        {
            if (target == null)
            {
                // All valves are open, just wait
                minute++;
                pressureReleased += pressurePerMinute;
            }
            else if (current == target)
            {
                target.IsOpen = true;
                minute++;
                pressureReleased += pressurePerMinute;
                pressurePerMinute += current.FlowRate;

                (target, path) = GetNextTarget(input, current, minute);
            }
            else
            {
                current = path.First();
                path.RemoveAt(0);
                minute++;
                pressureReleased += pressurePerMinute;
            }
        }

        return pressureReleased;
    }

    private static (Valve? Target, List<Valve> Path) GetNextTarget(Graph<Valve> graph, Valve current, int minute)
    {
        var targetAndPath = graph.GetVertices()
            .Where(v => v is { IsOpen: false, FlowRate: > 0 } && !StuckValves.Contains(v.Label))
            .Select(v => (Valve: v, Path: Dijkstra.GetShortestPath(graph, current, v).Skip(1).ToList()))
            .ToList();

        return targetAndPath.Any()
            ? targetAndPath.MaxBy(tuple => tuple.Valve.FlowRate / Math.Pow(tuple.Path.Count, 2))
            : (null, new List<Valve>());
    }
}
