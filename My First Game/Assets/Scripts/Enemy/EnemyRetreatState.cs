using UnityEngine;

namespace Game
{
    public class EnemyRetreatState : EnemyBaseState
    {
        public EnemyRetreatState(Enemy enemy, Animator anim) : base(enemy, anim) { }

        public override void OnEnter()
        {
            Debug.Log("Enemy entered Retreat State");
            anim.Play(WalkBackHash);
        }
        public override void FixedUpdate()
        {
            enemy.Retreat();
        }
    }
}
