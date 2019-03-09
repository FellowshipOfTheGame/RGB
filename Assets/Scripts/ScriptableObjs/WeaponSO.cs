using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Weapon-specific equipment data.
/// </summary>
[CreateAssetMenu]
[System.Serializable]
public class WeaponSO : EquipmentSO
{
    public EquipmentProperty fireRate;
    public EquipmentProperty intensityMult; // Multiplicador de intensidade: dano/raio
    public EquipmentProperty drainedPower; // Energia drenada do gerador
    public EquipmentProperty projectileSpeed;
    public EquipmentProperty projectileAcceleration;

    public float FireRate ()
    {
        return fireRate.curve.Evaluate(Level) * fireRate.baseValue;
    }

    public float IntensityMult ()
    {
        return intensityMult.curve.Evaluate(Level) * intensityMult.baseValue;
    }

    public float DrainedPower ()
    {
        return drainedPower.curve.Evaluate(Level) * drainedPower.baseValue;
    }

    public float ProjectileSpeed ()
    {
        return projectileSpeed.curve.Evaluate(Level) * projectileSpeed.baseValue;
    }

    public float ProjectileAcceleration ()
    {
        return projectileAcceleration.curve.Evaluate(Level) * projectileAcceleration.baseValue;
    }

}
