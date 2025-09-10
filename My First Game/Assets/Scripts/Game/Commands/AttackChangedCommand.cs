public class AttackChangedCommand : ICommand 
{
    public int Previous { get; }
    public int Current { get; }

    public AttackChangedCommand(int previous, int current)
    {
        Previous = previous;
        Current = current;
    }
}
