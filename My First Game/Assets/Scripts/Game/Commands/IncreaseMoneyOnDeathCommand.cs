public class IncreaseMoneyOnDeathCommand : ICommand 
{
    public int MoneyOnDeath { get; }
    public IncreaseMoneyOnDeathCommand(int moneyOnDeath)
    {
        MoneyOnDeath = moneyOnDeath;
    }
}
