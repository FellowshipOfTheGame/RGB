using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMultipleBHV : WeaponBHV
{
    public List<Vector2> gunsTransformPositions;
    public float horizontalSpeed = 1.0f;
    private int startGun = 0;

    public override  void Fire()
    {
        if (gunsTransformPositions.Count <= 0)
        {
            return;
        }
        base.Fire();
    }

    protected override void InstantiateProjectile()
    {
        Vector2 transfPosition = transform.position;
        transfPosition += gunsTransformPositions[startGun];
        Debug.Log("Transform.position: " + transform.position);
        Debug.Log("GunsTransf.position: " + gunsTransformPositions[startGun]);
        Debug.Log("Transf.position: " + transfPosition);
        GameObject projectile = Instantiate(projectilePrefab, transfPosition, transform.rotation);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(gunsTransformPositions[startGun].x * horizontalSpeed, 0f);
        projectile.GetComponent<ProjectileBHV>().Shoot(projectileSpeed, projectileAcceleration, intensityMult, tagsToHit);
        startGun++;
        if (startGun >= gunsTransformPositions.Count)
        {
            startGun = 0;
        }
    }
}
