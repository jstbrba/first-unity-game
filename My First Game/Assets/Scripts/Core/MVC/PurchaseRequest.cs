public class PurchaseRequest : Request
{
    public PurchaseType PurchaseType { get; }
    public int RequiredAmount { get; }
    public PurchaseRequest(PurchaseType purchaseType ,int requiredAmount, IContext returnContext) : base(returnContext)
    {
        PurchaseType = purchaseType;
        RequiredAmount = requiredAmount;
    }
}
public enum PurchaseType
{
    SpeedUpgrade,
    AttackUpgrade,
    DoorUpgrade,
    GenUpgrade
}
