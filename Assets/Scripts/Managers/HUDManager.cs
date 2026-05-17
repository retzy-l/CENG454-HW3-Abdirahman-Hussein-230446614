// HUDManager.cs
// CENG 454 – HW3: Core Breach
// Author: ABDIRAHMAN HUSSEIN | Student ID: 230446614
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coreHealthText;
    [SerializeField] private TMP_Text playerHealthText;
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text statusText;

    void Start()
    {
        // Subscribe in Start instead of OnEnable
        CoreHealth.OnHealthChanged += UpdateCoreHealth;
        PlayerHealth.OnHealthChanged += UpdatePlayerHealth;
        WaveManager.OnWaveStarted += UpdateWave;
        WaveManager.OnAllWavesCleared += HandleWin;
        CoreHealth.OnCoreDead += HandleCoreDead;

        // Set initial values manually
        UpdateCoreHealth(100f, 100f);
        UpdatePlayerHealth(100f, 100f);
        UpdateWave(0, 4);
    }

    void OnDestroy()
    {
        CoreHealth.OnHealthChanged -= UpdateCoreHealth;
        PlayerHealth.OnHealthChanged -= UpdatePlayerHealth;
        WaveManager.OnWaveStarted -= UpdateWave;
        WaveManager.OnAllWavesCleared -= HandleWin;
        CoreHealth.OnCoreDead -= HandleCoreDead;
    }

    private void UpdateCoreHealth(float current, float max)
    {
        Debug.Log("HUD received core health update: " + current);
        if (coreHealthText != null)
            coreHealthText.text = $"Core: {current:0}/{max:0}";
    }

    private void UpdatePlayerHealth(float current, float max)
    {
        if (playerHealthText != null)
            playerHealthText.text = $"HP: {current:0}/{max:0}";
    }

    private void UpdateWave(int wave, int total)
    {
        if (waveText != null)
            waveText.text = $"Wave {wave}/{total}";
        if (statusText != null)
            statusText.text = $"Wave {wave} incoming!";
    }

    private void HandleWin()
    {
        if (statusText != null)
            statusText.text = "All waves cleared! You Win!";
        if (waveText != null)
            waveText.text = "Complete!";
    }

    private void HandleCoreDead()
    {
        if (statusText != null)
            statusText.text = "Core Destroyed! Game Over!";
    }
}