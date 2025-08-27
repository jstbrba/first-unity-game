using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    public int currentHealth { get; private set; }

    public event Action<int,int> OnHealthChange;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = (int)Mathf.Clamp(currentHealth - _damage, 0, maxHealth);

        if (currentHealth > 0)
        {
            Debug.Log(gameObject.name + " took " + _damage + " damage");
        } else
        {
            Debug.Log(gameObject.name + " is dead");
            gameObject.SetActive(false);
            // Destroy(gameObject); // TODO: Use object pooling instead
        }
        OnHealthChange?.Invoke(currentHealth, maxHealth);
    }
    public void SetMaxHealth(int maxHealth) => this.maxHealth = Mathf.Max(0, maxHealth);
}
