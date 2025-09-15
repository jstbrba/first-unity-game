using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsConfig", menuName = "Configs/Player/PlayerStatsConfig")]
public class PlayerStatsConfig : ScriptableObject
{
    public float Speed = 6f;
    public int Attack = 3;
    public float jumpPower = 10f;
}

