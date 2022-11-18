namespace CodeChallenge.Template.Solution;

using CodeChallenge.Core;

internal class SolutionTemplateInputReader : AbstractInputReader<SolutionTemplateChallengeSelection>
{
    protected override string GetInputFilePath(SolutionTemplateChallengeSelection challengeSelection) =>
        Path.Combine(
            Environment.CurrentDirectory,
            "Resources/CodeChallenge.Template.Solution/CodeChallenge.Template.Solution.txt"
        );
}