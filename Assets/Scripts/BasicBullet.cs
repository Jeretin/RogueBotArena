using UnityEngine;

public class BasicBullet : BulletBehavior
{
    // Override the OnTriggerEnter method to destroy the bullet when it hits an enemy
    public override void OnTriggerEnter(Collider col)
    {
        
    }

    public override void SpawnBullet(Vector3 spawnPosition, Quaternion spawnRotation, GameObject targetObject)
    {
        base.SpawnBullet(spawnPosition, spawnRotation, targetObject);
    }
}
