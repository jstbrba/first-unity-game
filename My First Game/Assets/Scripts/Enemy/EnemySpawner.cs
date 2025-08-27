using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class EnemySpawner : MonoBehaviour {
        [SerializeField] private List<EnemyData> enemyData;
        [SerializeField] private int maxEnemies = 5;
        [SerializeField] private float spawnInterval = 5f;

        [SerializeField] private List<Transform> spawnPoints;

        EnemyFactory enemyFactory;

        private float spawnTimer;
        private int enemyCount;

        private void Start() => enemyFactory = new EnemyFactory();

        private void Update()
        {
            spawnTimer += Time.deltaTime;

            if (enemyCount < maxEnemies && spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0;
            }
        }
        private void SpawnEnemy()
        {
            EnemyData data = enemyData[Random.Range(0, enemyData.Count)];
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            // TODO: Enemy pooling
            enemyFactory.CreateEnemy(data, spawnPoint);
            enemyCount++;
        }
    }
}
