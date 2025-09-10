public class SpeedChangedCommand : ICommand 
{
    public float Previous { get; }
    public float Current { get; }
    public SpeedChangedCommand(float previous, float current)
    {
        Previous = previous;
        Current = current;
    }
}
