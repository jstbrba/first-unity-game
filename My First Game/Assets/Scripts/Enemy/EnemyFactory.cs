using UnityEngine;
namespace Game
{
    public class EnemyFactory 
    {
        public GameObject CreateEnemy(EnemyData enemyData, Transform spawnPoint)
        {
            EnemyBuilder builder = new EnemyBuilder()
                .SetBasePrefab(enemyData.enemyPrefab)
                .SetMaxHealth(enemyData.maxHealth)
                .SetSpeed(enemyData.speed)
                .SetMoneyOnDeath(enemyData.MoneyOnDeath)
                .SetSpawnPoint(spawnPoint);

            return builder.Build();
        }
    }
}
