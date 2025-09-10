public class SetAttackCommand : ICommand
{
    public int Attack { get; }
    public SetAttackCommand(int attack)
    {
        Attack = attack;
    }
}
