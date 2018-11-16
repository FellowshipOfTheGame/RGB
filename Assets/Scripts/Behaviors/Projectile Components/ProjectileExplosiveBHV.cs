using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines an explosive projectile behavior.
/// </summary>
public class ProjectileExplosiveBHV : ProjectileBHV
{
    public GameObject childProjectilePrefab;
    public float radiusScale = 1;

    private const float STOP_VELOCITY_THRESHOLD = 1.0f;

    private void Update()
    {
        // Calls Impact() if velocity reaches zero
        if (rigidBody.velocity.magnitude < STOP_VELOCITY_THRESHOLD)
        {
            Impact();
            Debug.Log("Velocity 0, Explode");
        }
    }

    protected override void Impact(Collider2D other = null)
    {
        base.Impact(other);
        GameObject childProjectile = Instantiate(childProjectilePrefab, transform.position, transform.rotation);
        childProjectile.transform.localScale *= radiusScale * intensityMult;
        childProjectile.GetComponent<ProjectileBHV>().Shoot(0f, 0f, intensityMult,tagsToHit);
    }

    private void OnDestroy()
    {
        
    }
}
