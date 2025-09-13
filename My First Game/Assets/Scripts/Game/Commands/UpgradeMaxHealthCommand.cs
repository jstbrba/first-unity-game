public class UpgradeMaxHealthCommand : ICommand
{
    public int Health { get; }
    public UpgradeMaxHealthCommand(int health)
    {
        Health = health;
    }
}
