// CoreHealth.cs
// CENG 454 – HW3: Core Breach
// Author: ABDIRAHMAN HUSSEIN | Student ID: 230446614
using UnityEngine;
using System;

public class CoreHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    // Observer events
    public static event Action<float, float> OnHealthChanged; // current, max
    public static event Action OnCoreDead;

    void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);
        Debug.Log("Core took damage! Current health: " + currentHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
            OnCoreDead?.Invoke();
    }

    public bool IsDead() => currentHealth <= 0;
}