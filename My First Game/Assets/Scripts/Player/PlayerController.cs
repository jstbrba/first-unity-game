using Game;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator anim;

    [SerializeField] private Transform enemy;

    private float horizontalInput;
    private bool jumpPressed;
    public float moveDir;
    private bool grounded;
    private bool facingRight;

    private StateMachine stateMachine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        // STATE MACHINE
        stateMachine = new StateMachine();

        // DECLARE STATES
        var idleState = new IdleState(this, anim);
        var walkingState = new WalkingState(this, anim);
        var walkingBackState = new WalkingBackState(this, anim);
        var jumpState = new JumpState(this, anim);

        // DEFINE TRANSITIONS
        At(idleState, walkingState, new FuncPredicate(() => moveDir > 0 && isGrounded()));
        At(idleState, walkingBackState, new FuncPredicate(() => moveDir < 0 && isGrounded()));
        At(idleState, jumpState, new FuncPredicate(() => moveDir == 0 && !isGrounded()));

        At(walkingState, jumpState, new FuncPredicate(() => moveDir > 0 && !isGrounded()));
        At(walkingState, walkingBackState, new FuncPredicate(() => moveDir < 0 && isGrounded()));
        At(walkingState, idleState, new FuncPredicate(() => moveDir == 0 && isGrounded()));

        At(jumpState, idleState, new FuncPredicate(() => moveDir == 0 && isGrounded()));
        At(jumpState, walkingState, new FuncPredicate(() => moveDir > 0 && isGrounded()));
        At(jumpState, walkingBackState, new FuncPredicate(() => moveDir < 0 && isGrounded()));

        At(walkingBackState, idleState, new FuncPredicate(() => moveDir == 0 && isGrounded()));
        At(walkingBackState, jumpState, new FuncPredicate(() => moveDir < 0 && !isGrounded()));
        At(walkingBackState, walkingState, new FuncPredicate(() => moveDir > 0 && isGrounded()));

        // SET INTITIAL STATE
        stateMachine.SetState(walkingState);
    }

    private void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
    private void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.W)) jumpPressed = true;
        if (Input.GetKeyUp(KeyCode.W)) jumpPressed = false;

        Debug.Log(moveDir);

        // FLIP CHARACTER TO FACE ENEMY
        facingRight = enemy.position.x > transform.position.x;
        transform.localScale = new Vector3(facingRight ? 1.5f : -1.5f, 1.5f, 1);

        stateMachine.Update();

    }
    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    private bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }

    public void HandleJump()
    {
        if (jumpPressed && isGrounded())
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y + jumpPower);

        if (!jumpPressed && rb.linearVelocity.y > 0)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y / 2);
    }
    public void HandleMovement()
    {
        // MOVEMENT
        rb.linearVelocity = new Vector2(horizontalInput * movementSpeed, rb.linearVelocity.y);

        // WALK ANIMATION
        if (Mathf.Abs(horizontalInput) > 0.01f)
        {
            Vector2 toEnemy = (enemy.position - transform.position).normalized;
            Vector2 moveDirVector = new Vector2(horizontalInput, 0).normalized;

            float dot = Vector2.Dot(toEnemy, moveDirVector);

            moveDir = dot > 0.1f ? 1f : -1f;
        }
        else
            moveDir = 0;
    }
}
