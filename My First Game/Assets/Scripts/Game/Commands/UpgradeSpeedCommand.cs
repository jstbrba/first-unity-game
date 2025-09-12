public class UpgradeSpeedCommand : ICommand
{
    public float Speed { get; }
    public UpgradeSpeedCommand(float speed)
    {
        Speed = speed;
    }
}
