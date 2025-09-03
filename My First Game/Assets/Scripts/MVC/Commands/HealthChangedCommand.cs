public class HealthChangedCommand : ICommand
{
    public int Previous { get; }
    public int Current { get; }

    public HealthChangedCommand(int previous, int current)
    {
        Previous = previous;
        Current = current;
    }
}
