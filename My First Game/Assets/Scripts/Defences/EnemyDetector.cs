using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    private int enemyCount = 0;
    public bool InRange => enemyCount > 0;
    private BoxCollider2D detectionRange;

    private void Awake()
    {
        detectionRange = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) enemyCount++;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) enemyCount--;
    }
}
