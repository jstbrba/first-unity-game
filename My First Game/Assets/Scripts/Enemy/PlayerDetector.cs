using UnityEngine;

namespace Game
{
    public class PlayerDetector : MonoBehaviour 
    {
        public Transform Player {  get; private set; }
        [SerializeField] private float detectionRange;
        [SerializeField] private float safeRange;
        [SerializeField] private float attackRange; // change later so that attack's class colliders and this range match, probably make a da
        public float Direction() => Mathf.Sign(Player.position.x - transform.position.x);

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        public bool CanAttack()
        {
            var distanceToPlayer = Player.position - transform.position;
            return distanceToPlayer.magnitude <= attackRange;
        }
        public bool InRange()
        {
            float distance = Vector3.Distance(transform.position, Player.position);
            return distance < detectionRange;
        }
        public bool SafeRange()
        {
            float distance = Vector3.Distance(transform.position, Player.position);
            return distance > safeRange;
        }

    }
}
