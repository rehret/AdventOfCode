namespace CodeChallenge.Core.IO;

/// <summary>
/// Groups lines of input, using empty lines as the delimiter
/// </summary>
/// <typeparam name="TChallengeSelection"></typeparam>
/// <typeparam name="TOutput">Type of individual value within a group. Returned type will be IEnumerable&lt;IEnumerable&lt;TOutput&gt;&gt;.</typeparam>
public interface IGroupedInputProvider<in TChallengeSelection, TOutput> : IInputProvider<TChallengeSelection, IEnumerable<IEnumerable<TOutput>>>
{ }