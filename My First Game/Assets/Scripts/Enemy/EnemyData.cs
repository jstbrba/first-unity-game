using UnityEngine;
namespace Game
{
    [CreateAssetMenu (fileName = "EnemyData", menuName = "Enemy/EnemyData", order = 0)]
    public class EnemyData : ScriptableObject
    {
        public GameObject enemyPrefab;
        public int maxHealth;
        public float speed;
        public int MoneyOnDeath;
    }
}
