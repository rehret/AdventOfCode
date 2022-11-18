namespace CodeChallenge.AdventOfCode.Attributes;

using CodeChallenge.Core;
using CodeChallenge.Core.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class AdventOfCodeSolutionAttribute : SolutionAttribute
{
    public int Year { get; }

    public int Day { get; }

    public int Puzzle { get; }

    public AdventOfCodeSolutionAttribute(int year, int day, int puzzle)
    {
        Year = year;
        Day = day;
        Puzzle = puzzle;
    }

    public override ChallengeSelection ToChallengeSelection()
    {
        return new AdventOfCodeChallengeSelection(Year, Day, Puzzle);
    }
}