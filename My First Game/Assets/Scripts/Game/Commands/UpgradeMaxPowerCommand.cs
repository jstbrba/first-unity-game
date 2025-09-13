public class UpgradeMaxPowerCommand : ICommand
{
    public int Power { get; }
    public UpgradeMaxPowerCommand(int power)
    {
        Power = power;
    }
}
