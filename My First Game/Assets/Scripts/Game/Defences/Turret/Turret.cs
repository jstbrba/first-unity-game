using Game;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Turret Settings")]
    // [SerializeField] private float damage = 1f;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private EnemyDetector enemyDetector;

    [Header("Ammo Settings")]
    [SerializeField] private ProjectileSettings projectileSettings;

    public float FireRate => fireRate;

    private Health health;
    private StateMachine stateMachine;

    private void Awake()
    {
        health = GetComponent<GeneratorMVC>().GeneratorHealth;

        stateMachine = new StateMachine();

        InactiveTurretState inactiveState = new InactiveTurretState(this);
        ActiveTurretState activeState = new ActiveTurretState(this);
        BrokenTurretState brokenState = new BrokenTurretState(this);

        At(inactiveState, activeState, new FuncPredicate(() => enemyDetector.InRange));
        At(activeState, inactiveState, new FuncPredicate(() => !enemyDetector.InRange));
        At(brokenState, inactiveState, new FuncPredicate(() => health.Model.Health.Value > 0));

        Any(brokenState, new FuncPredicate(() => health.Model.Health.Value == 0));

        stateMachine.SetState(inactiveState);
    }
    private void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
    private void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

    private void Update()
    {
        stateMachine.Update();
    }

    public void Fire()
    {
        var projectile = FlyweightFactory.Spawn(projectileSettings);
        projectile.GetComponent<Projectile>().SetDirection((int)Mathf.Sign(transform.localScale.x));
        projectile.transform.position = firePoint.position;
    }
    public void OnDeath()
    {
        Destroy(gameObject);
    }
}
