namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.InputProviders;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.Models;
using CodeChallenge.Core.IO;
using CodeChallenge.Core.IO.InputProviders;

internal class StrategyGuideStepInputProvider
    : AbstractInputProvider<AdventOfCodeChallengeSelection, IEnumerable<StrategyGuideStep>>, IStrategyGuideStepInputProvider
{
    public bool UseTargetResultInput { get; set; } = false;

    public StrategyGuideStepInputProvider(IInputReader<AdventOfCodeChallengeSelection> inputReader)
        : base(inputReader)
    { }

    protected override IEnumerable<StrategyGuideStep> ParseLines(IEnumerable<string> lines)
    {
        return lines
            .Select(line => line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
            .Select(parts => (OpponentsMove: parts[0], SuggestedMove: parts[1]))
            .Select(parts => UseTargetResultInput
                ? new StrategyGuideStep(ParseOpponentsMove(parts.OpponentsMove), ParseTargetResult(parts.SuggestedMove))
                : new StrategyGuideStep(ParseOpponentsMove(parts.OpponentsMove), ParseSuggestedMove(parts.SuggestedMove)));
    }

    private static RockPaperScissorsMove ParseOpponentsMove(string opponentsMove) => opponentsMove switch
    {
        "A" => RockPaperScissorsMove.Rock,
        "B" => RockPaperScissorsMove.Paper,
        "C" => RockPaperScissorsMove.Scissors,
        _   => throw new ArgumentOutOfRangeException(nameof(opponentsMove))
    };

    private static RockPaperScissorsMove ParseSuggestedMove(string suggestedMove) => suggestedMove switch
    {
        "X" => RockPaperScissorsMove.Rock,
        "Y" => RockPaperScissorsMove.Paper,
        "Z" => RockPaperScissorsMove.Scissors,
        _   => throw new ArgumentOutOfRangeException(nameof(suggestedMove))
    };

    private static TargetResult ParseTargetResult(string targetResult) => targetResult switch
    {
        "X" => TargetResult.Lose,
        "Y" => TargetResult.Draw,
        "Z" => TargetResult.Win,
        _   => throw new ArgumentOutOfRangeException(nameof(targetResult))
    };
}