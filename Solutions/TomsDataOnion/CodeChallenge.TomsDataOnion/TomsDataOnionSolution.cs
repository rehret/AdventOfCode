namespace CodeChallenge.TomsDataOnion;

using System.Diagnostics;
using System.Text;

using CodeChallenge.Core;
using CodeChallenge.Core.IO;
using CodeChallenge.TomsDataOnion.Attributes;
using CodeChallenge.TomsDataOnion.IO;

internal abstract class TomsDataOnionSolution : AbstractSolution<TomsDataOnionSolutionAttribute, TomsDataOnionChallengeSelection>
{
    private readonly IInputProvider<TomsDataOnionChallengeSelection, byte> _inputProvider;
    private readonly ITomsDataOnionOutputWriter _outputWriter;

    protected TomsDataOnionSolution(IInputProvider<TomsDataOnionChallengeSelection, byte> inputProvider, ITomsDataOnionOutputWriter outputWriter)
    {
        _inputProvider = inputProvider;
        _outputWriter = outputWriter;
    }

    public override async Task<string> SolveAsync(Stopwatch? stopwatch = null)
    {
        // Get input
        var input = await _inputProvider.GetInputAsync(GetChallengeSelection()).ConfigureAwait(false);

        // Process the layer
        stopwatch?.Start();
        var stringResult = Encoding.UTF8.GetString(Decode(input).ToArray());
        stopwatch?.Stop();

        // Write output
        var challengeSelection = GetChallengeSelection();
        await _outputWriter.WriteOutput(challengeSelection, stringResult).ConfigureAwait(false);

        return GetOutput(stringResult);
    }

    protected abstract IEnumerable<byte> Decode(IEnumerable<byte> input);

    protected override TomsDataOnionChallengeSelection BuildChallengeSolutionFromAttribute(TomsDataOnionSolutionAttribute attribute)
    {
        return new TomsDataOnionChallengeSelection(attribute.Layer);
    }

    private static string GetOutput(string result)
    {
        var indexOfPayload = result.IndexOf("==[ Payload ]==", StringComparison.Ordinal);
        return indexOfPayload < 0 ? result : result[..indexOfPayload];
    }
}