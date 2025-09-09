using UnityEngine;

public class EnemyMVC : MonoBehaviour
{
    [SerializeField] private HealthView _healthView;

    private IContext _enemyContext;

    public Health EnemyHealth;

    private void Start()
    {
        _enemyContext = new BaseContext();

        EnemyHealth = new Health(_enemyContext, _healthView, 20);
        EnemyHealth.Initialise();
    }
}
