using UnityEngine;

namespace Game
{
    public class EnemyRetreatState : EnemyBaseState
    {
        public EnemyRetreatState(Enemy enemy, Animator anim) : base(enemy, anim) { }

        public override void OnEnter()
        {
            anim.Play(RetreatHash);
        }
        public override void Update()
        {
            enemy.FacePlayer();
        }
        public override void FixedUpdate()
        {
            enemy.Retreat();
        }
    }
}
