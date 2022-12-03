namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day03;

using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

using Microsoft.VisualBasic;

[AdventOfCodeSolution(2022, 3, 2)]
internal class Solution02 : AdventOfCodeSolution<IEnumerable<string>, int>
{
    private const int GroupSize = 3;

    public Solution02(IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<string>> inputProvider) : base(inputProvider) { }

    protected override int ComputeSolution(IEnumerable<string> input)
    {
        return input
            .Aggregate(
                // Group lines into groups of size `GroupSize`
                (FinalList: new List<List<string>>(), CurrentList: new List<string>()),
                (acc, rucksack) =>
                {
                    acc.CurrentList.Add(rucksack);
                    if (acc.CurrentList.Count != GroupSize) return acc;
                    acc.FinalList.Add(acc.CurrentList);
                    return (acc.FinalList, new List<string>());
                }
            )
            .FinalList
            .Select(group =>
            {
                // Get all distinct characters in the group and find the character which appears in all of them
                var distinctItems = group.SelectMany(rucksack => rucksack.ToCharArray()).Distinct();
                return distinctItems.Single(item => group.All(rucksack => rucksack.IndexOf(item) >= 0));
            })
            .Select(ItemScoreHelpers.GetItemScore)
            .Sum();
    }
}
