namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day19.Models;

internal class WorkflowEngine(IDictionary<string, Workflow> workflows)
{
    private const string StartingWorkflowName = "in";

    public IDictionary<string, Workflow> Workflows { get; } = workflows;

    public AcceptedOrRejectedResult ProcessPart(Part part)
    {
        RuleResult result = RuleResult.RedirectResult(StartingWorkflowName);
        var workflow = Workflows[StartingWorkflowName];

        while (result is not AcceptedOrRejectedResult)
        {
            result = workflow.CheckPart(part);
            if (result is RedirectResult redirectResult)
            {
                workflow = Workflows[redirectResult.WorkflowName];
            }
        }

        return (AcceptedOrRejectedResult)result;
    }
}