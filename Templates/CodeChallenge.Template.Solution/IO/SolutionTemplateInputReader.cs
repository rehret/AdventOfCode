namespace CodeChallenge.Template.Solution.IO;

using CodeChallenge.Core;
using CodeChallenge.Core.IO;

internal class SolutionTemplateInputReader : AbstractInputReader<SolutionTemplateChallengeSelection>
{
    protected override string GetInputFilePath(SolutionTemplateChallengeSelection challengeSelection) =>
        Path.Combine(
            Environment.CurrentDirectory,
            "Resources/CodeChallenge.Template.Solution/CodeChallenge.Template.Solution.txt"
        );
}