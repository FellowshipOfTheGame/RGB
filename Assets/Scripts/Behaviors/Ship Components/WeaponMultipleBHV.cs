using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMultipleBHV : WeaponBHV
{
    [Header("Multiple Guns")]
    [Tooltip("Local transform positions of the multiple guns.")]
    public List<Vector2> gunsTransformPositions;
    [Tooltip("Lateral base speed of the projectiles. It is modified by the distance to the central position.x.")]
    public float lateralSpeed = 1.0f;

    private int currentGunIndex = 0;


    /// <summary>
    /// Orders weapon to shoot a projectile, if possible, and drains power accordingly.
    /// </summary>
    public override void Fire()
    {
        if (gunsTransformPositions.Count <= 0)
        {
            return;
        }
        base.Fire();
    }

    // Instancia (dispara) um projétil, com parâmetros conforme especificação da arma
    protected override void InstantiateProjectile()
    {
        // Shoots from correct position
        Vector2 transfPosition = transform.position;
        transfPosition += gunsTransformPositions[currentGunIndex]; // adjusts shot transform position
        GameObject projectile = Instantiate(projectilePrefab, transfPosition, transform.rotation);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(gunsTransformPositions[currentGunIndex].x * lateralSpeed, 0f);
        projectile.GetComponent<ProjectileBHV>().Shoot(projectileSpeed, projectileAcceleration, intensityMult, tagsToHit);
        // Switches to the next gun
        currentGunIndex++;
        if (currentGunIndex >= gunsTransformPositions.Count)
        {
            currentGunIndex = 0;
        }
    }
}
