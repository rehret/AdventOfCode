namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day03;

internal static class ItemScoreHelpers
{
    private const int UnicodeLowerCaseLetterOffset = 97;
    private const int UnicodeUpperCaseLetterOffset = 65;
    private const int PriorityLowerCaseLetterOffset = 1;
    private const int PriorityUpperCaseLetterOffset = 27;

    private const int EffectiveLowerCaseOffset = UnicodeLowerCaseLetterOffset - PriorityLowerCaseLetterOffset;
    private const int EffectiveUpperCaseOffset = UnicodeUpperCaseLetterOffset - PriorityUpperCaseLetterOffset;

    public static int GetItemScore(char item) =>
        item - (char.IsLower(item) ? EffectiveLowerCaseOffset : EffectiveUpperCaseOffset);
}