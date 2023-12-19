namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day19;

using System.Text.RegularExpressions;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day19.Models;
using CodeChallenge.AdventOfCode.AdventOfCode2023.Day19.Models.Rules;
using CodeChallenge.Core.IO;

internal static partial class Day19InputProviderBuilderExtensions
{
    public static IInputProvider<AdventOfCodeChallengeSelection, (WorkflowEngine WorkflowEngine, IEnumerable<Part> Parts)> BuildDay19InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder
    )
    {
        return builder
            .ReadChunks(string.IsNullOrWhiteSpace)
            .ParseUsing((IEnumerable<IEnumerable<string>> chunks) =>
            {
                var chunksArray = chunks as IEnumerable<string>[] ?? chunks.ToArray();
                var workflowChunk = chunksArray.First();
                var partsChunk = chunksArray.Last();

                var workflowEngine = ParseWorkflowEngine(workflowChunk);
                var parts = ParseParts(partsChunk);

                return (WorkflowEngine: workflowEngine, Parts: parts);
            })
            .Build();
    }

    private static WorkflowEngine ParseWorkflowEngine(IEnumerable<string> lines)
    {
        var workflows = lines.Select(line =>
        {
            var firstCurlyBraceIndex = line.IndexOf('{');
            var lastCurlyBraceIndex = line.LastIndexOf('}');
            var workflowName = line[..firstCurlyBraceIndex];
            var rules = line[(firstCurlyBraceIndex + 1)..lastCurlyBraceIndex]
                .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(ruleString => ruleString switch
                {
                    "A"                                  => new AcceptRule(),
                    "R"                                  => new RejectRule(),
                    _ when RuleRegex.IsMatch(ruleString) => ParseConditionalRule(ruleString),
                    _                                    => ParseDefaultRule(ruleString)
                })
                .ToList();

            return new Workflow(workflowName, rules.Take(rules.Count - 1), rules.Last());
        });

        return new WorkflowEngine(workflows.ToDictionary(workflow => workflow.WorkflowName));
    }

    private static IEnumerable<Part> ParseParts(IEnumerable<string> lines)
    {
        return lines.Select(line =>
        {
            var x = 0;
            var m = 0;
            var a = 0;
            var s = 0;

            var firstCurlyBraceIndex = line.IndexOf('{');
            var lastCurlyBraceIndex = line.LastIndexOf('}');

            var propertyDefinitions = line[(firstCurlyBraceIndex + 1)..lastCurlyBraceIndex]
                .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            foreach (var definition in propertyDefinitions)
            {
                var propertyAndValue = definition.Split('=', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                var property = propertyAndValue.First();
                var value = int.Parse(propertyAndValue.Last());

                switch (property)
                {
                    case "x":
                        x = value;
                        break;
                    case "m":
                        m = value;
                        break;
                    case "a":
                        a = value;
                        break;
                    case "s":
                        s = value;
                        break;
                    default:
                        throw new ArgumentException($"Unexpected Part property: '{property}'");
                }
            }

            return new Part(x, m, a, s);
        });
    }

    private static IRule ParseConditionalRule(string ruleString)
    {
        var partProperty = ruleString[0];
        var comparison = ruleString[1];
        var colonIndex = ruleString.IndexOf(':');
        var compareValue = int.Parse(ruleString[2..colonIndex]);
        var rulePassResult = ruleString[(colonIndex + 1)..];

        Func<Part, int> propertySelector = partProperty switch
        {
            'x' => PartPropertySelector.X,
            'm' => PartPropertySelector.M,
            'a' => PartPropertySelector.A,
            's' => PartPropertySelector.S,
            _   => throw new ArgumentOutOfRangeException(nameof(ruleString))
        };

        RuleResult ruleResult = rulePassResult switch
        {
            "A" => RuleResult.AcceptedResult,
            "R" => RuleResult.RejectedResult,
            _   => RuleResult.RedirectResult(rulePassResult)
        };

        return comparison == '<'
            ? new LessThanRule(propertySelector, compareValue, ruleResult)
            : new GreaterThanRule(propertySelector, compareValue, ruleResult);
    }

    private static IRule ParseDefaultRule(string ruleString)
    {
        return ruleString switch
        {
            "A" => new AcceptRule(),
            "R" => new RejectRule(),
            _   => new RedirectRule(ruleString)
        };
    }

    private static readonly Regex RuleRegex = GetRuleRegex();

    [GeneratedRegex(@"[xmas][<>]\d+")]
    private static partial Regex GetRuleRegex();
}