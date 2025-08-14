using UnityEngine;
namespace Game
{
    public class Enemy : MonoBehaviour
    {

        [SerializeField] private float movementSpeed;
        [SerializeField] private Transform player;
        [SerializeField] private float detectionRange;
        [SerializeField] private float safeRange;
        private float direction;

        private Rigidbody2D body;
        private Animator anim;
        private Vector3 originalScale;

        private StateMachine stateMachine;

        private Health health;
        private float lowHealthThreshold = 2;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            health = GetComponent<Health>();
            originalScale = transform.localScale;

            stateMachine = new StateMachine();

            var idleState = new EnemyIdleState(this, anim);
            var chaseState = new EnemyChaseState(this, anim);
            var retreatState = new EnemyRetreatState(this, anim);

            At(idleState, chaseState, new FuncPredicate(() => IsPlayerInRange()));
            At(chaseState, idleState, new FuncPredicate(() => !IsPlayerInRange()));

            At(retreatState, idleState, new FuncPredicate(() => IsSafeRange()));
            Any(retreatState, new FuncPredicate(() => IsLowHealth));

            stateMachine.SetState(idleState);
        }
        private void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);
        private void Update()
        {
            stateMachine.Update();
            // FLIP ENEMY
            if (player.position.x < transform.position.x)
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            else
                transform.localScale = originalScale;

            direction = Mathf.Sign(player.position.x - transform.position.x);

            Debug.Log(IsPlayerInRange() + " " +  IsSafeRange() + " " + IsLowHealth);

            if (Input.GetKeyDown(KeyCode.H)) health.TakeDamage(1);
        }
        private void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }
        public void Chase() => body.linearVelocity = new Vector2(movementSpeed * direction, body.linearVelocity.y);
        public void Retreat() => body.linearVelocity = new Vector2(-movementSpeed * direction, body.linearVelocity.y);

        private bool IsPlayerInRange()
        {
            float distance = Vector3.Distance(transform.position, player.position);
            return distance < detectionRange;
        }
        private bool IsSafeRange()
        {
            float distance = Vector3.Distance(transform.position, player.position);
            return distance > safeRange;
        }
        private bool IsLowHealth => (health.currentHealth <= lowHealthThreshold);
    }
}
