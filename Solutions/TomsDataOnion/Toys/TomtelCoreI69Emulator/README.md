# Tomtel Core i69 Emulator

## Building
```powershell
dotnet publish -r <architechture> -c Release
# where <architecture> is the system to build for, such as win-x64
```

## Running
```powershell
# From within .\bin\Release\<dotnet-version>\<architecture>\publish
.\TomtelCoreI69Emulator.exe <path to input file>
```