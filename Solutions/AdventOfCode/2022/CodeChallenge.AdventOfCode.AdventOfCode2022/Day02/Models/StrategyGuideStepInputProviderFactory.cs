namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.Models;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.InputProviders;
using CodeChallenge.Core.IO;

internal delegate IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<StrategyGuideStep>> StrategyGuideStepInputProviderFactory(StrategyGuideStepInputProviderType type);
