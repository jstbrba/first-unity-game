using UnityEngine;

namespace Game
{
    public class CrouchState : BaseState
    {
        public CrouchState(PlayerController player, Animator anim) : base(player, anim) { }

        public override void OnEnter()
        {
            Debug.Log("Entering Crouch State");
            anim.Play(CrouchHash);
        }
    }
}
