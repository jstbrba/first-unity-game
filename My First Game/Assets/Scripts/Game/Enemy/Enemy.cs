using UnityEngine;
namespace Game
{
    public class Enemy : Flyweight
    {
        new EnemySettings settings => (EnemySettings)base.settings;

        private Rigidbody2D body;
        private Animator anim;
        private PlayerDetector playerDetector;
        private Vector3 originalScale;

        private StateMachine stateMachine;
        private EnemyIdleState idleState;

        private Health health;
        private float lowHealthThreshold = 2;

        private EnemyAttack enemyAttack;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            health = GetComponent<Health>();
            health.SetMaxHealth(settings.maxHealth);
            enemyAttack = GetComponent<EnemyAttack>();
            playerDetector = GetComponent<PlayerDetector>();
            originalScale = transform.localScale;

            ConfigureStateMachine();
        }
        private void OnEnable()
        {
            stateMachine.SetState(idleState);
            health.OnDeath += HandleDeath;
        }
        private void OnDisable()
        {
            health.OnDeath -= HandleDeath;
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
            if (playerDetector.Player.position.x < transform.position.x)
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            else
                transform.localScale = originalScale;
        }

        private bool IsLowHealth => (health.CurrentHealth <= lowHealthThreshold);
        private void ConfigureStateMachine()
        {
            stateMachine = new StateMachine();

            idleState = new EnemyIdleState(this, anim);
            var chaseState = new EnemyChaseState(this, anim);
            var retreatState = new EnemyRetreatState(this, anim);
            var attackState = new EnemyAttackState(this, anim, enemyAttack);

            At(idleState, chaseState, new FuncPredicate(() => playerDetector.InRange() && !playerDetector.InAttackRange()));

            At(chaseState, idleState, new FuncPredicate(() => !playerDetector.InRange() || playerDetector.InAttackRange()));

            At(attackState, idleState, new FuncPredicate(() => attackState.IsAttackFinished));

            At(retreatState, idleState, new FuncPredicate(() => IsLowHealth && playerDetector.SafeRange() || !IsLowHealth && playerDetector.InAttackRange()));

            Any(retreatState, new FuncPredicate(() => (IsLowHealth && !playerDetector.SafeRange() || playerDetector.CloseRange()) && enemyAttack.IsRunning));
            Any(attackState, new FuncPredicate(() => playerDetector.CanAttack() && !enemyAttack.IsRunning));


            // stateMachine.SetState(idleState);
        }

        private void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);
        private void HandleDeath()
        {
            settings.moneyChannel.Invoke(settings.moneyOnDeath);
            FlyweightFactory.ReturnToPool(this);
        }
    }
}
