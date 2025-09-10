using UnityEngine;
using System.Collections;

public class Projectile : Flyweight
{
    new ProjectileSettings settings => (ProjectileSettings) base.settings;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private int direction = 1;
    private bool isReleased = false;
    public void SetDirection(int direction) => this.direction = direction;
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        isReleased = false;
        StartCoroutine(despawnAfterDelay(settings.despawnDelay));
    }

    private void Update()
    {
        transform.position += new Vector3(direction * settings.speed * Time.deltaTime, 0, 0);
    }

    private IEnumerator despawnAfterDelay(float delay)
    {
        yield return Helpers.GetWaitForSeconds(delay);
        ReleaseSelf();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<IDamageable>().ApplyDamage((int)settings.damage);
            ReleaseSelf();
        }
    }
    private void ReleaseSelf()
    {
        if (isReleased) return;
        isReleased = true;
        FlyweightFactory.ReturnToPool(this);
    }
}
