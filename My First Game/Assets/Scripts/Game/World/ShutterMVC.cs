using UnityEngine;

public class ShutterMVC : MonoBehaviour
{
    [SerializeField] private HealthView _healthView;

    private IContext _shutterContext;
    public Health ShutterHealth;

    private void Start()
    {
        _shutterContext = new BaseContext();

        ShutterHealth = new Health(_shutterContext, _healthView, 20);
        ShutterHealth.Initialise();
    }
}