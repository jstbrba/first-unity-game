using UnityEngine;
using System.Collections;

public class Projectile : Flyweight
{
    new ProjectileSettings settings => (ProjectileSettings) base.settings;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        StartCoroutine(despawnAfterDelay(settings.despawnDelay));
    }

    private void Update()
    {
        transform.Translate(transform.right * settings.speed * Time.deltaTime);
    }

    private IEnumerator despawnAfterDelay(float delay)
    {
        yield return Helpers.GetWaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(settings.damage);
            FlyweightFactory.ReturnToPool(this);
        }
    }
}
