namespace CodeChallenge.Core.IO;

public interface IInputFilePathProvider<in T>
    where T : ChallengeSelection
{
    string GetInputFilePath(T challengeSelection);
}