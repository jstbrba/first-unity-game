using UnityEngine;

namespace Game
{
    public class WalkingBackState : BaseState
    {
        public WalkingBackState(PlayerController player, Animator anim) : base(player, anim) { }

        public override void OnEnter()
        {
            anim.Play(WalkBackHash);
            Debug.Log("Entering Walking Back State");
        }
        public override void FixedUpdate()
        {
            player.HandleMovement();
            player.HandleJump();
        }
    }
}
