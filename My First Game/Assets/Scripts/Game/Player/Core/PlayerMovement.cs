using Game;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private IContext _context;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private InputReader inputReader;
    private float originalXScale;

    private float _horizontalInput;
    private bool _jumpPressed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        inputReader = GetComponent<InputReader>();
        originalXScale = transform.localScale.x;
    }
    public void Initialise(IContext context) 
    { 
        _context = context;

        _context.CommandBus.Dispatch(new SetSpeedCommand(movementSpeed));
    }
    public void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _jumpPressed = inputReader.jumpPressed;
    }
    public void FlipPlayer()
    {
        if (Mathf.Abs(_horizontalInput) > 0)
            transform.localScale = new Vector3(Mathf.Sign(_horizontalInput) * originalXScale, transform.localScale.y, transform.localScale.z);
    }

    public bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }

    // FIXME : Player does a super jump if you quickly tap jump, release, then hold it.
    public void HandleJump()
    {
        if (_jumpPressed && isGrounded())
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y + jumpPower);

        if (!_jumpPressed && rb.linearVelocity.y > 0)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y / 2);
    }
    public void HandleMovement(float speedMultiplier) => rb.linearVelocity = new Vector2(_horizontalInput * movementSpeed * speedMultiplier, rb.linearVelocity.y);
}
