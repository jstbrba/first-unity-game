using UnityEngine;

public class PlayerMVC : MonoBehaviour
{
    [SerializeField] private HealthView _healthView;

    private IContext _playerContext;
    public Health PlayerHealth;

    private void Start()
    {
        _playerContext = new BaseContext();

        PlayerHealth = new Health(_playerContext, _healthView, 20);
        PlayerHealth.Initialise();
    }
}
