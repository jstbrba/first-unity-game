using UnityEngine;

namespace Game
{
    public class EnemyRetreatState : EnemyBaseState
    {
        public EnemyRetreatState(Enemy enemy, Animator anim) : base(enemy, anim) { }

        public override void OnEnter()
        {
            Debug.Log("Enemy Entered Retreat State");
            anim.Play(RetreatHash);
        }
        public override void FixedUpdate()
        {
            enemy.Retreat();
        }
    }
}
