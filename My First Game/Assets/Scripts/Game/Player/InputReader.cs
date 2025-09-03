using UnityEngine;
namespace Game
{
    public class InputReader : MonoBehaviour {
        // TODO: Switch to new input system
        public float moveAxis => Input.GetAxis("Horizontal");
        public bool jumpPressed => Input.GetKey(KeyCode.W);
        public bool crouchPressed => Input.GetKey(KeyCode.S);
        public bool sprintPressed => Input.GetKey(KeyCode.LeftShift);
        public bool interactPressed => Input.GetKeyDown(KeyCode.E);
        public bool placePressed => Input.GetKeyDown(KeyCode.F);
    }
}
