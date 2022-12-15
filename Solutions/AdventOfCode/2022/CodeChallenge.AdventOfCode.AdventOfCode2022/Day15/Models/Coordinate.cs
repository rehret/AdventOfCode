namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day15.Models;

internal record Coordinate(int X, int Y)
{
    public int GetDistanceTo(Coordinate other)
    {
        return Math.Abs(other.X - X) + Math.Abs(other.Y - Y);
    }
}