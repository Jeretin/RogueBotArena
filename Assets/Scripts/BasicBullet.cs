using UnityEngine;

public class BasicBullet : BulletBehavior
{
    // Override the OnTriggerEnter method to destroy the bullet when it hits an enemy
    public override void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<SoldierAbilities>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
