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
    [SerializeField] private ProjectileData projectileData;
    private ProjectileFactory projectileFactory;

    public float FireRate => fireRate;

    private Health health;
    private StateMachine stateMachine;

    private void Awake()
    {
        health = GetComponent<Health>();

        projectileFactory = new ProjectileFactory();
        stateMachine = new StateMachine();

        InactiveTurretState inactiveState = new InactiveTurretState(this);
        ActiveTurretState activeState = new ActiveTurretState(this);
        BrokenTurretState brokenState = new BrokenTurretState(this);

        At(inactiveState, activeState, new FuncPredicate(() => enemyDetector.InRange));
        At(activeState, inactiveState, new FuncPredicate(() => !enemyDetector.InRange));
        At(brokenState, inactiveState, new FuncPredicate(() => health.CurrentHealth > 0));

        Any(brokenState, new FuncPredicate(() => health.CurrentHealth == 0));

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
        int direction = (int) Mathf.Sign(transform.localScale.x);
        projectileFactory.CreateProjectile(projectileData, firePoint, direction);
    }
}
