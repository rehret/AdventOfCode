namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCorel69Emulator.Instructions;

internal sealed class JezInstruction : AbstractFixedOpCodeInstruction
{
    protected override byte OpCode => 0x21;

    protected override void Execute(MachineState state)
    {
        var imm32 = GetImm32(state);
        IncrementProgramCounter(state, 5);
        if (state.EightBitRegisters[MachineState.Registers.EightBit.F] == 0x00)
        {
            state.ThirtyTwoBitRegisters[MachineState.Registers.ThirtyTwoBit.PC] = imm32;
        }
    }
}