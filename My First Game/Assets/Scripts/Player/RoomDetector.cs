using UnityEngine;
namespace Game {
    public class RoomDetector : MonoBehaviour {
        private CameraController cam;

        private void Start()
        {
            cam = Camera.main.GetComponent<CameraController>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            BoxCollider2D room = collision.GetComponent<BoxCollider2D>();
            if (room != null)
            {
                cam.SetRoom(room);
            }
        }
    }
}
