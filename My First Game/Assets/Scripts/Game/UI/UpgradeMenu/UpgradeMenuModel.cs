using Utilities;

public class UpgradeMenuModel : BaseModel 
{
    public Observable<bool> IsMenuOpen { get { return _isMenuOpen; } }
    public Observable<int> SpeedUpgradePrice {  get { return _speedUpgradePrice; } }
    public Observable<int> AttackUpgradePrice {  get { return _attackUpgradePrice; } }
    public Observable<int> DoorUpgradePrice {  get { return _doorUpgradePrice; } }
    public Observable<int> GenUpgradePrice {  get { return _genUpgradePrice; } }

    private Observable<bool> _isMenuOpen = new Observable<bool>();
    private Observable<int> _speedUpgradePrice = new Observable<int>();
    private Observable<int> _attackUpgradePrice = new Observable<int>();
    private Observable<int> _doorUpgradePrice = new Observable<int>();
    private Observable<int> _genUpgradePrice = new Observable<int>();

    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        _isMenuOpen.Value = false;

        _speedUpgradePrice.Value = 2;
        _attackUpgradePrice.Value = 4;
        _doorUpgradePrice.Value = 20;
        _genUpgradePrice.Value = 20;
    }
}
