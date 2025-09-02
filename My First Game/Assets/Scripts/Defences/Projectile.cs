using UnityEngine;
using Utilities;

public class Projectile : MonoBehaviour
{
    public float Damage { get; private set; } = 1f;
    public float Speed { get; private set; } = 5f;
    public float Lifetime { get; private set; } = 10f;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private int direction;

    CountdownTimer despawnTimer;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        direction = (int)Mathf.Sign(transform.localScale.x);

        despawnTimer = new CountdownTimer(Lifetime);
        despawnTimer.Start();
    }

    private void Update()
    {
        despawnTimer.Tick(Time.deltaTime);
        if (despawnTimer.IsFinished) Destroy(gameObject);
        transform.Translate(direction * Speed * Time.deltaTime, 0f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(Damage);
            Destroy(gameObject);
            // TODO : Use flyweight pattern for projectiles
        }
    }

    public class Builder
    {
        private GameObject projectilePrefab;
        private float damage;
        private float speed;
        private float lifetime;
        private Transform firePoint;

        public Builder SetBasePrefab(GameObject projectilePrefab)
        {
            this.projectilePrefab = projectilePrefab;
            return this;
        }
        public Builder SetDamage(float damage)
        {
            this.damage = damage;
            return this;
        }
        public Builder SetSpeed(float speed)
        {
            this.speed = speed;
            return this;
        }
        public Builder SetLifetime(float lifetime)
        {
            this.lifetime = lifetime;
            return this;
        }
        public Builder SetFirePoint(Transform firePoint)
        {
            this.firePoint = firePoint;
            return this;
        }
        public GameObject Build()
        {
            GameObject projectile = Instantiate(projectilePrefab);

            Projectile projectileComponent = projectile.GetComponent<Projectile>();
            projectileComponent.Damage = damage;
            projectileComponent.Speed = speed;
            projectileComponent.Lifetime = lifetime;
            projectile.transform.position = firePoint.position;
            return projectile;
        }
    }
}
