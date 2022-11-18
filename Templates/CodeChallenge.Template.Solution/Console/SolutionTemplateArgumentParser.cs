namespace CodeChallenge.Template.Solution.Console;

using CodeChallenge.Core;
using CodeChallenge.Core.Console;
using CodeChallenge.Core.IO;

/// <inheritdoc cref="AbstractChallengeArgumentParser" />
internal class SolutionTemplateArgumentParser : AbstractChallengeArgumentParser
{
    private static readonly string[] StaticAliases = { "CodeChallenge.Template.Solution" };
    private static readonly string[] StaticArgumentPartNames = { };

    public override string DisplayName => "CodeChallenge.Template.Solution";

    public override string[] Aliases => StaticAliases;
    public override string[] ArgumentPartNames => StaticArgumentPartNames;

    public override bool TryParse(string remainingArguments, out ChallengeSelection challengeSelection)
    {
        challengeSelection = new SolutionTemplateChallengeSelection();
        return true;
    }
}