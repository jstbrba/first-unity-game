using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;
    public int CurrentHealth => currentHealth;
    public void SetCurrentHealth(int health) => currentHealth = health;

    public event Action<int,int> OnHealthChange;
    public event Action OnDeath;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = (int)Mathf.Clamp(currentHealth - _damage, 0, maxHealth);

        if (currentHealth == 0)
        {
            OnDeath?.Invoke();
        }
        OnHealthChange?.Invoke(currentHealth, maxHealth);
    }
    public void SetMaxHealth(int maxHealth) => this.maxHealth = Mathf.Max(0, maxHealth);
}
