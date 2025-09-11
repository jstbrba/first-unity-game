using Game;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private IContext _context;
    private PlayerStatsModel _statsModel;

    private float _movementSpeed;
    private float _jumpPower;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider;
    private InputReader _inputReader;
    private float _originalXScale;

    private float _horizontalInput;
    private bool _jumpPressed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _inputReader = GetComponent<InputReader>();
        _originalXScale = transform.localScale.x;
    }
    public void Initialise(IContext context) 
    { 
        _context = context;

        _statsModel = _context.ModelLocator.Get<PlayerStatsModel>();
        _movementSpeed = _statsModel.Speed.Value;
        _jumpPower = _statsModel.JumpPower.Value;

        _statsModel.Speed.onValueChanged += Model_Speed_OnValueChanged;
        _statsModel.JumpPower.onValueChanged += Model_JumpPower_OnValueChanged;
    }
    public void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _jumpPressed = _inputReader.jumpPressed;
    }
    public void FlipPlayer()
    {
        if (Mathf.Abs(_horizontalInput) > 0)
            transform.localScale = new Vector3(Mathf.Sign(_horizontalInput) * _originalXScale, transform.localScale.y, transform.localScale.z);
    }

    public bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0, Vector2.down, 0.1f, _groundLayer);
        return hit.collider != null;
    }

    // FIXME : Player does a super jump if you quickly tap jump, release, then hold it.
    public void HandleJump()
    {
        if (_jumpPressed && isGrounded())
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _rb.linearVelocity.y + _jumpPower);

        if (!_jumpPressed && _rb.linearVelocity.y > 0)
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _rb.linearVelocity.y / 2);
    }
    public void HandleMovement(float speedMultiplier) => _rb.linearVelocity = new Vector2(_horizontalInput * _movementSpeed * speedMultiplier, _rb.linearVelocity.y);

    public void Model_Speed_OnValueChanged(float previous, float current) => _movementSpeed = current;
    public void Model_JumpPower_OnValueChanged(float previous, float current) => _jumpPower = current;
}
