namespace CodeChallenge.TomsDataOnion.Configuration;

using CodeChallenge.Core.Configuration;

internal class TomsDataOnionConfiguration : ICodeChallengeConfiguration
{
    public bool UseSimpleBase { get; set; } = false;

    public string OutputFileSuffix { get; set; } = "";
}