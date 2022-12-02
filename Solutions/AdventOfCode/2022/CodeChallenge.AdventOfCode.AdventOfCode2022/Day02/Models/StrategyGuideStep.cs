namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.Models;

internal record StrategyGuideStep(RockPaperScissorsMove PlayersMove, RockPaperScissorsMove OpponentsMove)
{
    public StrategyGuideStep(RockPaperScissorsResult rockPaperScissorsResult, RockPaperScissorsMove opponentsMove)
        : this(RockPaperScissorsMove.GetSuggestedMoveFromTargetResult(rockPaperScissorsResult, opponentsMove), opponentsMove)
    { }
}