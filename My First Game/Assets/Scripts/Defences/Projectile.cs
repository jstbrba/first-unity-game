using UnityEngine;
using Utilities;

public class Projectile : MonoBehaviour 
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifetime = 10f;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private int direction;

    CountdownTimer despawnTimer;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        direction = (int)Mathf.Sign(transform.localScale.x);

        despawnTimer = new CountdownTimer(lifetime);
        despawnTimer.Start();
    }

    private void Update()
    {
        despawnTimer.Tick(Time.deltaTime);
        if (despawnTimer.IsFinished) Destroy(gameObject);
        transform.Translate(direction * speed * Time.deltaTime, 0f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
            // TODO : Use flyweight pattern for projectiles
        }
    }
}
