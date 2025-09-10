public class ApplyChargeCommand : ICommand
{
    public int Charge { get; }
    public ApplyChargeCommand(int charge)
    {
        Charge = charge;
    }
}
