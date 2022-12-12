namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCorel69Emulator.Instructions;

internal abstract class Abstract32BitMoveInstruction : Instruction
{
    protected abstract bool HasImm32Source { get; }

    protected override bool CheckOpCode(byte opCode)
    {
        // opCode must start with 0b10
        return opCode >> 6 == 2 && HasImm32Source == OpCodeHasImm32Source(opCode);
    }

    private static bool OpCodeHasImm32Source(byte opCode)
    {
        return GetIntermediateMoveSource(opCode) == 0;
    }

    protected override void Execute(MachineState state)
    {
        // All three of these need to be grabbed before we increment the program counter
        var imm32 = GetImm32(state);
        var threeBitSrc = GetIntermediateMoveSource(state.OpCode);
        var threeBitDest = GetIntermediateMoveDestination(state.OpCode);

        IncrementProgramCounter(state, (uint)(HasImm32Source ? 5 : 1));

        var sourceValue = HasImm32Source ? imm32 : GetSourceValue(state, threeBitSrc);

        var destinationLocation = GetThirtyTwoBitRegister(threeBitDest);
        state.ThirtyTwoBitRegisters[destinationLocation] = sourceValue;
    }

    private static uint GetSourceValue(MachineState state, byte threeBitSrc)
    {
        return state.ThirtyTwoBitRegisters[GetThirtyTwoBitRegister(threeBitSrc)];
    }
}