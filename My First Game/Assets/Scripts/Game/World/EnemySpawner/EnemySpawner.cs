using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class EnemySpawner : MonoBehaviour {
        private IContext _context;
        private EnemySpawnerModel _model;

        [Header("Enemy Data")]
        [SerializeField] private List<EnemySettings> enemySettings;

        [Header("Spawn Settings")]
        [SerializeField] private float spawnInterval = 5f;

        [Header("Spawn Points")]
        [SerializeField] private List<Transform> spawnPoints;

        private float spawnTimer;
        private int enemyCount;
        private bool _limitReached;
        public void Initialise(IContext context)
        {
            _context = context;

            _model = _context.ModelLocator.Get<EnemySpawnerModel>();
            _model.SpawnCount.onValueChanged += Model_SpawnCount_OnValueChanged;
        }

        private void Update()
        {
            spawnTimer += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.T)) DespawnEnemiesOutOfView();

            if (spawnTimer > spawnInterval && !_limitReached)
            {
                SpawnEnemy();
                spawnTimer = 0;
                _model.SpawnCount.Value++; // CHANGE LATER COS THIS CLASS SHOULDN'T BE ALLOWED or not if i cba
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
        public void Model_SpawnCount_OnValueChanged(int previous, int current)
        {
            _limitReached = current >= _model.SpawnLimit.Value ? true : false;
        }
    }
}
