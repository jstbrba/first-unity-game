using UnityEngine;

public class GeneratorMVC : MonoBehaviour
{
    [SerializeField] private HealthView _healthView;

    private IContext _generatorContext;
    public Health GeneratorHealth;

    private void Start()
    {
        _generatorContext = new BaseContext();

        GeneratorHealth = new Health(_generatorContext, _healthView, 20);
        GeneratorHealth.Initialise();
    }
}