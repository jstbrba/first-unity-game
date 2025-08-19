using UnityEngine;
namespace Game
{
    public class InputReader : MonoBehaviour {
        public float moveAxis => Input.GetAxis("Horizontal");
        public bool jumpPressed => Input.GetKey(KeyCode.W);
        public bool crouchPressed => Input.GetKey(KeyCode.S);
        public bool sprintPressed => Input.GetKey(KeyCode.LeftShift);
        public bool attackPressed => Input.GetKey(KeyCode.E);
    }
}
