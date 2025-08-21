using UnityEngine;

namespace Game
{
    public class PlayerDetector : MonoBehaviour 
    {
        private Transform player;
        [SerializeField] private float detectionRange;
        [SerializeField] private float safeRange;
        [SerializeField] private float attackRange; // change later so that attack's class colliders and this range match, probably make a da
        public float Direction() => Mathf.Sign(player.position.x - transform.position.x);

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        public bool CanAttack()
        {
            var distanceToPlayer = player.position - transform.position;
            return distanceToPlayer.magnitude <= attackRange;
        }
        public bool InRange()
        {
            float distance = Vector3.Distance(transform.position, player.position);
            return distance < detectionRange;
        }
        public bool SafeRange()
        {
            float distance = Vector3.Distance(transform.position, player.position);
            return distance > safeRange;
        }

    }
}
