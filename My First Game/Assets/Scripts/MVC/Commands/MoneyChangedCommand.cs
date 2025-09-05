public class MoneyChangedCommand : ICommand
{
    public int Previous { get; }
    public int Current { get; }
    public MoneyChangedCommand(int previous, int current)
    {
        Previous = previous;
        Current = current;
    }
}
