// Enemy.cs
// CENG 454 – HW3: Core Breach
// Author: ABDIRAHMAN HUSSEIN | Student ID: 230446614
using UnityEngine;
using System;

public class Enemy : MonoBehaviour, IDamageable, IPoolable
{
    [SerializeField] private float maxHealth = 30f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float lifetime = 20f;

    private float currentHealth;
    private float lifeTimer;
    private IMovementStrategy movementStrategy;
    private Transform coreTransform;
    private ObjectPool enemyPool;
    private bool isDying = false;

    public static event Action<Enemy> OnEnemyDied;

    public void Init(IMovementStrategy strategy, Transform core, ObjectPool pool)
    {
        movementStrategy = strategy;
        coreTransform = core;
        enemyPool = pool;
    }

    public void OnSpawn()
    {
        currentHealth = maxHealth;
        lifeTimer = 0f;
        isDying = false;
        CoreHealth.OnCoreDead -= HandleCoreDead;
        CoreHealth.OnCoreDead += HandleCoreDead;
    }

    public void OnReturnToPool()
    {
        CoreHealth.OnCoreDead -= HandleCoreDead;
        isDying = false;
    }

    void Update()
    {
        if (coreTransform == null) return;
        movementStrategy?.Move(transform, coreTransform, moveSpeed);

        // Hard floor lock
        Vector3 pos = transform.position;
        if (pos.y != 1f)
        {
            pos.y = 1f;
            transform.position = pos;
        }

        // Auto die if stuck too long
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifetime)
            Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDying) return;

        // Only react to Core and Player — ignore everything else
        if (other.CompareTag("Core"))
        {
            CoreHealth core = other.GetComponentInParent<CoreHealth>();
            if (core != null) core.TakeDamage(damage);
            Die();
            return;
        }

        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponentInParent<PlayerHealth>();
            if (player != null) player.TakeDamage(damage);
            Die();
            return;
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDying) return;
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    public bool IsDead() => currentHealth <= 0;

    private void Die()
    {
        if (isDying) return;
        isDying = true;
        OnEnemyDied?.Invoke(this);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (enemyPool != null)
            enemyPool.Return(gameObject);
        else
            gameObject.SetActive(false);
    }

    private void HandleCoreDead()
    {
        if (!isDying) ReturnToPool();
    }
}