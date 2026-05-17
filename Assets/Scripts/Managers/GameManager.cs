// GameManager.cs
// CENG 454 – HW3: Core Breach
// Author: ABDIRAHMAN HUSSEIN | Student ID: 230446614
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Observer subscribers — react to game events
    void OnEnable()
    {
        CoreHealth.OnCoreDead += HandleCoreDead;
        PlayerHealth.OnPlayerDead += HandlePlayerDead;
        WaveManager.OnAllWavesCleared += HandleWin;
    }

    void OnDisable()
    {
        CoreHealth.OnCoreDead -= HandleCoreDead;
        PlayerHealth.OnPlayerDead -= HandlePlayerDead;
        WaveManager.OnAllWavesCleared -= HandleWin;
    }

    private void HandleCoreDead()
    {
        Debug.Log("GAME OVER — Core destroyed!");
        StartCoroutine(RestartAfterDelay("Core Destroyed! Game Over."));
    }

    private void HandlePlayerDead()
    {
        Debug.Log("GAME OVER — Player died!");
        StartCoroutine(RestartAfterDelay("You Died! Game Over."));
    }

    private void HandleWin()
    {
        Debug.Log("YOU WIN — All waves cleared!");
        StartCoroutine(RestartAfterDelay("Mission Complete! You Win!"));
    }

    private IEnumerator RestartAfterDelay(string message)
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}