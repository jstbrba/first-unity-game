using UnityEngine;

namespace Game
{
    public class EnemyAttackState : EnemyBaseState
    {
        private bool attackFinished;
        private readonly EnemyAttack enemyAttack;
        public EnemyAttackState(Enemy enemy, Animator anim, EnemyAttack enemyAttack) : base(enemy, anim) 
        {
            this.enemyAttack = enemyAttack;
        }

        public override void OnEnter()
        {
            attackFinished = false;
            anim.Play(AttackHash);

            enemyAttack.OnEnemyAttackEnd += HandleAttackEnd;
        }
        public override void OnExit()
        {
            enemyAttack.OnEnemyAttackEnd -= HandleAttackEnd;
        }
        private void HandleAttackEnd() => attackFinished = true;
        public bool IsAttackFinished => attackFinished;
    }
}
