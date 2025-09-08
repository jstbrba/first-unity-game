public class MaxHealthChangedCommand : ICommand
{
    public int Previous { get; }
    public int Current { get; }
    public MaxHealthChangedCommand(int previous, int current)
    {
        Previous = previous;
        Current = current;
    }
}
