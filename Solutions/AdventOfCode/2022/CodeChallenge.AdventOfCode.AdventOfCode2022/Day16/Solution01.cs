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

    public Solution01(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
        : base(inputProviderBuilder.BuildDay16InputProvider())
    { }

    protected override int ComputeSolution(Graph<Valve> input)
    {
        var valves = input.GetVertices();
        var start = valves.Single(v => v.Label == StartingValve);
        var targetValves = valves.Where(v => !v.Equals(start) && v.FlowRate > 0);

        var initialState = new State(input, 0, 0, 0, start, targetValves);
        return GetMaxFlow(initialState);
    }

    private int GetMaxFlow(State state)
    {
        int ComputeRemainingFlow()
        {
            return state.FlowRate * (TotalTimeInMinutes - state.Minute);
        }

        if (!state.RemainingValves.Any())
        {
            return state.TotalFlow + ComputeRemainingFlow();
        }

        return state.RemainingValves
            .Select(valve =>
            {
                var distance = GetDistance(state.Graph, state.CurrentValve, valve);

                if (distance >= TotalTimeInMinutes - state.Minute)
                {
                    return state.TotalFlow + ComputeRemainingFlow();
                }

                var newState = state with
                {
                    TotalFlow = state.TotalFlow + state.FlowRate * distance,
                    Minute = state.Minute + distance,
                    CurrentValve = valve,
                    FlowRate = state.FlowRate + valve.FlowRate,
                    RemainingValves = state.RemainingValves.Where(v => !v.Equals(valve))
                };

                return GetMaxFlow(newState);
            })
            .Max();
    }

    private readonly IDictionary<(Valve, Valve), int> _distanceCache = new Dictionary<(Valve, Valve), int>();
    private int GetDistance(Graph<Valve> graph, Valve start, Valve end)
    {
        if (_distanceCache.TryGetValue((start, end), out var distance))
        {
            return distance;
        }

        if (_distanceCache.TryGetValue((end, start), out var reverseDistance))
        {
            return reverseDistance;
        }

        _distanceCache.Add((start, end), Dijkstra.GetShortestPath(graph, start, end).Count());
        return _distanceCache[(start, end)];
    }

    private record State(Graph<Valve> Graph, int Minute, int FlowRate, int TotalFlow, Valve CurrentValve, IEnumerable<Valve> RemainingValves);
}