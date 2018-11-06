using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBHV : MonoBehaviour
{
    public WeaponSO weaponSettings;
    private int upgradeLevel = 1;
    public float fireRate;
    public float intensityMult; // Multiplicador de intensidade: dano/raio
    public float drainedPower; // Energia drenada do gerador
    public float projectileSpeed;
    public float projectileAcceleration;
    public GameObject projectilePrefab;

    [TagSelector]
    public List<string> tagsToHit = new List<string>();

    //private float shootDeltaTime;
    protected float shootTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;

        if (weaponSettings != null) weaponSettings.LoadToWeapon(this); // FIXME: local provisório

    }

    public virtual void Fire()
    {
        if (shootTimer >= 1 / fireRate)
        {
            shootTimer = 0;
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.GetComponent<ProjectileBHV>().Shoot(projectileSpeed, projectileAcceleration, intensityMult, tagsToHit);
        }
        
    }

    public void Upgrade()
    {

    }
}
