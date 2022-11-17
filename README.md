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

| Challenge        | Challenge Selector                   | Aliases (Can be used instead of the first segment) |
|------------------|--------------------------------------|----------------------------------------------------|
| Advent Of Code   | `AdventOfCode/<Year>/<Day>/<Puzzle>` | `AdventOfCode`, `Advent`                           |
| Tom's Data Onion | `TomsDataOnion/<Layer>`              | `TomsDataOnion`, `Toms`, `DataOnion`               |

The first segment of the challenge selector is case-insensitive.

## Adding a New Challenge
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
   - It is recommended to implement `public static bool TryParse(string input, out ChallengeSelection challengeSelection)` on this type to make it easier to determine the target solution from CLI input.
5. Extend `SolutionAttribute` for flagging the solution classes
    - Take any indicators (such as year, day, and puzzle in the case of Advent of Code) via the constructor and set them in public properties.
    - The implementation of the abstract method `ToPuzzleSelection()` should return an instance of the type created in step 4.
6. It is recommended to create an abstract `Solution` base class for all solutions within a challenge space.
   - At a minimum, each solution must implement `ISolution`, but if each problem must be executed in a different way, then the abstract base is not needed.
   - There is an `AbstractSolution<TSolutionAttribute, TChallengeSelection>` which can be extended to provide some helper methods in the solution implementation.
       - In particular, it provides a method for getting the current `ChallengeSelection` via reflection of the `SolutionAttribute`.
       - This is most useful when creating another abstract base Solution type for an entire challenge space. Stand-alone Solution implementations may not need this functionality.
7. Create Solution implementations
   - There are only two requirements here:
       1. Must implement `ISolution`
       2. Must annotate the class with an attribute derived from `SolutionAttribute`
8. Register Solutions in Autofac
   - The abstract Autofac module `SolutionAutoRegisteringModule` can be extended to automatically register implementations in the challenge space's assembly automatically.
9. Add input files to `Resources/<Challenge Name>/`, with the nested folder structure left up to the solution to organize as it makes sense.
10. Update `CodeChallenge.Runner.ChallengeSelectionParser` to know how to find the new challenge
    - `TryParsePuzzleType()` takes a string and returns a `ChallengeType` enum
    - `TryParse()` switches on the returned `ChallengeType` enum