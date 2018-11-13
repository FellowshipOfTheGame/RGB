using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBHV : EquipmentBHV
{
    [SerializeField]
    public float fireRate;
    [SerializeField]
    public float intensityMult; // Multiplicador de intensidade: dano/raio
    [SerializeField]
    public float drainedPower; // Energia drenada do gerador
    [SerializeField]
    public float projectileSpeed;
    [SerializeField]
    public float projectileAcceleration;
    public GameObject projectilePrefab;

    private EnergyGeneratorBHV generator;

    [TagSelector]
    public List<string> tagsToHit = new List<string>();

    //private float shootDeltaTime;
    protected float shootTimer = 0;

    private void Awake()
    {
        generator = GetComponentInParent<EnergyGeneratorBHV>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        //if (DemoManager.debugMode) UpdateEquipmentAttributes(); // FIXME: local provisório
    }

    protected override void UpdateEquipmentAttributes()
    {
        if (equipmentData == null)
        {
            return;
        }
        fireRate = ((WeaponSO)equipmentData).FireRate();
        intensityMult = ((WeaponSO)equipmentData).IntensityMult();
        drainedPower = ((WeaponSO)equipmentData).DrainedPower();
        projectileSpeed = ((WeaponSO)equipmentData).ProjectileSpeed();
        projectileAcceleration = ((WeaponSO)equipmentData).ProjectileAcceleration();
    }

    public virtual void Fire()
    {
        SincronizeShots();
        if (shootTimer >= 1 / fireRate)
        {
            if (generator.Consume(drainedPower))
            {
                shootTimer %= (1/fireRate);
                InstantiateProjectile();
            }  
        }
    }

    private void SincronizeShots ()
    {
        if (shootTimer > 2 * (1 / fireRate)) // para corrigir sincronia entre os tiros
        {
            shootTimer = 2 * (1 / fireRate) + 0.000001f; // soma valor pequeno para evitar que resultado seja menor (ponto flutuante)
        }
    }

    protected virtual void InstantiateProjectile ()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.GetComponent<ProjectileBHV>().Shoot(projectileSpeed, projectileAcceleration, intensityMult, tagsToHit);
    }
}
