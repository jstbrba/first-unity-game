using UnityEngine;

namespace Game
{
    public class TargetDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private Vector2 _detectionBox;
        [SerializeField] private float _distance;

        public bool TargetInRange()
        {
            Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, direction, 0.5f, layerMask);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("EnemyTarget"))
                {
                    Debug.Log("Target IN RANGE!!");
                    return true;
                }
            }
            return false;
        }
        private void OnDrawGizmos()
        {
            Vector2 position = (Vector2)transform.position + new Vector2(transform.localScale.x * _distance, 0);

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(position, _detectionBox);
        }
    }
}
