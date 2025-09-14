using UnityEngine;

namespace Game
{
    public class TargetDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private BoxCollider2D boxCollider;

        public bool TargetInRange()
        {
            Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, direction, 0.5f, layerMask);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("EnemyTarget"))
                    return true;
            }
            return false;
        }
    }
}
