namespace CodeChallenge.TomsDataOnion.Configuration;

using CodeChallenge.Configuration;

internal class TomsDataOnionConfiguration : ICodeChallengeConfiguration
{
    public bool UseSimpleBase { get; set; } = false;

    public string OutputFileSuffix { get; set; } = "";
}