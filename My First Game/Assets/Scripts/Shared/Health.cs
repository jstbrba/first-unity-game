using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int startingHealth;
    public int currentHealth { get; private set; }

    public event Action<int,int> OnHealthChange;

    private void Awake()
    {
        currentHealth = startingHealth;
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = (int)Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            Debug.Log(gameObject.name + " took " + _damage + " damage");
        } else
        {
            Debug.Log(gameObject.name + " is dead");
            gameObject.SetActive(false);
        }
        OnHealthChange?.Invoke(currentHealth, startingHealth);
    }
}
