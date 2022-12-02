namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.Models;

internal record RockPaperScissorsResult
{
    public int Score { get; }

    private RockPaperScissorsResult(int score)
    {
        Score = score;
    }

    internal static readonly RockPaperScissorsResult Win = new WinResult();
    internal static readonly RockPaperScissorsResult Draw = new DrawResult();
    internal static readonly RockPaperScissorsResult Lose = new LoseResult();

    internal sealed record WinResult() : RockPaperScissorsResult(6);
    internal sealed record DrawResult() : RockPaperScissorsResult(3);
    internal sealed record LoseResult() : RockPaperScissorsResult(0);
}