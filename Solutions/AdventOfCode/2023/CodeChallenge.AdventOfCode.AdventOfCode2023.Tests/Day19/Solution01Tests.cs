namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Tests.Day19;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day19;
using CodeChallenge.AdventOfCode.AdventOfCode2023.Day19.Models;
using CodeChallenge.AdventOfCode.AdventOfCode2023.Day19.Models.Rules;
using CodeChallenge.Core.IO;

public class Solution01Tests
{
    private readonly Solution01 _solution;

    public Solution01Tests()
    {
        var inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        var inputProviderBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(inputReaderMock.Object);
        _solution = new Solution01(inputProviderBuilder);
    }

    [Fact]
    public async Task ComputeSolutionAsync_GivenSampleInput_ProducesSampleOutput()
    {
        var workflowEngine = new WorkflowEngine(new List<Workflow>
        {
            new("px",
                new IRule[]
                {
                    new LessThanRule(PartPropertySelector.A, 2006, RuleResult.RedirectResult("qkq")),
                    new GreaterThanRule(PartPropertySelector.M, 2090, RuleResult.AcceptedResult)
                },
                new RedirectRule("rfg")),
            new("pv",
                new IRule[]
                {
                    new GreaterThanRule(PartPropertySelector.A, 1716, RuleResult.RejectedResult)
                },
                new AcceptRule()),
            new("lnx",
                new IRule[]
                {
                    new GreaterThanRule(PartPropertySelector.M, 1548, RuleResult.AcceptedResult)
                },
                new AcceptRule()),
            new ("rfg",
                new IRule[]
                {
                    new LessThanRule(PartPropertySelector.S, 537, RuleResult.RedirectResult("gd")),
                    new GreaterThanRule(PartPropertySelector.X, 2440, RuleResult.RejectedResult)
                },
                new AcceptRule()),
            new ("qs",
                new IRule[]
                {
                    new GreaterThanRule(PartPropertySelector.S, 3448, RuleResult.AcceptedResult)
                },
                new RedirectRule("lnx")),
            new ("qkq",
                new IRule[]
                {
                    new LessThanRule(PartPropertySelector.X, 1416, RuleResult.AcceptedResult)
                },
                new RedirectRule("crn")),
            new ("crn",
                new IRule[]
                {
                    new GreaterThanRule(PartPropertySelector.X, 2662, RuleResult.AcceptedResult)
                },
                new RejectRule()),
            new ("in",
                new IRule[]
                {
                    new LessThanRule(PartPropertySelector.S, 1351, RuleResult.RedirectResult("px"))
                },
                new RedirectRule("qqz")),
            new ("qqz",
                new IRule[]
                {
                    new GreaterThanRule(PartPropertySelector.S, 2770, RuleResult.RedirectResult("qs")),
                    new LessThanRule(PartPropertySelector.M, 1801, RuleResult.RedirectResult("hdj"))
                },
                new RejectRule()),
            new ("gd",
                new IRule[]
                {
                    new GreaterThanRule(PartPropertySelector.A, 3333, RuleResult.RejectedResult)
                },
                new RejectRule()),
            new ("hdj",
                new IRule[]
                {
                    new GreaterThanRule(PartPropertySelector.M, 838, RuleResult.AcceptedResult)
                },
                new RedirectRule("pv"))
        }.ToDictionary(workflow => workflow.WorkflowName));

        var parts = new List<Part>
        {
            new(787, 2655, 1222, 2876),
            new(1679, 44, 2067, 496),
            new(2036, 264, 79, 2244),
            new(2461, 1339, 466, 291),
            new(2127, 1623, 2188, 1013)
        };

        var result = await _solution.ComputeSolutionAsync((WorkflowEngine: workflowEngine, Parts: parts))
            .ConfigureAwait(false);

        Assert.Equal(19114, result);
    }
}