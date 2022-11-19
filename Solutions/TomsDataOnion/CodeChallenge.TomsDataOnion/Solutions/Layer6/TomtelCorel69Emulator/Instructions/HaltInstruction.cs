namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCorel69Emulator.Instructions;

internal sealed class HaltInstruction : AbstractFixedOpCodeInstruction
{
    protected override byte OpCode => 0x01;

    protected override void Execute(MachineState state)
    {
        IncrementProgramCounter(state, 1);
        state.IsExecuting = false;
    }
}