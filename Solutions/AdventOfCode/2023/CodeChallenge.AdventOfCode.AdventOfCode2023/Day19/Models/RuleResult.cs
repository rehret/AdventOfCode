namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day19.Models;

internal abstract record RuleResult
{
    public static readonly AcceptedResult AcceptedResult = new();
    public static readonly RejectedResult RejectedResult = new();
    public static RedirectResult RedirectResult(string workflowName) => new(workflowName);
    public static readonly ConditionNotMetResult ConditionNotMetResult = new();
}

internal abstract record AcceptedOrRejectedResult : RuleResult;

internal sealed record AcceptedResult : AcceptedOrRejectedResult;

internal sealed record RejectedResult : AcceptedOrRejectedResult;

internal sealed record RedirectResult(string WorkflowName) : RuleResult;

internal sealed record ConditionNotMetResult : RuleResult;