using UnityEngine;
namespace Game
{
    public class EnemyBuilder 
    {
        private GameObject enemyPrefab;
        private int maxHealth;
        private float speed;
        private int moneyOnDeath;
        private Transform spawnPoint;

        public EnemyBuilder SetBasePrefab(GameObject enemyPrefab)
        {
            this.enemyPrefab = enemyPrefab;
            return this;
        }
        public EnemyBuilder SetMaxHealth(int maxHealth)
        {
            this.maxHealth = maxHealth;
            return this;
        }
        public EnemyBuilder SetSpeed(float speed)
        {
            this.speed = speed;
            return this;
        }
        public EnemyBuilder SetMoneyOnDeath(int moneyOnDeath)
        {
            this.moneyOnDeath = moneyOnDeath;
            return this;
        }
        public EnemyBuilder SetSpawnPoint(Transform spawnPoint)
        {
            this.spawnPoint = spawnPoint;
            return this;
        }
        public GameObject Build()
        {
            GameObject enemy = GameObject.Instantiate(enemyPrefab);

            enemy.GetComponent<Health>().SetMaxHealth(maxHealth);
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            enemyComponent.SetMovementSpeed(speed);
            enemyComponent.SetMoneyOnDeath(moneyOnDeath);
            enemy.transform.position = spawnPoint.position;

            return enemy;
        }
    }
}
