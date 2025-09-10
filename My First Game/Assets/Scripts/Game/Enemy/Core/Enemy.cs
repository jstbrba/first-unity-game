using UnityEngine;
namespace Game
{
    public class Enemy : Flyweight
    {
        private IContext _context;
        [HideInInspector] new EnemySettings settings => (EnemySettings)base.settings;

        private Rigidbody2D body;
        private Animator anim;
        private PlayerDetector playerDetector;
        private TargetDetector targetDetector;
        private Vector3 originalScale;

        private StateMachine stateMachine;
        private EnemyIdleState idleState;

        private float lowHealthThreshold = 2;
        private bool _isLowHealth = false;

        private EnemyAttack enemyAttack;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            enemyAttack = GetComponent<EnemyAttack>();
            playerDetector = GetComponent<PlayerDetector>();
            targetDetector = GetComponent<TargetDetector>();
            originalScale = transform.localScale;

            ConfigureStateMachine();
        }
        public void Initialise(IContext context)
        {
            _context = context;

            _context.CommandBus.AddListener<HealthChangedCommand>(HealthCheck);
            _context.CommandBus.AddListener<DeathCommand>(HandleDeath);

            _context.CommandBus.Dispatch(new SetSpeedCommand(settings.speed));
            _context.CommandBus.Dispatch(new SetMoneyOnDeathCommand(settings.moneyOnDeath));
        }
        private void OnEnable()
        {
            stateMachine.SetState(idleState);

            _context?.CommandBus.Dispatch(new RespawnCommand());
        }
        private void OnDisable()
        {
            _context?.CommandBus.RemoveListener<HealthChangedCommand>(HealthCheck);
            _context?.CommandBus.RemoveListener<DeathCommand>(HandleDeath);
        }
        private void Update()
        {
            stateMachine.Update();
        }
        private void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }
        public void Chase() => body.linearVelocity = new Vector2(settings.speed * playerDetector.Direction(), body.linearVelocity.y);
        public void Retreat() => body.linearVelocity = new Vector2(-settings.speed * playerDetector.Direction(), body.linearVelocity.y);
        public void FacePlayer()
        {
            if (playerDetector.PlayerActive && playerDetector.Player.position.x < transform.position.x)
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            else
                transform.localScale = originalScale;
        }
        private void ConfigureStateMachine()
        {
            stateMachine = new StateMachine();

            idleState = new EnemyIdleState(this, anim);
            var chaseState = new EnemyChaseState(this, anim);
            var retreatState = new EnemyRetreatState(this, anim);
            var attackState = new EnemyAttackState(this, anim, enemyAttack);

            At(idleState, chaseState, new FuncPredicate(() => playerDetector.InRange() && !playerDetector.InAttackRange() && !targetDetector.TargetInRange()));

            At(chaseState, idleState, new FuncPredicate(() => !playerDetector.InRange() || playerDetector.InAttackRange()));

            At(attackState, idleState, new FuncPredicate(() => attackState.IsAttackFinished));

            At(retreatState, idleState, new FuncPredicate(() => _isLowHealth && playerDetector.SafeRange() || !_isLowHealth && playerDetector.InAttackRange()));

            Any(retreatState, new FuncPredicate(() => (_isLowHealth && !playerDetector.SafeRange() || playerDetector.CloseRange()) && enemyAttack.IsRunning));
            Any(attackState, new FuncPredicate(() => (playerDetector.CanAttack() || targetDetector.TargetInRange()) && !enemyAttack.IsRunning));


            // stateMachine.SetState(idleState);
        }

        private void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);
        public void HandleDeath(DeathCommand command)
        {
            settings.moneyChannel.Invoke(settings.moneyOnDeath);
            FlyweightFactory.ReturnToPool(this);
        }
        public void HealthCheck(HealthChangedCommand command)
        {
            _isLowHealth = command.Current < lowHealthThreshold ? true : false;
        }
    }
}
