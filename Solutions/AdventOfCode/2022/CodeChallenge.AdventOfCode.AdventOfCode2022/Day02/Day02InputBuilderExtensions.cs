namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day02;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.Models;
using CodeChallenge.Core.IO;

internal static class Day02InputBuilderExtensions
{
    public static IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<StrategyGuideStep>> BuildDay02InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder,
        StrategyGuideStepInputProviderType type
    )
    {
        return builder
            .ReadLines(StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ParseUsing(line =>
            {
                var parts = line.Split(' ', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                return BuildStrategyGuideStep(type, parts[0], parts[1]);
            })
            .Build();
    }

    private static StrategyGuideStep BuildStrategyGuideStep(
        StrategyGuideStepInputProviderType type,
        string opponentsMove,
        string suggestedMoveOrTargetType
    ) => type switch
    {
        StrategyGuideStepInputProviderType.SuggestedMove =>
            new StrategyGuideStep(ParseMove(suggestedMoveOrTargetType), ParseMove(opponentsMove)),
        StrategyGuideStepInputProviderType.TargetResult =>
            new StrategyGuideStep(ParseTargetResult(suggestedMoveOrTargetType), ParseMove(opponentsMove)),
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
    };

    private static RockPaperScissorsMove ParseMove(string suggestedMove) => suggestedMove switch
    {
        "A" or "X" => RockPaperScissorsMove.Rock,
        "B" or "Y" => RockPaperScissorsMove.Paper,
        "C" or "Z" => RockPaperScissorsMove.Scissors,
        _          => throw new ArgumentOutOfRangeException(nameof(suggestedMove), suggestedMove, null)
    };

    private static RockPaperScissorsResult ParseTargetResult(string targetResult) => targetResult switch
    {
        "X" => RockPaperScissorsResult.Lose,
        "Y" => RockPaperScissorsResult.Draw,
        "Z" => RockPaperScissorsResult.Win,
        _   => throw new ArgumentOutOfRangeException(nameof(targetResult), targetResult, null)
    };
}

internal enum StrategyGuideStepInputProviderType
{
    SuggestedMove,
    TargetResult
}