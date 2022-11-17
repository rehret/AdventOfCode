namespace CodeChallenge.AdventOfCode;

using CodeChallenge;

public record AdventOfCodeChallengeSelection(int Year, int Day, int Puzzle) : ChallengeSelection
{
    public override string ToString()
    {
        return $"AdventOfCode/{Year:0000}/{Day:00}/{Puzzle:00}";
    }
}