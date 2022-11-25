namespace CodeChallenge.Core;

using System.Diagnostics;
using System.Reflection;

public abstract class AbstractSolution<TSolutionAttribute, TChallengeSelection> : ISolution
    where TSolutionAttribute : Attribute
{
    public abstract Task<string> SolveAsync(Stopwatch? stopwatch = null);

    protected TChallengeSelection GetChallengeSelection()
    {
        var attribute = GetType().GetCustomAttribute<TSolutionAttribute>();
        if (attribute == null)
        {
            throw new Exception($"Solution '{GetType().FullName}' does not have a SolutionAttribute");
        }

        return BuildChallengeSolutionFromAttribute(attribute);
    }

    protected abstract TChallengeSelection BuildChallengeSolutionFromAttribute(TSolutionAttribute attribute);
}