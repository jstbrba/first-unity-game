using Game;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private IContext _context;

    [Header("Turret Settings")]
    // [SerializeField] private float damage = 1f;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private EnemyDetector enemyDetector;

    [Header("Ammo Settings")]
    [SerializeField] private ProjectileSettings projectileSettings;

    private bool _isBroken = false;

    public float FireRate => fireRate;

    //private Health health;
    private StateMachine stateMachine;

    private void Awake()
    {
        //health = GetComponent<GeneratorMVC>().GeneratorHealth;

        stateMachine = new StateMachine();

        InactiveTurretState inactiveState = new InactiveTurretState(this);
        ActiveTurretState activeState = new ActiveTurretState(this);
        BrokenTurretState brokenState = new BrokenTurretState(this);

        At(inactiveState, activeState, new FuncPredicate(() => enemyDetector.InRange));
        At(activeState, inactiveState, new FuncPredicate(() => !enemyDetector.InRange));
        At(brokenState, inactiveState, new FuncPredicate(() => !_isBroken));

        Any(brokenState, new FuncPredicate(() => _isBroken));

        stateMachine.SetState(inactiveState);
    }
    public void Initialise(IContext context)
    {
        _context = context;

        _context.CommandBus.AddListener<DeathCommand>(OnDeath);
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
    public void OnDeath(DeathCommand command)
    {
        _isBroken = true;
    }
}
