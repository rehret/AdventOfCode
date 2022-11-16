namespace AdventOfCode;

[AttributeUsage(AttributeTargets.Class)]
public class SolutionAttribute : Attribute
{
    public int Year { get; }

    public int Day { get; }

    public int Puzzle { get; }

    public SolutionAttribute(int year, int day, int puzzle)
    {
        Year = year;
        Day = day;
        Puzzle = puzzle;
    }
}