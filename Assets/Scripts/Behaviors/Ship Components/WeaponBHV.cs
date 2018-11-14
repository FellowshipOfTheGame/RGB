using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines specific behaviours for the equipment type: Weapon.
/// </summary>
public class WeaponBHV : EquipmentBHV
{
    [Header("Projectile")]
    public GameObject projectilePrefab; // Prefab a ser instanciado como projétil
    [TagSelector]
    public List<string> tagsToHit = new List<string>();

    // These weapons specifications don't need to be initialized from the inspector if the equipmentData is not null
    [Header("Attributes - alternative to data-defined")]
    public float fireRate;
    public float intensityMult; // Multiplicador de intensidade: dano/raio
    public float drainedPower; // Energia drenada do gerador
    public float projectileSpeed;
    public float projectileAcceleration;


    protected float shootTimer = 0;

    private EnergyGeneratorBHV generator; // Componente do gerador

    // Awake is called when the script is first loaded
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

    /// <summary>
    /// Orders weapon to shoot a projectile, if possible, and drains power accordingly.
    /// </summary>
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

    // Estabelece um limite para a espera do tiro, de forma que armas de mesma frequência que percam a sincronia possam ser ressincronizadas
    private void SincronizeShots ()
    {
        // FIXME: para versão final, '1/fireRate' deve ser armazenado como variável da classe, para evitar repetição da operação
        if (shootTimer > 2 * (1 / fireRate)) // para corrigir sincronia entre os tiros
        {
            shootTimer = 2 * (1 / fireRate) + 0.000001f; // soma valor pequeno para evitar que resultado seja menor (ponto flutuante)
        }
    }

    // Instancia (dispara) um projétil, com parâmetros conforme especificação da arma
    protected virtual void InstantiateProjectile ()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.GetComponent<ProjectileBHV>().Shoot(projectileSpeed, projectileAcceleration, intensityMult, tagsToHit);
    }
}
