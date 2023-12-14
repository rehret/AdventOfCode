namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day04;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day04.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2023, 4, 2)]
internal class Solution02(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
    : AdventOfCodeSolution<IEnumerable<ScratchCard>, int>(inputProviderBuilder.BuildDay04InputProvider())
{
    protected override int ComputeSolution(IEnumerable<ScratchCard> scratchCards)
    {
        var originalCards = scratchCards.OrderBy(card => card.CardNumber).ToArray();
        var cachedCardScores = new Dictionary<int, int>();
        var usedCards = originalCards.ToList();

        for (var i = 0; i < usedCards.Count; i++)
        {
            var card = usedCards[i];
            var cardScore = cachedCardScores.GetOrSet(card.CardNumber, () => ComputeCardScore(card));
            if (cardScore > 0)
            {
                usedCards.AddRange(originalCards[(card.CardNumber)..(card.CardNumber + cardScore)]);
            }
        }

        return usedCards.Count;
    }

    private static int ComputeCardScore(ScratchCard card) => card.ScratchedNumbers.Count(num => card.WinningNumbers.Contains(num));
}
