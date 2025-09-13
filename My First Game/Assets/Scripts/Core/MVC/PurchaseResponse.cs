public class PurchaseResponse : ICommand
{
    public PurchaseType PurchaseType { get; }
    public bool Success { get; }
    public PurchaseResponse(PurchaseType purchaseType ,bool success)
    {
        PurchaseType = purchaseType;
        Success = success;
    }
}
