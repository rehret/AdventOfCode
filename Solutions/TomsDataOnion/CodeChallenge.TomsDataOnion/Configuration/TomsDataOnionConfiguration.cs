namespace CodeChallenge.TomsDataOnion.Configuration;

using CodeChallenge.Core.Configuration;

internal class TomsDataOnionConfiguration : ICodeChallengeConfiguration
{
    public string OutputFileSuffix { get; set; } = "";
}