public class SetSpeedCommand : ICommand 
{
    public float Speed { get; }
    public SetSpeedCommand(float speed)
    {
        Speed = speed;
    }
}
