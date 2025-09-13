public class UpgradeAttackCommand : ICommand
{
    public int Attack { get; }
    public UpgradeAttackCommand(int attack)
    {
        Attack = attack;
    }
}
