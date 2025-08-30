using UnityEngine;
namespace Game
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform Player;
        private BoxCollider2D roomBounds;

        private float halfHeight;
        private float halfWidth;

        [SerializeField] private float smoothSpeed = 5f;
        private Vector3 velocity = Vector3.zero;

        private void Start()
        {
            Camera cam = Camera.main;
            halfHeight = cam.orthographicSize;
            halfWidth = cam.aspect * halfHeight;
        }
        private void Update()
        {
            if (Player == null || roomBounds == null) return;

            Vector3 targetPosition;

            float xClamp = Mathf.Clamp(Player.position.x,
                roomBounds.bounds.min.x + halfWidth,
                roomBounds.bounds.max.x - halfWidth);

            float yClamp = Mathf.Clamp(Player.position.y,
                roomBounds.bounds.min.y + halfHeight,
                roomBounds.bounds.max.y - halfHeight);

            targetPosition = new Vector3(xClamp, yClamp, transform.position.z);

            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref velocity,
                1f / smoothSpeed);
        }
        public void SetRoom(BoxCollider2D newBounds)
        {
            roomBounds = newBounds;
        }
    }
}
