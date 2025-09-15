using UnityEngine;
[CreateAssetMenu(fileName = "EnemyStatsConfig", menuName = "Configs/Enemy/EnemyStatsConfig")]
public class EnemyStatsConfig : ScriptableObject
{
    public float Speed;
    public int Attack;
    public int MoneyOnDeath;
}
