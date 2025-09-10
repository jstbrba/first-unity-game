using Game;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private IContext _context;

    private InputReader inputReader;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private Animator anim;

    private float horizontalInput;
    private bool isMoving => horizontalInput != 0;
    private bool jumpPressed;
    private bool crouchPressed;
    private bool isSprinting;
    private bool isAttacking;

    private StateMachine stateMachine;

    private void Awake()
    {
        inputReader = GetComponent<InputReader>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        anim = GetComponent<Animator>();

        ConfigureStateMachine();
    }
    public void Initialise(IContext context)
    {
        _context = context;
    }
    private void Update()
    {
        horizontalInput = inputReader.moveAxis;
        isSprinting = inputReader.sprintPressed && Mathf.Abs(horizontalInput) > 0.01f;
        jumpPressed = inputReader.jumpPressed;
        crouchPressed = inputReader.crouchPressed;

        stateMachine.Update();
    }
    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
    private void ConfigureStateMachine()
    {
        // STATE MACHINE
        stateMachine = new StateMachine();

        // DECLARE STATES
        var idleState = new IdleState(playerMovement, anim);
        var walkingState = new WalkingState(playerMovement, anim);
        var sprintState = new SprintState(playerMovement, anim);
        var crouchState = new CrouchState(playerMovement, anim);
        var jumpState = new JumpState(playerMovement, anim);
        var attackState = new AttackState(playerMovement, anim, playerAttack);

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
    private bool CanIdle() => !isMoving && !crouchPressed && playerMovement.isGrounded();
    private bool CanWalk() => isMoving && playerMovement.isGrounded() && !isSprinting && !crouchPressed;
    private bool CanSprint() => isMoving && playerMovement.isGrounded() && isSprinting && !crouchPressed;
    private bool CanJump() => !playerMovement.isGrounded();
    private bool CanCrouch() => playerMovement.isGrounded() && crouchPressed;
    private bool CanAttack() => playerAttack.IsAttacking && playerMovement.isGrounded();
}
