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
    private PlayerAttack playerAttack;
    private InputReader inputReader;
    private Health health;
    private TrapPlacer trapPlacer;
    private float originalXScale;

    private float horizontalInput;
    private bool isMoving => horizontalInput != 0;
    private bool jumpPressed;
    private bool crouchPressed;
    private bool isSprinting;
    private bool isAttacking;

    private StateMachine stateMachine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        inputReader = GetComponent<InputReader>();
        health = GetComponent<Health>();
        trapPlacer = GetComponent<TrapPlacer>();

        originalXScale = transform.localScale.x;

        ConfigureStateMachine();
    }
    private void OnEnable()
    {
        health.OnDeath += HandleDeath;
    }
    private void OnDisable()
    {
        health.OnDeath -= HandleDeath;
    }
    private void Update()
    {
        horizontalInput = inputReader.moveAxis;
        isSprinting = inputReader.sprintPressed && Mathf.Abs(horizontalInput) > 0.01f;
        jumpPressed = inputReader.jumpPressed;
        crouchPressed = inputReader.crouchPressed;

        if (Mathf.Abs(horizontalInput) > 0)
            transform.localScale = new Vector3(Mathf.Sign(horizontalInput) * originalXScale, transform.localScale.y, transform.localScale.z);

        if (inputReader.placePressed) trapPlacer.Place();

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

    // FIXME : Player does a super jump if you quickly tap jump, release, then hold it.
    public void HandleJump()
    {
        if (jumpPressed && isGrounded())
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y + jumpPower);

        if (!jumpPressed && rb.linearVelocity.y > 0)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y / 2);
    }
    public void HandleMovement(float speedMultiplier) => rb.linearVelocity = new Vector2(horizontalInput * movementSpeed * speedMultiplier, rb.linearVelocity.y);
    private void ConfigureStateMachine()
    {
        // STATE MACHINE
        stateMachine = new StateMachine();

        // DECLARE STATES
        var idleState = new IdleState(this, anim);
        var walkingState = new WalkingState(this, anim);
        var sprintState = new SprintState(this, anim);
        var crouchState = new CrouchState(this, anim);
        var jumpState = new JumpState(this, anim);
        var attackState = new AttackState(this, anim, playerAttack);

        // DEFINE TRANSITIONS 
        At(idleState, jumpState, new FuncPredicate(() => CanJump()));
        At(idleState, walkingState, new FuncPredicate(() => CanWalk()));
        At(idleState, crouchState, new FuncPredicate(() => CanCrouch()));
        At(idleState, sprintState, new FuncPredicate(() => CanSprint()));
        At(idleState, attackState, new FuncPredicate(() => CanAttack()));

        At(walkingState, jumpState, new FuncPredicate(() => CanJump()));
        At(walkingState, idleState, new FuncPredicate(() => CanIdle()));
        At(walkingState, sprintState, new FuncPredicate(() => CanSprint()));
        At(walkingState, crouchState, new FuncPredicate(() => CanCrouch()));
        At(walkingState, attackState, new FuncPredicate(() => CanAttack()));

        At(jumpState, idleState, new FuncPredicate(() => CanIdle()));
        At(jumpState, walkingState, new FuncPredicate(() => CanWalk()));
        At(jumpState, sprintState, new FuncPredicate(() => CanSprint()));
        At(jumpState, crouchState, new FuncPredicate(() => CanCrouch()));
        
        At(attackState, idleState, new FuncPredicate(() => attackState.IsAttackFinished));

        At(crouchState, idleState, new FuncPredicate(() => CanIdle()));
        At(crouchState, walkingState, new FuncPredicate(() => CanWalk()));
        At(crouchState, sprintState, new FuncPredicate(() => CanSprint()));
        At(crouchState, jumpState, new FuncPredicate(() => CanJump()));

        At(sprintState, walkingState, new FuncPredicate(() => CanWalk()));
        At(sprintState, jumpState, new FuncPredicate(() => CanJump()));
        At(sprintState, idleState, new FuncPredicate(() => CanIdle()));
        At(sprintState, crouchState, new FuncPredicate(() => CanCrouch()));
        At(sprintState, attackState, new FuncPredicate(() => CanAttack()));

        // SET INTITIAL STATE
        stateMachine.SetState(idleState);
    }
    private void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
    private void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);
    private bool CanIdle() => !isMoving && !crouchPressed && isGrounded();
    private bool CanWalk() => isMoving && isGrounded() && !isSprinting && !crouchPressed;
    private bool CanSprint() => isMoving && isGrounded() && isSprinting && !crouchPressed;
    private bool CanJump() => !isGrounded();
    private bool CanCrouch() => isGrounded() && crouchPressed;
    private bool CanAttack() => playerAttack.IsAttacking && isGrounded();

    private void HandleDeath() => Destroy(gameObject);
}
