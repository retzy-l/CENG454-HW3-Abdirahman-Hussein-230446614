// PlayerHealth.cs
// CENG 454 – HW3: Core Breach
// Author: ABDIRAHMAN HUSSEIN | Student ID: 230446614
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    public static event Action<float, float> OnHealthChanged;
    public static event Action OnPlayerDead;

    void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        if (currentHealth <= 0)
            OnPlayerDead?.Invoke();
    }

    public bool IsDead() => currentHealth <= 0;
}