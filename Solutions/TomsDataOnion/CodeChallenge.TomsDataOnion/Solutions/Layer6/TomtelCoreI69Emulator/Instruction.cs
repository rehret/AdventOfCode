namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCoreI69Emulator;

using CodeChallenge.TomsDataOnion.Solutions.Layer6.Helpers;
using CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCoreI69Emulator.Instructions;

internal abstract class Instruction
{
    private static readonly Instruction AddInstruction = new AddInstruction();
    // ReSharper disable once IdentifierTypo
    private static readonly Instruction AptrInstruction = new AptrInstruction();
    private static readonly Instruction CmpInstruction = new CmpInstruction();
    private static readonly Instruction HaltInstruction = new HaltInstruction();
    private static readonly Instruction JezInstruction = new JezInstruction();
    private static readonly Instruction JnzInstruction = new JnzInstruction();
    private static readonly Instruction MvInstruction = new MvInstruction();
    private static readonly Instruction MviInstruction = new MviInstruction();
    private static readonly Instruction Mv32Instruction = new Mv32Instruction();
    private static readonly Instruction Mvi32Instruction = new Mvi32Instruction();
    private static readonly Instruction OutInstruction = new OutInstruction();
    private static readonly Instruction SubInstruction = new SubInstruction();
    private static readonly Instruction XorInstruction = new XorInstruction();

    public static void ExecuteNextInstruction(MachineState state)
    {
        FromState(state).Execute(state);
    }

    private static Instruction FromState(MachineState state) =>
        state.OpCode switch
        {
            var opCode when AddInstruction.CheckOpCode(opCode)   => AddInstruction,
            var opCode when AptrInstruction.CheckOpCode(opCode)  => AptrInstruction,
            var opCode when CmpInstruction.CheckOpCode(opCode)   => CmpInstruction,
            var opCode when HaltInstruction.CheckOpCode(opCode)  => HaltInstruction,
            var opCode when JezInstruction.CheckOpCode(opCode)   => JezInstruction,
            var opCode when JnzInstruction.CheckOpCode(opCode)   => JnzInstruction,
            var opCode when MvInstruction.CheckOpCode(opCode)    => MvInstruction,
            var opCode when MviInstruction.CheckOpCode(opCode)   => MviInstruction,
            var opCode when Mv32Instruction.CheckOpCode(opCode)  => Mv32Instruction,
            var opCode when Mvi32Instruction.CheckOpCode(opCode) => Mvi32Instruction,
            var opCode when OutInstruction.CheckOpCode(opCode)   => OutInstruction,
            var opCode when SubInstruction.CheckOpCode(opCode)   => SubInstruction,
            var opCode when XorInstruction.CheckOpCode(opCode)   => XorInstruction,
            _                                                        => throw new ArgumentOutOfRangeException()
        };

    protected abstract bool CheckOpCode(byte opCode);

    protected abstract void Execute(MachineState state);

    protected static void IncrementProgramCounter(MachineState state, uint increment)
    {
        state.ThirtyTwoBitRegisters[MachineState.Registers.ThirtyTwoBit.PC] += increment;
    }

    protected static byte GetImm8(MachineState state)
    {
        var pc = state.ThirtyTwoBitRegisters[MachineState.Registers.ThirtyTwoBit.PC];
        return state.Memory[pc + 1];
    }

    protected static uint GetImm32(MachineState state)
    {
        var pc = state.ThirtyTwoBitRegisters[MachineState.Registers.ThirtyTwoBit.PC];
        return LittleEndianBitConverter.ToUInt32(state.Memory[((int)pc + 1)..((int)pc + 5)].AsSpan());
    }

    protected static byte GetIntermediateMoveSource(byte instr)
    {
        return (byte)(instr & 0b00000111);
    }

    protected static byte GetIntermediateMoveDestination(byte instr)
    {
        return (byte)((instr & 0b00111000) >> 3);
    }

    protected static MachineState.Registers.EightBit GetEightBitRegister(byte threeBitLocation)
    {
        return threeBitLocation switch
        {
            1 => MachineState.Registers.EightBit.A,
            2 => MachineState.Registers.EightBit.B,
            3 => MachineState.Registers.EightBit.C,
            4 => MachineState.Registers.EightBit.D,
            5 => MachineState.Registers.EightBit.E,
            6 => MachineState.Registers.EightBit.F,
            7 => MachineState.Registers.EightBit.Pointer,
            _ => throw new ArgumentException($"Invalid 3-bit location for 8-bit registers: {threeBitLocation}", nameof(threeBitLocation))
        };
    }

    protected static MachineState.Registers.ThirtyTwoBit GetThirtyTwoBitRegister(byte threeBitLocation)
    {
        return threeBitLocation switch
        {
            1 => MachineState.Registers.ThirtyTwoBit.LA,
            2 => MachineState.Registers.ThirtyTwoBit.LB,
            3 => MachineState.Registers.ThirtyTwoBit.LC,
            4 => MachineState.Registers.ThirtyTwoBit.LD,
            5 => MachineState.Registers.ThirtyTwoBit.PTR,
            6 => MachineState.Registers.ThirtyTwoBit.PC,
            _ => throw new ArgumentException($"Invalid 3-bit location for 32-bit registers: {threeBitLocation}", nameof(threeBitLocation))
        };
    }
}
