public class SetMoneyOnDeathCommand : ICommand
{
    public int MoneyOnDeath { get; }
    public SetMoneyOnDeathCommand(int moneyOnDeath)
    {
        MoneyOnDeath = moneyOnDeath;
    }
}
