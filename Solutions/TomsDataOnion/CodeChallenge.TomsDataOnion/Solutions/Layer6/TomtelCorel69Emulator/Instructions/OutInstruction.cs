namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCorel69Emulator.Instructions;

internal sealed class OutInstruction : AbstractFixedOpCodeInstruction
{
    protected override byte OpCode => 0x02;

    protected override void Execute(MachineState state)
    {
        IncrementProgramCounter(state, 1);
        state.OutputStream.WriteByte(state.EightBitRegisters[MachineState.Registers.EightBit.A]);
    }
}