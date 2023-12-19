namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day19.Models.Rules;

internal record RejectRule : IRule
{
    public RuleResult CheckPart(Part part) => RuleResult.RejectedResult;
}