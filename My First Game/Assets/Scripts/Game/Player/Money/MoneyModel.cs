using Utilities;
public class MoneyModel
{
    public Observable<int> Money { get { return _money; } }

    private readonly Observable<int> _money = new Observable<int>();

    public void Initialise()
    {
        Money.Value = 0;
    }
}
