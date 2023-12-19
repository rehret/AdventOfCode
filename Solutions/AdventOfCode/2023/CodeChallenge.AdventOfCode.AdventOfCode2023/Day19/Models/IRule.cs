namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day19.Models;

internal interface IRule
{
    RuleResult CheckPart(Part part);
}