public class JumpPowerChangedCommand : ICommand
{
    public float Previous { get; }
    public float Current { get; }
    public JumpPowerChangedCommand(float previous, float current)
    {
        Previous = previous;
        Current = current;
    }
}
