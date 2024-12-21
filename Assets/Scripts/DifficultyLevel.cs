using UnityEngine;

[System.Serializable]
public class DifficultyLevel
{
    [SerializeField] public int enemiesPerCycle;
    [SerializeField] public int maxEnemiesOnGround;
    [SerializeField] public float spawnInterval;
    [SerializeField] public float spawnintervalVariance;
    [SerializeField] public int shooterPercentage;
    [SerializeField] public int puncherPercentage;
    [SerializeField] public int waveLength;

    public DifficultyLevel(int enemiesPerCycle, int maxEnemiesOnGround, float spawnInterval, float spawnintervalVariance, int shooterPercentage, int puncherPercentage, int waveLength)
    {
        this.enemiesPerCycle = enemiesPerCycle;
        this.maxEnemiesOnGround = maxEnemiesOnGround;
        this.spawnInterval = spawnInterval;
        this.spawnintervalVariance = spawnintervalVariance;
        this.shooterPercentage = shooterPercentage;
        this.puncherPercentage = puncherPercentage;
        this.waveLength = waveLength;

    }
}
