namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Tests.Day19;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day19;
using CodeChallenge.AdventOfCode.AdventOfCode2023.Day19.Models;
using CodeChallenge.AdventOfCode.AdventOfCode2023.Day19.Models.Rules;
using CodeChallenge.Core.IO;

public class Day19InputProviderBuilderExtensionsTests
{
    private readonly Mock<IInputReader<AdventOfCodeChallengeSelection>> _inputReaderMock;
    private readonly IInputProviderBuilder<AdventOfCodeChallengeSelection> _inputProviderBuilder;

    public Day19InputProviderBuilderExtensionsTests()
    {
        _inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        _inputProviderBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(_inputReaderMock.Object);
    }

    [Fact]
    public async Task GetInputAsync_GivenSampleInput_ParsesWorkflowEngineAndPartsFromChunks()
    {
        var input = new[]
        {
            "px{a<2006:qkq,m>2090:A,rfg}",
            "pv{a>1716:R,A}",
            "lnx{m>1548:A,A}",
            "rfg{s<537:gd,x>2440:R,A}",
            "qs{s>3448:A,lnx}",
            "qkq{x<1416:A,crn}",
            "crn{x>2662:A,R}",
            "in{s<1351:px,qqz}",
            "qqz{s>2770:qs,m<1801:hdj,R}",
            "gd{a>3333:R,R}",
            "hdj{m>838:A,pv}",
            "",
            "{x=787,m=2655,a=1222,s=2876}",
            "{x=1679,m=44,a=2067,s=496}",
            "{x=2036,m=264,a=79,s=2244}",
            "{x=2461,m=1339,a=466,s=291}",
            "{x=2127,m=1623,a=2188,s=1013}"
        };
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<AdventOfCodeChallengeSelection>()))
            .ReturnsAsync(string.Join("\n", input));

        var result = await _inputProviderBuilder.BuildDay19InputProvider()
            .GetInputAsync(new AdventOfCodeChallengeSelection(0, 0, 0))
            .ConfigureAwait(false);

        var expectedWorkflowEngine = new WorkflowEngine(new List<Workflow>
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

        var expectedParts = new List<Part>
        {
            new(787, 2655, 1222, 2876),
            new(1679, 44, 2067, 496),
            new(2036, 264, 79, 2244),
            new(2461, 1339, 466, 291),
            new(2127, 1623, 2188, 1013)
        };

        Assert.Collection(result.WorkflowEngine.Workflows.Values,
            expectedWorkflowEngine.Workflows.Values.Select<Workflow, Action<Workflow>>(expectedWorkflow => workflow =>
            {
                Assert.Equal(expectedWorkflow.WorkflowName, workflow.WorkflowName);
                Assert.Collection(workflow.Rules,
                    expectedWorkflow.Rules.Select<IRule, Action<IRule>>(expectedRule => (IRule rule) =>
                    {
                        Assert.Equal(expectedRule, rule);
                    }).ToArray()
                );
                Assert.Equal(expectedWorkflow.DefaultRule, workflow.DefaultRule);
            }).ToArray()
        );

        Assert.Collection(result.Parts,
            expectedParts.Select<Part, Action<Part>>(expectedPart => part => Assert.Equal(expectedPart, part)).ToArray()
        );
    }
}