using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class EnemySpawner : MonoBehaviour {
        [Header("Enemy Data")]
        [SerializeField] private List<EnemySettings> enemySettings;

        [Header("Spawn Settings")]
        [SerializeField] private float spawnInterval = 5f;

        [Header("Spawn Points")]
        [SerializeField] private List<Transform> spawnPoints;

        private float spawnTimer;
        private int enemyCount;

        private void Update()
        {
            spawnTimer += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.T)) DespawnEnemiesOutOfView(); 

            if (spawnTimer > spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0;
            }
        }
        private void SpawnEnemy()
        {
            var enemy = FlyweightFactory.Spawn(enemySettings[Random.Range(0,enemySettings.Count)]);
            enemy.transform.position = spawnPoints[Random.Range(0,spawnPoints.Count)].position;
        }
        public void DespawnEnemiesOutOfView()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                if (enemy.GetComponent<PlayerDetector>().OutOfView())
                {
                    Enemy enemyComponent = enemy.GetComponent<Enemy>();
                    FlyweightFactory.ReturnToPool(enemyComponent);
                }
            }
                    
        }
    }
}
