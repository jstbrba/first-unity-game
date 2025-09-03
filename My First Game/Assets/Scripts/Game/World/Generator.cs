using System;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private int maxPower;
    private int currentPower;

    public event Action<int,int> OnPowerChange;
    public event Action OnPowerDown;
    private void Start()
    {
        currentPower = maxPower;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            TakeDamage(2);;
        }
    }
    private void TakeDamage(int damage)
    {
        currentPower = Mathf.Clamp(currentPower - damage, 0, maxPower);

        if (currentPower == 0)
        {
            OnPowerDown?.Invoke();
            Debug.Log("POWER DOWN");
        }

        OnPowerChange?.Invoke(currentPower, maxPower);
    }
    public void Repair(int value) => currentPower = Mathf.Clamp(currentPower + value, 0, maxPower);
}
