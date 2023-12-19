namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day19.Models.Rules;

internal record RedirectRule(string WorkflowName) : IRule
{
    public RuleResult CheckPart(Part part) => RuleResult.RedirectResult(WorkflowName);
}