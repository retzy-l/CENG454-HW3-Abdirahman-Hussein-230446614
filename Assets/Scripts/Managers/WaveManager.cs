// WaveManager.cs
// CENG 454 – HW3: Core Breach
// Author: ABDIRAHMAN HUSSEIN | Student ID: 230446614
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ObjectPool enemyPool;
    [SerializeField] private Transform coreTransform;
    [SerializeField] private Transform[] spawnPoints;

    [Header("Wave Settings")]
    [SerializeField] private int totalWaves = 4;
    [SerializeField] private int enemiesPerWave = 6;
    [SerializeField] private float timeBetweenWaves = 1f;

    private int currentWave = 0;
    private int enemiesAlive = 0;
    private bool gameOver = false;

    public static event Action<int, int> OnWaveStarted;
    public static event Action OnAllWavesCleared;

    void Start()
    {
        Enemy.OnEnemyDied += HandleEnemyDied;
        CoreHealth.OnCoreDead += HandleCoreDead;
        StartCoroutine(RunWaves());
    }

    void OnDestroy()
    {
        Enemy.OnEnemyDied -= HandleEnemyDied;
        CoreHealth.OnCoreDead -= HandleCoreDead;
    }

    private IEnumerator RunWaves()
    {
        yield return new WaitForSeconds(2f);

        for (int w = 1; w <= totalWaves; w++)
        {
            if (gameOver) yield break;

            currentWave = w;
            enemiesAlive = enemiesPerWave; // SET FULL COUNT BEFORE SPAWNING

            OnWaveStarted?.Invoke(currentWave, totalWaves);

            for (int i = 0; i < enemiesPerWave; i++)
            {
                if (gameOver) yield break;
                SpawnEnemy();
                yield return new WaitForSeconds(0.8f);
            }

            
            float timeout = 5f;
            float elapsed = 0f;
            while (enemiesAlive > 0 && !gameOver && elapsed < timeout)
            {
                elapsed += Time.deltaTime;
                Debug.Log("Waiting... enemiesAlive: " + enemiesAlive + " elapsed: " + elapsed);
                yield return null;
            }
            enemiesAlive = 0; // force reset

            if (gameOver) yield break;

            if (w < totalWaves)
                yield return new WaitForSeconds(timeBetweenWaves);
        }

        if (!gameOver)
            OnAllWavesCleared?.Invoke();
    }

    private void SpawnEnemy()
    {
        if (spawnPoints == null || spawnPoints.Length == 0) return;

        Transform spawnPoint = spawnPoints[
            UnityEngine.Random.Range(0, spawnPoints.Length)];

        GameObject enemyObj = enemyPool.Get();
        enemyObj.transform.position = spawnPoint.position;

        Enemy enemy = enemyObj.GetComponent<Enemy>();
        if (enemy != null)
        {
            IMovementStrategy strategy = currentWave <= 1
                ? (IMovementStrategy)new DirectChaseStrategy()
                : new FlankingStrategy();

            enemy.Init(strategy, coreTransform, enemyPool);
        }
    }

    private void HandleEnemyDied(Enemy e)
    {
        enemiesAlive = Mathf.Max(0, enemiesAlive - 1);
        Debug.Log("Enemy died. Enemies alive: " + enemiesAlive);
    }

    private void HandleCoreDead()
    {
        gameOver = true;
    }
}