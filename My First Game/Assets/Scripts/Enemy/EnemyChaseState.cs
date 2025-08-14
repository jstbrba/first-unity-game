using UnityEngine;

namespace Game
{
    public class EnemyChaseState : EnemyBaseState
    {
        public EnemyChaseState(Enemy enemy, Animator anim) : base(enemy, anim) { }

        public override void OnEnter()
        {
            Debug.Log("Enemy entered Chase State");
            anim.Play(WalkHash);
        }
        public override void FixedUpdate()
        {
            enemy.Chase();
        }
    }
}
