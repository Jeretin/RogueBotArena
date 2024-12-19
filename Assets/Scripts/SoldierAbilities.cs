using UnityEngine;

public class SoldierAbilities : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletSpawn;

    [SerializeField] private float health = 10f;


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
            Destroy(gameObject);
        }
    }
}
