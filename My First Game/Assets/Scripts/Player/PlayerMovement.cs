using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator anim;

    [SerializeField] private Transform enemy;

    private float horizontalInput;
    private bool grounded;
    private bool facingRight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // FLIP CHARACTER TO FACE ENEMY
        facingRight = enemy.position.x > transform.position.x;
        transform.localScale = new Vector3(facingRight ? 1.5f : -1.5f, 1.5f, 1);
        

        // MOVEMENT
        rb.linearVelocity = new Vector2(horizontalInput * movementSpeed, rb.linearVelocity.y);

        anim.SetBool("grounded", isGrounded() ? true : false);

        // JUMP
        if (Input.GetKeyDown(KeyCode.W) && isGrounded())
        {
            anim.SetTrigger("jump");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y + jumpPower);
            }
        if (Input.GetKeyUp(KeyCode.W) && rb.linearVelocity.y > 0)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y / 2);

        // WALK ANIMATION
        if (Mathf.Abs(horizontalInput) > 0.01f)
        {
            anim.SetBool("walking", true);
            Vector2 toEnemy = (enemy.position - transform.position).normalized;
            Vector2 moveDirVector = new Vector2(horizontalInput, 0).normalized;

            float dot = Vector2.Dot(toEnemy, moveDirVector);

            float moveDir = dot > 0.1f ? 1f : -1f;
            anim.SetFloat("moveDir", moveDir);
        }
        else
            anim.SetBool("walking", false);
    }

    private bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }
}
