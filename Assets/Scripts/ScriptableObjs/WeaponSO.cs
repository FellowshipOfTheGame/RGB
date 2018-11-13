using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponSO : EquipmentSO
{
    
    public EquipmentProperty fireRate;
    public EquipmentProperty intensityMult; // Multiplicador de intensidade: dano/raio
    public EquipmentProperty drainedPower; // Energia drenada do gerador
    public EquipmentProperty projectileSpeed;
    public EquipmentProperty projectileAcceleration;

    public WeaponSO childWeapon;

    public float FireRate ()
    {
        return fireRate.curve.Evaluate(level) * fireRate.baseValue;
    }

    public float IntensityMult ()
    {
        return intensityMult.curve.Evaluate(level) * intensityMult.baseValue;
    }

    public float DrainedPower ()
    {
        return drainedPower.curve.Evaluate(level) * drainedPower.baseValue;
    }

    public float ProjectileSpeed ()
    {
        return projectileSpeed.curve.Evaluate(level) * projectileSpeed.baseValue;
    }

    public float ProjectileAcceleration ()
    {
        return projectileAcceleration.curve.Evaluate(level) * projectileAcceleration.baseValue;
    }

    public override void Upgrade ()
    {
        base.Upgrade();
        if (childWeapon != null)
        {
            childWeapon.level = level;
        }
    }
}
