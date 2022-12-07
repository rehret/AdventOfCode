namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCorel69Emulator;

using System.Text.RegularExpressions;

internal partial class TomtelCoreI69Emulator : IDisposable, IAsyncDisposable
{
    private readonly MachineState _state;
    private readonly MemoryStream _outputStream;

    public TomtelCoreI69Emulator(int memorySize = 0)
    {
        _outputStream = new MemoryStream(memorySize);
        _state = new MachineState(_outputStream);
    }

    public void LoadProgram(byte[] program)
    {
        _outputStream.Seek(0, SeekOrigin.Begin);
        _state.LoadProgram(program);
    }

    public IEnumerable<byte> Execute()
    {
        _state.ExecuteProgram();
        return _outputStream.ToArray();
    }

    public static byte[] LoadProgramFromString(string input)
    {
        var lines = input.Split('\n');
        return lines.SelectMany(line =>
        {
            // Remove comments
            if (line.Contains('#'))
            {
                line = line[..line.IndexOf('#')];
            }

            var operands = WhitespaceRegex().Split(line.Trim());
            return operands.Select(op => Convert.ToByte(op, 16));
        }).ToArray();
    }

    public void Dispose()
    {
        _outputStream.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _outputStream.DisposeAsync().ConfigureAwait(false);
    }

    [GeneratedRegex(@"\s+", RegexOptions.Compiled)]
    private static partial Regex WhitespaceRegex();
}