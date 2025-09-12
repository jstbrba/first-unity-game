using UnityEngine;

public class HealthView : MonoBehaviour, IView
{
    protected IContext Context { get { return _context; } }
    protected HealthModel _model;
    protected int _maxHealth;
    protected int _currentHealth;

    private IContext _context;
    public virtual void Initialise(IContext context)
    {
        _context = context;

        _model = _context.ModelLocator.Get<HealthModel>();

        InitialiseValues();

        _model.MaxHealth.onValueChanged += Model_MaxHealth_OnValueChanged;
        _model.CurrentHealth.onValueChanged += Model_CurrentHealth_OnValueChanged;
    }
    protected virtual void Model_MaxHealth_OnValueChanged(int previous, int current) => _maxHealth = current;
    protected virtual void Model_CurrentHealth_OnValueChanged(int previous, int current)
    {
        _currentHealth = current;
        Debug.Log(gameObject.name + " health is now at " +  _currentHealth);
    }

    protected virtual void InitialiseValues()
    {
        _maxHealth = _model.MaxHealth.Value;
        _currentHealth = _model.CurrentHealth.Value;
    }
}
