namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.Models;

internal record RockPaperScissorsMove
{
    public int Score { get; }

    private RockPaperScissorsMove(int score)
    {
        Score = score;
    }

    internal static readonly RockPaperScissorsMove Rock = new RockMove();
    internal static readonly RockPaperScissorsMove Paper = new PaperMove();
    internal static readonly RockPaperScissorsMove Scissors = new ScissorsMove();

    private sealed record RockMove() : RockPaperScissorsMove(1);
    private sealed record PaperMove() : RockPaperScissorsMove(2);
    private sealed record ScissorsMove() : RockPaperScissorsMove(3);

    public RockPaperScissorsResult GetResultAgainstMove(RockPaperScissorsMove opponentsMove) =>
        GetPlayerResult(this, opponentsMove);

    public static RockPaperScissorsResult GetPlayerResult(
        RockPaperScissorsMove playersMove,
        RockPaperScissorsMove opponentsMove
    ) => playersMove switch
    {
        RockMove when opponentsMove is RockMove         => RockPaperScissorsResult.Draw,
        RockMove when opponentsMove is PaperMove        => RockPaperScissorsResult.Lose,
        RockMove when opponentsMove is ScissorsMove     => RockPaperScissorsResult.Win,
        RockMove     => throw new ArgumentOutOfRangeException(nameof(opponentsMove), opponentsMove, null),

        PaperMove when opponentsMove is RockMove        => RockPaperScissorsResult.Win,
        PaperMove when opponentsMove is PaperMove       => RockPaperScissorsResult.Draw,
        PaperMove when opponentsMove is ScissorsMove    => RockPaperScissorsResult.Lose,
        PaperMove    => throw new ArgumentOutOfRangeException(nameof(opponentsMove), opponentsMove, null),

        ScissorsMove when opponentsMove is RockMove     => RockPaperScissorsResult.Lose,
        ScissorsMove when opponentsMove is PaperMove    => RockPaperScissorsResult.Win,
        ScissorsMove when opponentsMove is ScissorsMove => RockPaperScissorsResult.Draw,
        ScissorsMove => throw new ArgumentOutOfRangeException(nameof(opponentsMove), opponentsMove, null),

        _ => throw new ArgumentOutOfRangeException(nameof(playersMove), playersMove, null)
    };

    public static RockPaperScissorsMove GetSuggestedMoveFromTargetResult(
        RockPaperScissorsResult rockPaperScissorsResult,
        RockPaperScissorsMove opponentsMove
    ) => rockPaperScissorsResult switch
    {
        RockPaperScissorsResult.WinResult when opponentsMove is RockMove      => Paper,
        RockPaperScissorsResult.WinResult when opponentsMove is PaperMove     => Scissors,
        RockPaperScissorsResult.WinResult when opponentsMove is ScissorsMove  => Rock,
        RockPaperScissorsResult.WinResult  => throw new ArgumentOutOfRangeException(nameof(opponentsMove), opponentsMove, null),

        RockPaperScissorsResult.DrawResult                                    => opponentsMove,

        RockPaperScissorsResult.LoseResult when opponentsMove is RockMove     => Scissors,
        RockPaperScissorsResult.LoseResult when opponentsMove is PaperMove    => Rock,
        RockPaperScissorsResult.LoseResult when opponentsMove is ScissorsMove => Paper,
        RockPaperScissorsResult.LoseResult => throw new ArgumentOutOfRangeException(nameof(opponentsMove), opponentsMove, null),

        _ => throw new ArgumentOutOfRangeException(nameof(rockPaperScissorsResult), rockPaperScissorsResult, null)
    };
}