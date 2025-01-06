using UnityEngine;

public class SoldierAbilities : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletSpawn;
    private GameManager gameManager;

    [SerializeField] private float health = 10f;
    [SerializeField] private int pointsGiven = 100;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void ShootAtPlayer()
    {
        Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Health: " + health);
        if (health <= 0)
        {
            gameManager.enemyCount--;
            gameManager.AddScore(pointsGiven);
            gameManager.AddEnemyKilled();
            Destroy(gameObject);
        }
    }
}
