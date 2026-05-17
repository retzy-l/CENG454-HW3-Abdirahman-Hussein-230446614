// AudioManager.cs
// CENG 454 – HW3: Core Breach
// Author: ABDIRAHMAN HUSSEIN | Student ID: 230446614
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    [Header("Sound Clips")]
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip bulletHitClip;
    [SerializeField] private AudioClip enemyDieClip;
    [SerializeField] private AudioClip winClip;
    [SerializeField] private AudioClip loseClip;
    [SerializeField] private AudioClip waveStartClip;

    public static AudioManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void OnEnable()
    {
        WaveManager.OnWaveStarted += HandleWaveStarted;
        WaveManager.OnAllWavesCleared += HandleWin;
        CoreHealth.OnCoreDead += HandleLose;
        PlayerHealth.OnPlayerDead += HandleLose;
        Enemy.OnEnemyDied += HandleEnemyDied;
    }

    void OnDisable()
    {
        WaveManager.OnWaveStarted -= HandleWaveStarted;
        WaveManager.OnAllWavesCleared -= HandleWin;
        CoreHealth.OnCoreDead -= HandleLose;
        PlayerHealth.OnPlayerDead -= HandleLose;
        Enemy.OnEnemyDied -= HandleEnemyDied;
    }

    public void PlayShoot()
    {
        if (shootClip != null)
            sfxSource.PlayOneShot(shootClip, 0.5f);
    }

    public void PlayBulletHit()
    {
        if (bulletHitClip != null)
            sfxSource.PlayOneShot(bulletHitClip, 0.7f);
    }

    private void HandleEnemyDied(Enemy e)
    {
        if (enemyDieClip != null)
            sfxSource.PlayOneShot(enemyDieClip, 0.8f);
    }

    private void HandleWaveStarted(int wave, int total)
    {
        if (waveStartClip != null)
            sfxSource.PlayOneShot(waveStartClip, 1f);
    }

    private void HandleWin()
    {
        if (winClip != null)
        {
            musicSource.Stop();
            sfxSource.PlayOneShot(winClip, 1f);
        }
    }

    private void HandleLose()
    {
        if (loseClip != null)
        {
            musicSource.Stop();
            sfxSource.PlayOneShot(loseClip, 1f);
        }
    }
}