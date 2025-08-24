using UnityEngine;
namespace Game
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform Player;
        [SerializeField] private Vector3 roomPosition;

        [SerializeField] private Transform transitionPoint;

        [SerializeField] private float smoothSpeed = 5f;
        private Vector3 velocity = Vector3.zero;

        private void Update()
        {
            Vector3 targetPosition;

            if (Player.position.x < transitionPoint.position.x) targetPosition = roomPosition;
            else targetPosition = new Vector3(Player.position.x, 0f, -10f);

            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref velocity,
                1f / smoothSpeed);
        }
    }
}
