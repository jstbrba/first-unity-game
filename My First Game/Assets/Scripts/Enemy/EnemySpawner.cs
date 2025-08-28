using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class EnemySpawner : MonoBehaviour {
        [Header("Enemy Data")]
        [SerializeField] private List<EnemyData> enemyData;

        [Header("Spawn Settings")]
        [SerializeField] private int maxEnemies = 5;
        [SerializeField] private float spawnInterval = 5f;

        [Header("Spawn Points")]
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

            // TODO : Optimisation - Use flyweight pattern for object pooling
            enemyFactory.CreateEnemy(data, spawnPoint);
            enemyCount++;
        }
    }
}
