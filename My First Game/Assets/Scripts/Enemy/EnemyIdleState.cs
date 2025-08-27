using UnityEngine;

namespace Game
{
    public class EnemyIdleState : EnemyBaseState
    {
        public EnemyIdleState(Enemy enemy, Animator anim) : base(enemy, anim) { }

        public override void OnEnter()
        {
            Debug.Log("Enemy Entered Idle State");
            anim.Play(IdleHash);
        }
    }
}
