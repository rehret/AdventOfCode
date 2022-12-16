namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day16.Models;

internal record Valve(string Label, int FlowRate)
{
    public bool IsOpen { get; set; }
}