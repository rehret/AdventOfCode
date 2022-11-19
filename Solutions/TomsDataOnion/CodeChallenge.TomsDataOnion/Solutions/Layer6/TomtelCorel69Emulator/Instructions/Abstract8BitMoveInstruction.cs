namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCorel69Emulator.Instructions;

internal abstract class Abstract8BitMoveInstruction : Instruction
{
    protected abstract bool HasImm8Source { get; }

    protected override bool CheckOpCode(byte opCode)
    {
        // opCode must start with 0b01
        return opCode >> 6 == 1 && HasImm8Source == OpCodeHasImm8Source(opCode);
    }

    private static bool OpCodeHasImm8Source(byte opCode)
    {
        return GetIntermediateMoveSource(opCode) == 0;
    }

    protected override void Execute(MachineState state)
    {
        // All three of these need to be grabbed before we increment the program counter
        var imm8 = GetImm8(state);
        var threeBitSrc = GetIntermediateMoveSource(state.OpCode);
        var threeBitDest = GetIntermediateMoveDestination(state.OpCode);

        IncrementProgramCounter(state, (uint)(HasImm8Source ? 2 : 1));

        var sourceValue = HasImm8Source ? imm8 : GetSourceValue(state, threeBitSrc);

        var destinationLocation = GetEightBitRegister(threeBitDest);
        if (destinationLocation == MachineState.Registers.EightBit.Pointer)
        {
            state.Memory[state.ThirtyTwoBitRegisters[MachineState.Registers.ThirtyTwoBit.PTR] + state.EightBitRegisters[MachineState.Registers.EightBit.C]] = sourceValue;
        }
        else
        {
            state.EightBitRegisters[destinationLocation] = sourceValue;
        }
    }

    protected virtual byte GetSourceValue(MachineState state, byte threeBitSrc) => throw new NotImplementedException();
}