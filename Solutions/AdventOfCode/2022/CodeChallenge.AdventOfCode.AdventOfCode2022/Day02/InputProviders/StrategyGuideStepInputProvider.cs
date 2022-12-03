namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.InputProviders;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.Models;
using CodeChallenge.Core.IO;
using CodeChallenge.Core.IO.InputProviders;

internal class StrategyGuideStepInputProvider
    : AbstractInputProvider<AdventOfCodeChallengeSelection, IEnumerable<StrategyGuideStep>>
{
    private readonly StrategyGuideStepInputProviderType _type;

    public StrategyGuideStepInputProvider(IInputReader<AdventOfCodeChallengeSelection> inputReader, StrategyGuideStepInputProviderType type)
        : base(inputReader)
    {
        _type = type;
    }

    protected override IEnumerable<StrategyGuideStep> ParseLines(IEnumerable<string> lines)
    {
        return lines
            .Select(line => line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
            .Select(parts => (OpponentsMove: parts[0], SuggestedMoveOrTargetType: parts[1]))
            .Select(parts => BuildStrategyGuideStep(_type, parts));
    }

    private static RockPaperScissorsMove ParseMove(string suggestedMove) => suggestedMove switch
    {
        "A" or "X" => RockPaperScissorsMove.Rock,
        "B" or "Y" => RockPaperScissorsMove.Paper,
        "C" or "Z" => RockPaperScissorsMove.Scissors,
        _   => throw new ArgumentOutOfRangeException(nameof(suggestedMove), suggestedMove, null)
    };

    private static RockPaperScissorsResult ParseTargetResult(string targetResult) => targetResult switch
    {
        "X" => RockPaperScissorsResult.Lose,
        "Y" => RockPaperScissorsResult.Draw,
        "Z" => RockPaperScissorsResult.Win,
        _   => throw new ArgumentOutOfRangeException(nameof(targetResult), targetResult, null)
    };

    private static StrategyGuideStep BuildStrategyGuideStep(
        StrategyGuideStepInputProviderType type,
        (string OpponentsMove, string SuggestedMoveOrTargetType) parts
    )
        => type switch
        {
            StrategyGuideStepInputProviderType.SuggestedMove =>
                new StrategyGuideStep(ParseMove(parts.SuggestedMoveOrTargetType), ParseMove(parts.OpponentsMove)),
            StrategyGuideStepInputProviderType.TargetResult =>
                new StrategyGuideStep(ParseTargetResult(parts.SuggestedMoveOrTargetType), ParseMove(parts.OpponentsMove)),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
}