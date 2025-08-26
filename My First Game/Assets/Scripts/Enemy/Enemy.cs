using UnityEngine;
using Utilities;
namespace Game
{
    public class Enemy : MonoBehaviour
    {

        [SerializeField] private float movementSpeed;
        [SerializeField] private int moneyOnDeath = 20;

        private Rigidbody2D body;
        private Animator anim;
        private PlayerDetector playerDetector;
        private Vector3 originalScale;

        private StateMachine stateMachine;

        private Health health;
        private float lowHealthThreshold = 2;

        private EnemyAttack enemyAttack;

        public int MoneyOnDeath => moneyOnDeath;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            health = GetComponent<Health>();
            enemyAttack = GetComponent<EnemyAttack>();
            playerDetector = GetComponent<PlayerDetector>();
            originalScale = transform.localScale;

            ConfigureStateMachine();
        }
        private void Update()
        {
            stateMachine.Update();

            // FLIP ENEMY
            if (playerDetector.Player.position.x < transform.position.x)
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            else
                transform.localScale = originalScale;
        }
        private void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }
        public void Chase() => body.linearVelocity = new Vector2(movementSpeed * playerDetector.Direction(), body.linearVelocity.y);
        public void Retreat() => body.linearVelocity = new Vector2(-movementSpeed * playerDetector.Direction(), body.linearVelocity.y);

        private bool IsLowHealth => (health.currentHealth <= lowHealthThreshold);
        private void ConfigureStateMachine()
        {
            stateMachine = new StateMachine();

            var idleState = new EnemyIdleState(this, anim);
            var chaseState = new EnemyChaseState(this, anim);
            var retreatState = new EnemyRetreatState(this, anim);
            var attackState = new EnemyAttackState(this, anim, enemyAttack);

            At(idleState, chaseState, new FuncPredicate(() => playerDetector.InRange() && !playerDetector.InAttackRange()));
            At(chaseState, idleState, new FuncPredicate(() => !playerDetector.InRange() || playerDetector.InAttackRange()));

            At(retreatState, idleState, new FuncPredicate(() => playerDetector.SafeRange()));
            Any(retreatState, new FuncPredicate(() => (IsLowHealth || playerDetector.CloseRange()) && enemyAttack.IsRunning));
            Any(attackState, new FuncPredicate(() => playerDetector.CanAttack() && !enemyAttack.IsRunning));

            At(attackState, idleState, new FuncPredicate(() => attackState.IsAttackFinished));

            stateMachine.SetState(idleState);
        }

        private void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);
    }
}
