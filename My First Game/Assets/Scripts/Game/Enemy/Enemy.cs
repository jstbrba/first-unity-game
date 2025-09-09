using UnityEngine;
namespace Game
{
    public class Enemy : Flyweight
    {
        [HideInInspector] new EnemySettings settings => (EnemySettings)base.settings;

        private Rigidbody2D body;
        private Animator anim;
        private PlayerDetector playerDetector;
        private TargetDetector targetDetector;
        private Vector3 originalScale;

        private StateMachine stateMachine;
        private EnemyIdleState idleState;

        private EnemyMVC mvc;
        private float lowHealthThreshold = 2;

        private EnemyAttack enemyAttack;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            mvc = GetComponent<EnemyMVC>();
            enemyAttack = GetComponent<EnemyAttack>();
            playerDetector = GetComponent<PlayerDetector>();
            targetDetector = GetComponent<TargetDetector>();
            originalScale = transform.localScale;

            ConfigureStateMachine();
        }
        private void OnEnable()
        {
            stateMachine.SetState(idleState);
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

        private bool IsLowHealth => (mvc.EnemyHealth.Model.Health.Value <= lowHealthThreshold);
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

            At(retreatState, idleState, new FuncPredicate(() => IsLowHealth && playerDetector.SafeRange() || !IsLowHealth && playerDetector.InAttackRange()));

            Any(retreatState, new FuncPredicate(() => (IsLowHealth && !playerDetector.SafeRange() || playerDetector.CloseRange()) && enemyAttack.IsRunning));
            Any(attackState, new FuncPredicate(() => (playerDetector.CanAttack() || targetDetector.TargetInRange()) && !enemyAttack.IsRunning));


            // stateMachine.SetState(idleState);
        }

        private void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);
        public void HandleDeath()
        {
            settings.moneyChannel.Invoke(settings.moneyOnDeath);
            FlyweightFactory.ReturnToPool(this);
        }
    }
}
