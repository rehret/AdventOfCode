namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCorel69Emulator.Instructions;

internal sealed class XorInstruction : AbstractFixedOpCodeInstruction
{
    protected override byte OpCode => 0xC4;

    protected override void Execute(MachineState state)
    {
        IncrementProgramCounter(state, 1);
        state.EightBitRegisters[MachineState.Registers.EightBit.A] ^= state.EightBitRegisters[MachineState.Registers.EightBit.B];
    }
}