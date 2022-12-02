namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.Models;

internal record StrategyGuideStep(RockPaperScissorsMove OpponentsMove, RockPaperScissorsMove SuggestedMove)
{
    public StrategyGuideStep(RockPaperScissorsMove opponentsMove, TargetResult targetResult)
        : this(opponentsMove, GetSuggestedMoveFromTargetResult(opponentsMove, targetResult))
    { }

    public int GetRoundScore() => GetMoveScore() + GetResultScore();

    private int GetResultScore() => IsWinningMove() ? 6 : IsDraw() ? 3 : 0;

    private bool IsWinningMove()
    {
        return OpponentsMove switch
        {
            RockPaperScissorsMove.Rock     => SuggestedMove is RockPaperScissorsMove.Paper,
            RockPaperScissorsMove.Paper    => SuggestedMove is RockPaperScissorsMove.Scissors,
            RockPaperScissorsMove.Scissors => SuggestedMove is RockPaperScissorsMove.Rock,
            _                              => throw new ArgumentOutOfRangeException()
        };
    }

    private bool IsDraw() => OpponentsMove == SuggestedMove;

    private int GetMoveScore() => SuggestedMove switch
    {
        RockPaperScissorsMove.Rock     => 1,
        RockPaperScissorsMove.Paper    => 2,
        RockPaperScissorsMove.Scissors => 3,
        _                              => throw new ArgumentOutOfRangeException()
    };

    private static RockPaperScissorsMove GetSuggestedMoveFromTargetResult(
        RockPaperScissorsMove opponentsMove,
        TargetResult targetResult
    ) =>
        targetResult switch
        {
            TargetResult.Win => opponentsMove switch
            {
                RockPaperScissorsMove.Rock     => RockPaperScissorsMove.Paper,
                RockPaperScissorsMove.Paper    => RockPaperScissorsMove.Scissors,
                RockPaperScissorsMove.Scissors => RockPaperScissorsMove.Rock,
                _                              => throw new ArgumentOutOfRangeException(nameof(OpponentsMove))
            },
            TargetResult.Draw => opponentsMove,
            TargetResult.Lose => opponentsMove switch
            {
                RockPaperScissorsMove.Rock     => RockPaperScissorsMove.Scissors,
                RockPaperScissorsMove.Paper    => RockPaperScissorsMove.Rock,
                RockPaperScissorsMove.Scissors => RockPaperScissorsMove.Paper,
                _                              => throw new ArgumentException(nameof(opponentsMove))
            },
            _ => throw new ArgumentOutOfRangeException(nameof(targetResult))
        };
}