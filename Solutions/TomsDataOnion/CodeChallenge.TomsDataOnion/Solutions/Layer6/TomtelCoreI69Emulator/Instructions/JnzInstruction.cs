namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCoreI69Emulator.Instructions;

internal sealed class JnzInstruction : AbstractFixedOpCodeInstruction
{
    protected override byte OpCode => 0x22;

    protected override void Execute(MachineState state)
    {
        var imm32 = GetImm32(state);
        IncrementProgramCounter(state, 5);
        if (state.EightBitRegisters[MachineState.Registers.EightBit.F] != 0x00)
        {
            state.ThirtyTwoBitRegisters[MachineState.Registers.ThirtyTwoBit.PC] = imm32;
        }
    }
}