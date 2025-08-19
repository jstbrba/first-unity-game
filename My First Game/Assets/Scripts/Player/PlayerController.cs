using Game;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private PlayerAttack playerAttack;
    private InputReader inputReader;

    [SerializeField] private Transform enemy;

    private float horizontalInput;
    private bool jumpPressed;
    private bool crouchPressed;
    private bool isSprinting;
    public float moveDir;
    private bool grounded;

    private StateMachine stateMachine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        inputReader = GetComponent<InputReader>();

        // STATE MACHINE
        stateMachine = new StateMachine();

        // DECLARE STATES
        var idleState = new IdleState(this, anim);
        var walkingState = new WalkingState(this, anim);
        var sprintState = new SprintState(this, anim);
        var crouchState = new CrouchState(this, anim);
        var jumpState = new JumpState(this, anim);
        var attackState = new AttackState(this, anim, playerAttack);

        // DEFINE TRANSITIONS ----------------------------------MAKE SIMPLER LATER COS THIS IS LONGGGGGGGGGGGGG------------
        At(idleState, walkingState, new FuncPredicate(() => moveDir != 0 && isGrounded()));
        At(idleState, jumpState, new FuncPredicate(() => moveDir == 0 && !isGrounded()));

        At(walkingState, jumpState, new FuncPredicate(() => moveDir != 0 && !isGrounded()));
        At(walkingState, idleState, new FuncPredicate(() => moveDir == 0 && isGrounded() && !isSprinting && !crouchPressed));

        At(jumpState, idleState, new FuncPredicate(() => moveDir == 0 && isGrounded()));
        At(jumpState, walkingState, new FuncPredicate(() => moveDir != 0 && isGrounded()));

        At(idleState, attackState, new FuncPredicate(() => Input.GetKeyDown(KeyCode.E) && isGrounded()));
        At(attackState, idleState, new FuncPredicate(() => attackState.IsAttackFinished));

        At(idleState, crouchState, new FuncPredicate(() => moveDir == 0 && isGrounded() && crouchPressed));
        At(crouchState, idleState, new FuncPredicate(() => moveDir == 0 && isGrounded() && !crouchPressed));

        At(walkingState, sprintState, new FuncPredicate(() => moveDir != 0 && isGrounded() && isSprinting));
        At(sprintState, walkingState, new FuncPredicate(() => moveDir != 0 && isGrounded() && !isSprinting));
        At(sprintState, jumpState, new FuncPredicate(() => moveDir != 0 && !isGrounded() && isSprinting));

        //Any(idleState, new FuncPredicate(() => moveDir == 0 && isGrounded() && !crouchPressed && !isSprinting && !attackState.IsAttackFinished));
        //Any(attackState, new FuncPredicate(() => isGrounded() && Input.GetKeyDown(KeyCode.E)));

        // SET INTITIAL STATE
        stateMachine.SetState(idleState);
    }

    private void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
    private void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);
    private void Update()
    {
        horizontalInput = inputReader.moveAxis;
        isSprinting = inputReader.sprintPressed && Mathf.Abs(horizontalInput) > 0.01f;
        jumpPressed = inputReader.jumpPressed;
        crouchPressed = inputReader.crouchPressed;

        // FLIP CHARACTER
        if (Mathf.Abs(horizontalInput) > 0)
        {
            moveDir = Mathf.Sign(horizontalInput);
            transform.localScale = new Vector3(moveDir * 1.5f, 1.5f, 1.5f);
        }
        else moveDir = 0;

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
    public void HandleMovement(float speedMultiplier)
    {
        // MOVEMENT
        rb.linearVelocity = new Vector2(horizontalInput * movementSpeed * speedMultiplier, rb.linearVelocity.y);

    }
}
