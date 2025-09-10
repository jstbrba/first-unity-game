public class PowerChangedCommand : ICommand
{
    public int Previous { get; }
    public int Current { get; }

    public PowerChangedCommand(int previous, int current)
    {
        Previous = previous;
        Current = current;
    }
}
