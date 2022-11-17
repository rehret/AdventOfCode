# Code Challenges
This repo contains solutions to a variety of code challenges.

## Running
bash:
```bash
./run.sh <challenge selector>
```

powershell / cmd.exe:
```powershell
./run <challenge selector>
```

### Current Challenge Selectors

Run the above commands without any arguments to see the support challenges. The first segment of the challenge selector is case-insensitive.

## Adding a New Challenge
### The easy way
1. Install the project template
    ```bash
    dotnet new install ./templates/CodeChallenge.Template.Solution/
    ```
   - The template can be reinstalled to pick up changes by running
       ```bash
       dotnet new install --force ./templates/CodeChallenge.Template.Solution/
       ```
2. Create the template
    ```bash
    # From inside the target C# project folder (such as ./Solutions/AdventOfCode/AdventOfCode2021/)
    dotnet new codechallenge.solution --shortName <Project Shortname>
   
    # From inside the Solution Folder (such as ./Solutions/AdventOfCode/)
    dotnet new codechallenge.solution -n <Full Project Name> --shortName <Project Shortname>
   
    # From anywhere
    dotnet new codechallenge.solution -n <Full Project Name> --shortName <Project Shortname> -o <Path to C# project folder>
    ```
   - The project short name is used to name the classes in the template
     - For example, if `--shortName DemoChallenge` was passed, one class name might be `AbstractDemoChallengeSolution`
3. Add the project to the solution
4. Implement the top-level types as needed

### The hard way
1. Create the project at the path `Solutions/<Challenge Name>/<Project Folder>/<Project>.csproj`
2. Extend `IInputReader<TChallengeSelection>` as needed
    - This type is used for getting input from files
    - There is an `AbstractInputReader<TChallengeSelection>` which does typical file processing (trim, split on `\n`, etc) and extending classes need only implement a method for getting the file path
    - Input files should be places in `Resources/<Challenge Name>/` with the folder structure within left up to the solution to organize as it makes sense
    - Register any implementations of `IInputReader<TChallengeSelection>` in Autofac
        - The abstract Autofac module `InputReaderAutoRegisteringModule` can be extended to automatically register implementations in the challenge space's assembly automatically.
3. Extend `IInputProvider<TChallengeSelection, TOutput>` as needed
    - This type is used for getting input (typically via an instance of `IInputProvider<TChallengeSelection>`, but not always) and modifying it to prepare it for the individual solution implementations. This can be as simple as parsing each line as an `int`, but anything can be returned.
    - General-use implementations should be put in `CodeChallenge.InputProviders` for re-use across challenges.
    - There is an `AbstractInputProvider<TChallengeSelection>` which can be used in most cases. It takes an instance of `IInputReader<TChallengeSelection, TOutput>` via the constructor for reading from a file. Classes extending this provide an implementation for `protected abstract TOutput ProcessLine(string line)` to change one line of input into the desired output type.
        - If the input should not be processed line-by-line, this abstract base class should not be used.
    - Register any implementations of `IInputProvider<TChallengeSelection, TOutput>` in Autofac
        - The abstract Autofac module `InputProviderAutoRegisteringModule` can be extended to automatically register implementations in the challenge space's assembly automatically.
4. Extend `ChallengeSelection` as a way to indicate a particular problem & solution within the challenge space.
    - It is recommended to override `ToString()` as this is used when a requested solution isn't found.
5. Extend `AbstractChallengeArgumentParser` to provide functionality for parsing command-line arguments into the `ChallengeSelection` created in step 4.
    - Extending this automatically adds support for the challenge type to `CodeChallenge.Runner`
    - The implementation defines the argument parsing and usage message
6. Extend `SolutionAttribute` for flagging the solution classes
    - Take any indicators (such as year, day, and puzzle in the case of Advent of Code) via the constructor and set them in public properties.
    - The implementation of the abstract method `ToPuzzleSelection()` should return an instance of the type created in step 4.
7. It is recommended to create an abstract `Solution` base class for all solutions within a challenge space.
    - At a minimum, each solution must implement `ISolution`, but if each problem must be executed in a different way, then the abstract base is not needed.
    - There is an `AbstractSolution<TSolutionAttribute, TChallengeSelection>` which can be extended to provide some helper methods in the solution implementation.
        - In particular, it provides a method for getting the current `ChallengeSelection` via reflection of the `SolutionAttribute`.
        - This is most useful when creating another abstract base Solution type for an entire challenge space. Stand-alone Solution implementations may not need this functionality.
8. Create Solution implementations
    - There are only two requirements here:
        1. Must implement `ISolution`
        2. Must annotate the class with an attribute derived from `SolutionAttribute`
9. Register Solutions in Autofac
    - The abstract Autofac module `SolutionAutoRegisteringModule` can be extended to automatically register implementations in the challenge space's assembly automatically.
10. Add input files to `Resources/<Challenge Name>/`, with the nested folder structure left up to the solution to organize as it makes sense.
11. Add a reference to the new csproj from `CodeChallenge.Runner` to ensure the new DLL is copied to the output folder.
