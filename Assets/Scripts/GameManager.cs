using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private float minSpawnDistanceFromPlayer = 10.0f;
    [SerializeField] private float spawnRange = 10.0f;
    [SerializeField] private DifficultyLevel[] difficultyLevels = null;
    [SerializeField] private GameObject[] enemyPrefabs = null;
    [Tooltip("The lowest difficulty level is 0")]
    [SerializeField] private int startDifficultyLevel = 0;
    
    [HideInInspector] public int enemyCount = 0;
    private int spawnedEnemies = 0;
    private float spawnTimer = 0.0f;
    private float timeAlive = 0.0f;
    private int currentDifficultyLevel = 0;

    private void Start()
    {
        // Start the game with the first difficulty level
        currentDifficultyLevel = startDifficultyLevel;
        Time.timeScale = 1.0f;
    }

    private void Update(){

        // Spawn enemies
        if (spawnTimer > difficultyLevels[currentDifficultyLevel].spawnInterval){   // Check if it's time to spawn an enemy
            if (enemyCount < difficultyLevels[currentDifficultyLevel].maxEnemiesOnGround){  // Check if there are too many enemies on the ground
                if (spawnedEnemies < difficultyLevels[currentDifficultyLevel].waveLength){  // Check if the wave is over
                    SpawnEnemy();
                } else if (enemyCount == 0){    // If the wave is over and there are no enemies left, increase the difficulty
                    IncreaseDifficulty();
                    spawnedEnemies = 0;
                }
            }
            spawnTimer = 0.0f;
        }

        #region Timers

        timeAlive += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        #endregion
    }

    private void SpawnEnemy(){

        for (int i = 0; i < difficultyLevels[currentDifficultyLevel].enemiesPerCycle; i++){
            // Get a random position in nav mesh area
            Vector3 spawnPosition = GetRandomNavmeshLocation(spawnRange);

            // Get a random enemy prefab with the correct percentage
            GameObject enemyPrefab = GetRandomEnemyPrefab();

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemyCount++;
            spawnedEnemies++;
        }
        
    }

    private void IncreaseDifficulty(){
        currentDifficultyLevel++;
    }

    private void ResetGame(){
        currentDifficultyLevel = 0;
        timeAlive = 0.0f;
        enemyCount = 0;
    }

    Vector3 GetRandomNavmeshLocation(float range){
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * range;


        // Check if the random point is too close to the player
        while (Vector3.Distance(randomPoint, player.transform.position) < minSpawnDistanceFromPlayer){
            randomPoint = transform.position + Random.insideUnitSphere * range;
        }

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas)){
            return hit.position;
        } else {
            Debug.LogWarning("Couldn't find a valid position");
            return transform.position;
        }
    }

    GameObject GetRandomEnemyPrefab(){
        int random = Random.Range(0, 100);

        if (random < difficultyLevels[currentDifficultyLevel].shooterPercentage){
            return enemyPrefabs[0];
        } else if (random > difficultyLevels[currentDifficultyLevel].shooterPercentage){
            return enemyPrefabs[1];
        }

        return enemyPrefabs[0];
    }

    public void PlayerDied(){
        deathScreen.SetActive(true);
    }
    
    
}
