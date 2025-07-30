using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        // MOVEMENT
        rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * movementSpeed, rb.linearVelocity.y);
    }
}
