using UnityEngine;

namespace Game
{
    public class PlayerDetector : MonoBehaviour
    {
        public Transform Player { get; private set; }
        [SerializeField] private float detectionRange;
        [SerializeField] private float safeRange;
        [SerializeField] private float closeRange = 1f;
        [SerializeField] private float attackRange; // change later so that attack's class colliders and this range match, probably make a da
        [SerializeField] private float outOfViewRange = 20f;
        public float Direction() => Mathf.Sign(Player.position.x - transform.position.x);

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
        public bool PlayerActive => Player != null;
        public bool CanAttack()
        {
            if (!PlayerActive) return false;
            var distanceToPlayer = Player.position - transform.position;
            return distanceToPlayer.magnitude <= attackRange;
        }
        public bool InRange()
        {
            if (!PlayerActive) return false;
            float distance = Vector3.Distance(transform.position, Player.position);
            return distance < detectionRange;
        }
        public bool InAttackRange()
        {
            if (!PlayerActive) return false;
            float distance = Vector3.Distance(transform.position, Player.position);
            return distance < attackRange;
        }
        public bool SafeRange()
        {
            if (!PlayerActive) return false;
            float distance = Vector3.Distance(transform.position, Player.position);
            return distance > safeRange;
        }
        public bool CloseRange()
        {
            if (!PlayerActive) return false;
            float distance = Vector3.Distance(transform.position, Player.position);
            return distance < closeRange;
        }
        public bool OutOfView()
        {
            if (!PlayerActive) return false;
            float distance = Vector3.Distance(transform.position, Player.position);
            return distance > outOfViewRange;
        }
    }
}
