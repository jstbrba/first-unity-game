using UnityEngine;

public class EnemyStatsView : MonoBehaviour, IView
{
    public IContext Context { get { return _context; } }

    private IContext _context;
    private EnemyStatsModel _model;
    public void Initialise(IContext context)
    {
        _context = context;

        _model = _context.ModelLocator.Get<EnemyStatsModel>();

        _model.Speed.onValueChanged += Model_Speed_OnValueChanged;
        _model.Attack.onValueChanged += Model_Attack_OnValueChanged;
        _model.MoneyOnDeath.onValueChanged += Model_MoneyOnDeath_OnValueChanged;
    }

    public void Model_Speed_OnValueChanged(float previous, float current) { }
    public void Model_Attack_OnValueChanged(int previous, int current) { }
    public void Model_MoneyOnDeath_OnValueChanged(int previous, int current) { }

}
