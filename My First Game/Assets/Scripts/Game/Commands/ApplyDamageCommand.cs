public class ApplyDamageCommand : ICommand
{
    public int Damage { get; }
    public ApplyDamageCommand(int damage)
    {
        Damage = damage;
    }
}
