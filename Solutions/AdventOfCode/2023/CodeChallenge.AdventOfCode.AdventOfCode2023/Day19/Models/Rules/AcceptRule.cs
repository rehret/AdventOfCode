namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day19.Models.Rules;

internal record AcceptRule : IRule
{
    public RuleResult CheckPart(Part part) => RuleResult.AcceptedResult;
}