using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Text;

using CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCoreI69Emulator;

var rootCommand = new RootCommand("TomtelCoreI69Emulator");
var filePathArgument = new Argument<FileInfo>("FilePath", description: "Specifies the input file containing TomtelCoreI69 assembly code");
rootCommand.AddArgument(filePathArgument);
rootCommand.SetHandler(async filePath =>
{
    string program;

    var fileStream = filePath.Open(FileMode.Open, FileAccess.Read);
    await using (fileStream.ConfigureAwait(false))
    {
        using var streamReader = new StreamReader(fileStream);
        program = await streamReader.ReadToEndAsync().ConfigureAwait(false);
    }

    var machine = new TomtelCoreI69Emulator();
    await using var machineDisposable = machine.ConfigureAwait(false);

    machine.LoadProgram(TomtelCoreI69Emulator.LoadProgramFromString(program));
    var result = machine.Execute();

    Console.WriteLine(Encoding.UTF8.GetString(result.ToArray()));
}, filePathArgument);

var parser = new CommandLineBuilder(rootCommand).UseDefaults().Build();
return await parser.InvokeAsync(args).ConfigureAwait(false);