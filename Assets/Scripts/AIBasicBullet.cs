using UnityEngine;

public class AIBasicBullet : BulletBehavior
{
    // Override the OnTriggerEnter method to destroy the bullet when it hits the player
    public override void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
