using UnityEngine;

public class PlayerStatsView : MonoBehaviour, IView
{
    // View will be later
    public IContext Context { get { return _context; } }

    private IContext _context;
    private PlayerStatsModel _model;
    public void Initialise(IContext context)
    {
        _context = context;

        _model = _context.ModelLocator.Get<PlayerStatsModel>();

        _model.Speed.onValueChanged += Model_Speed_OnValueChanged;
        _model.Attack.onValueChanged += Model_Attack_OnValueChanged;
        _model.JumpPower.onValueChanged += Model_JumpPower_OnValueChanged;
    }

    public void Model_Speed_OnValueChanged(float previous, float current) { }
    public void Model_Attack_OnValueChanged(int previous, int current) { }
    public void Model_JumpPower_OnValueChanged(float previous, float current) { }
}
