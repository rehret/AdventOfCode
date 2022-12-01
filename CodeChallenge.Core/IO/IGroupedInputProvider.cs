namespace CodeChallenge.Core.IO;

public interface IGroupedInputProvider<in TChallengeSelection, TOut> : IInputProvider<TChallengeSelection, IEnumerable<IEnumerable<TOut>>>
{ }