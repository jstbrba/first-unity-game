using UnityEngine;
namespace Game
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform Player;
        private BoxCollider2D floorBounds;

        private float halfHeight;
        private float halfWidth;

        [SerializeField] private float smoothSpeed = 5f;
        private Vector3 velocity = Vector3.zero;
        [SerializeField] private float snapDistance = 5f; // If player is too far from camera, then it snaps to the player

        private void Start()
        {
            Camera cam = Camera.main;
            halfHeight = cam.orthographicSize;
            halfWidth = cam.aspect * halfHeight;
        }
        private void Update()
        {
            if (Player == null || floorBounds == null) return;

            Vector3 targetPosition;

            float xClamp = Mathf.Clamp(Player.position.x,
                floorBounds.bounds.min.x + halfWidth,
                floorBounds.bounds.max.x - halfWidth);

            float yClamp = Mathf.Clamp(Player.position.y,
                floorBounds.bounds.min.y + halfHeight,
                floorBounds.bounds.max.y - halfHeight);

            targetPosition = new Vector3(xClamp, yClamp, transform.position.z);

            float distance = Vector3.Distance(transform.position, targetPosition);
            Debug.Log(distance);

            if (distance < snapDistance)
            {
                transform.position = Vector3.SmoothDamp(
                    transform.position,
                    targetPosition,
                    ref velocity,
                    1f / smoothSpeed);
            }
            else
                transform.position = targetPosition;
        }
        public void SetRoom(BoxCollider2D newBounds)
        {
            if (newBounds.CompareTag("CameraBounds"))
                floorBounds = newBounds;
        }
    }
}
