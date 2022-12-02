namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.InputProviders;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.Models;
using CodeChallenge.Core.IO;

internal interface IStrategyGuideStepInputProvider : IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<StrategyGuideStep>>
{
    public bool UseTargetResultInput { get; set; }
}