namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day19.Models.Rules;

internal record GreaterThanRule(Func<Part, int> ConditionPropertyAccessor, int ComparisonValue, RuleResult ConditionPassedResult) : IRule
{
    public RuleResult CheckPart(Part part)
    {
        return ConditionPropertyAccessor(part) > ComparisonValue
            ? ConditionPassedResult
            : RuleResult.ConditionNotMetResult;
    }
}