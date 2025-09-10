public class MaxPowerChangedCommand : ICommand
{
    public int Previous { get; }
    public int Current { get; }

    public MaxPowerChangedCommand(int previous, int current)
    {
        Previous = previous;
        Current = current;
    }
}