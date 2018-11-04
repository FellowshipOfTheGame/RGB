using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplosiveBHV : ProjectileBHV
{
    public GameObject childProjectilePrefab;

    private void Update()
    {
        if (rigidBody.velocity.y < 0)
        {
            Impact();
            Debug.Log("Velocity 0, Explode");
        }
    }

    protected override void Impact(Collider2D other = null)
    {
        base.Impact();
        GameObject childProjectile = Instantiate(childProjectilePrefab, transform.position, transform.rotation);
        childProjectile.GetComponent<ProjectileBHV>().Shoot(0f, 0f, tagsToHit);
    }

    private void OnDestroy()
    {
        
    }
}
