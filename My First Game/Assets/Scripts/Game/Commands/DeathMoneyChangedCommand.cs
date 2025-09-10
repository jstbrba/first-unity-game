public class DeathMoneyChangedCommand : ICommand
{
    public int Previous { get; }
    public int Current { get; }

    public DeathMoneyChangedCommand(int previous, int current)
    {
        Previous = previous;
        Current = current;
    }
}
