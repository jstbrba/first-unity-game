using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class EnemySpawner : MonoBehaviour {
        [Header("Enemy Data")]
        [SerializeField] private List<EnemyData> enemyData;

        [Header("Spawn Settings")]
        [SerializeField] private float spawnInterval = 5f;

        [Header("Spawn Points")]
        [SerializeField] private List<Transform> spawnPoints;

        [Header("Enemy Pool")]
        [SerializeField] private int poolSize = 10;

        private EnemyFactory enemyFactory;
        private List<GameObject> enemyPool = new List<GameObject>();

        private float spawnTimer;
        private int enemyCount;

        private void Start()
        {
            enemyFactory = new EnemyFactory();

            for (int i = 0; i < poolSize; i++)
            {
                EnemyData data = enemyData[Random.Range(0, enemyData.Count)];
                Transform spawnPoint = spawnPoints[0];
                GameObject enemy = enemyFactory.CreateEnemy(data, spawnPoint);
                enemy.SetActive(false);
                enemyPool.Add(enemy);
            }
        }

        private void Update()
        {
            spawnTimer += Time.deltaTime;

            if (ActiveEnemyCount() < poolSize && spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0;
            }
        }
        private void SpawnEnemy()
        {
            foreach (var enemy in enemyPool)
            {
                if (!enemy.activeInHierarchy)
                {
                    EnemyData data = enemyData[Random.Range(0, enemyData.Count)];
                    Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                    enemy.transform.position = spawnPoint.position;
                    enemy.GetComponent<Health>().SetCurrentHealth(data.maxHealth);
                    enemy.SetActive(true);
                    return;
                }
            }
        }
        private int ActiveEnemyCount()
        {
            int count = 0;
            foreach(var enemy in enemyPool)
                if (enemy.activeInHierarchy) count++;
            return count;
        }
    }
}
