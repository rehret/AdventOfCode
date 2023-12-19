namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day19.Models;

internal class Workflow(string workflowName, IEnumerable<IRule> rules, IRule defaultRule)
{
    public string WorkflowName { get; } = workflowName;

    public IEnumerable<IRule> Rules { get; } = rules;

    public IRule DefaultRule { get; } = defaultRule;

    public RuleResult CheckPart(Part part)
    {
        var result = Rules.Select(rule => rule.CheckPart(part)).FirstOrDefault(r => r is not ConditionNotMetResult);
        return result ?? DefaultRule.CheckPart(part);
    }
}