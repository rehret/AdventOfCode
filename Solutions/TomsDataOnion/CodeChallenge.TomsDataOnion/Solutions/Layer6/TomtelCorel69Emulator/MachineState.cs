namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCorel69Emulator;

using System.Diagnostics.CodeAnalysis;

internal sealed class MachineState
{
    public byte[] Memory { get; private set; }
    public Stream OutputStream { get; }
    public IDictionary<Registers.EightBit, byte> EightBitRegisters { get; private set; }
    public IDictionary<Registers.ThirtyTwoBit, uint> ThirtyTwoBitRegisters { get; private set; }
    public bool IsExecuting { get; set; }
    public bool IsProgramLoaded { get; private set; }

    public MachineState(Stream outputStream)
        : this(Array.Empty<byte>(),
            outputStream,
            DefaultEightBitRegisters,
            DefaultThirtyTwoBitRegisters,
            false,
            false)
    { }

    private MachineState(
        byte[] memory,
        Stream outputStream,
        IDictionary<Registers.EightBit, byte> eightBitRegisters,
        IDictionary<Registers.ThirtyTwoBit, uint> thirtyTwoBitRegisters,
        bool isExecuting,
        bool isProgramLoaded
    )
    {
        Memory = memory;
        OutputStream = outputStream;
        EightBitRegisters = eightBitRegisters;
        ThirtyTwoBitRegisters = thirtyTwoBitRegisters;
        IsExecuting = isExecuting;
        IsProgramLoaded = isProgramLoaded;
    }

    public byte OpCode => Memory[ThirtyTwoBitRegisters[Registers.ThirtyTwoBit.PC]];

    public void LoadProgram(byte[] program)
    {
        var newMemory = new byte[program.Length];
        Array.Copy(program, newMemory, newMemory.Length);

        Memory = newMemory;
        EightBitRegisters = DefaultEightBitRegisters;
        ThirtyTwoBitRegisters = DefaultThirtyTwoBitRegisters;
        IsProgramLoaded = true;
        IsExecuting = false;
    }

    public void ExecuteProgram()
    {
        if (!IsProgramLoaded)
        {
            throw new Exception("No program has been loaded");
        }

        IsExecuting = true;
        while (IsExecuting)
        {
            Instruction.ExecuteNextInstruction(this);
        }
    }

    private static IDictionary<Registers.EightBit, byte> DefaultEightBitRegisters =>
        new Dictionary<Registers.EightBit, byte>
        {
            { Registers.EightBit.A, 0x00 },
            { Registers.EightBit.B, 0x00 },
            { Registers.EightBit.C, 0x00 },
            { Registers.EightBit.D, 0x00 },
            { Registers.EightBit.E, 0x00 },
            { Registers.EightBit.F, 0x00 }
        };

    private static IDictionary<Registers.ThirtyTwoBit, uint> DefaultThirtyTwoBitRegisters =>
        new Dictionary<Registers.ThirtyTwoBit, uint>
        {
            { Registers.ThirtyTwoBit.LA, 0 },
            { Registers.ThirtyTwoBit.LB, 0 },
            { Registers.ThirtyTwoBit.LC, 0 },
            { Registers.ThirtyTwoBit.LD, 0 },
            { Registers.ThirtyTwoBit.PTR, 0 },
            { Registers.ThirtyTwoBit.PC, 0 }
        };

    public static class Registers
    {
        public enum EightBit
        {
            A,
            B,
            C,
            D,
            E,
            F,
            Pointer
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public enum ThirtyTwoBit
        {
            LA,
            LB,
            LC,
            LD,
            PTR,
            PC
        }
    }
}