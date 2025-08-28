using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    public int Money { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void UpdateMoney(int amount) => Money = Mathf.Max(0, Money + amount);
}
