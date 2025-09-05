using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI waveText;

    [Header("Prefabs")]
    [SerializeField] private GameObject flagPrefab;
    [SerializeField] private GameObject enemyPrefab;

    [Header("Spawn Points")]
    [SerializeField] private Transform flagSpawnPointsParent;
    [SerializeField] private Transform enemySpawnPointsParent;

    [Header("Enemy Spawn Settings")]
    [SerializeField] private float initialSpawnRate = 6f;   // first wave spawn delay
    [SerializeField] private float minSpawnRate = 0.5f;     // fastest spawn delay
    [SerializeField] private float spawnRateDecrease = 0.2f; // how much faster per wave

    private List<Transform> flagSpawnPoints = new List<Transform>();
    private List<Transform> enemySpawnPoints = new List<Transform>();

    private int currentWave = 1;
    private int flagsRemaining;
    private int enemiesAlive;
    private bool spawningEnemies;

    private void Start()
    {
        // Collect spawn points
        foreach (Transform child in flagSpawnPointsParent)
            flagSpawnPoints.Add(child);

        foreach (Transform child in enemySpawnPointsParent)
            enemySpawnPoints.Add(child);

        StartWave();
    }
   
    private void StartWave()
    {
       

        if (waveText != null)
        {
            waveText.text = $"Wave: {currentWave}";
        }

        // ---------------- Flags ----------------
        int flagsToSpawn = Mathf.Min(currentWave, flagSpawnPoints.Count);
        flagsRemaining = flagsToSpawn;

        List<Transform> availableFlagPoints = new List<Transform>(flagSpawnPoints);

        for (int i = 0; i < flagsToSpawn; i++)
        {
            int index = Random.Range(0, availableFlagPoints.Count);
            Transform chosenPoint = availableFlagPoints[index];

            Instantiate(flagPrefab, chosenPoint.position, Quaternion.identity);

            availableFlagPoints.RemoveAt(index);
        }

        // ---------------- Enemies ----------------
        float currentSpawnRate = Mathf.Max(initialSpawnRate - (currentWave - 1) * spawnRateDecrease, minSpawnRate);
        spawningEnemies = true;
        StartCoroutine(SpawnEnemiesRoutine(currentSpawnRate));

        // Update stats
        GameManager.Instance.maxWaveReached = currentWave;
    }

    private IEnumerator SpawnEnemiesRoutine(float spawnRate)
    {
        while (spawningEnemies)
        {

            // --- Check if player exists ---
            GameObject player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                
                spawningEnemies = false;
                yield break; // stop coroutine
            }

            if (flagsRemaining > 0)
            {
                // Spawn 2 enemies
                for (int i = 0; i < 2; i++)
                {
                    int randomIndex = Random.Range(0, enemySpawnPoints.Count);
                    Instantiate(enemyPrefab, enemySpawnPoints[randomIndex].position, Quaternion.identity);
                    enemiesAlive++;
                }
            }
            else
            {
                // stop spawning if no flags left
                spawningEnemies = false;
                break;
            }

            yield return new WaitForSeconds(spawnRate);
        }
    }

    // Called by Flag script when collected
    public void FlagCollected()
    {
        flagsRemaining--;
        GameManager.Instance.totalFlagsCollected++;

        if (flagsRemaining <= 0 && enemiesAlive <= 0)
            NextWave();
    }

    // Called by Enemy script when it dies
    public void EnemyDied()
    {
        enemiesAlive--;
        GameManager.Instance.totalKills++;

        if (flagsRemaining <= 0 && enemiesAlive <= 0)
            NextWave();
    }

    private void NextWave()
    {
        currentWave++;
        GameManager.Instance.IncreaseDifficulty();
        StartWave();
    }
}
