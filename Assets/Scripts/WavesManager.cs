using System.Collections;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int normalEnemyCount;       // Number of normal enemies
        public GameObject[] enemyPrefabs;  // List of normal enemy prefabs

        public int bossCount;              // Number of bosses in this wave
        public GameObject bossPrefab;      // Boss prefab

        public float spawnRate = 1f;       // Time delay between spawns
    }

    public Wave[] waves;
    public Transform[] spawnPoints;

    [SerializeField]private int currentWaveIndex = 0;
    public int enemiesAlive = 0;

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("All waves finished!");
            yield break;
        }

        Debug.Log("Starting Wave " + (currentWaveIndex + 1));

        Wave wave = waves[currentWaveIndex];

        // Spawn normal enemies
        for (int i = 0; i < wave.normalEnemyCount; i++)
        {
            SpawnRandomEnemy(wave.enemyPrefabs);
            yield return new WaitForSeconds(wave.spawnRate);
        }

        // Spawn boss enemies
        for (int i = 0; i < wave.bossCount; i++)
        {
            SpawnBoss(wave.bossPrefab);
            yield return new WaitForSeconds(wave.spawnRate);
        }
    }

    void SpawnRandomEnemy(GameObject[] enemyPrefabs)
    {
        if (enemyPrefabs.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoint.position, spawnPoint.rotation);

        enemiesAlive++;
    }

    void SpawnBoss(GameObject bossPrefab)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);

        enemiesAlive++; // you want to complete the waves list uncomment this line
    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0)
        {
            currentWaveIndex++;
            StartCoroutine(StartNextWave());
        }
    }
}
