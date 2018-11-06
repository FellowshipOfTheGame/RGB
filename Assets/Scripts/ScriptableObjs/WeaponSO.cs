using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponSO : ScriptableObject
{
    [System.Serializable]
    public struct WeaponProperty
    {
        public float baseValue;
        public AnimationCurve curve;
    }

    public int level = 1;
    public WeaponProperty fireRate;
    public WeaponProperty intensityMult; // Multiplicador de intensidade: dano/raio
    public WeaponProperty drainedPower; // Energia drenada do gerador
    public WeaponProperty projectileSpeed;
    public WeaponProperty projectileAcceleration;

    public void LoadToWeapon (WeaponBHV weapon)
    {
        weapon.fireRate = FireRate(level);
        weapon.intensityMult = IntensityMult(level);
        weapon.drainedPower = DrainedPower(level);
        weapon.projectileSpeed = ProjectileSpeed(level);
        weapon.projectileAcceleration = ProjectileAcceleration(level);
    }

    private float FireRate(float level)
    {
        return fireRate.curve.Evaluate(level) * fireRate.baseValue;
    }

    private float IntensityMult (float xPoint)
    {
        return intensityMult.curve.Evaluate(level) * intensityMult.baseValue;
    }

    private float DrainedPower (float xPoint)
    {
        return drainedPower.curve.Evaluate(level) * drainedPower.baseValue;
    }

    private float ProjectileSpeed (float xPoint)
    {
        return projectileSpeed.curve.Evaluate(level) * projectileSpeed.baseValue;
    }

    private float ProjectileAcceleration (float xPoint)
    {
        return projectileAcceleration.curve.Evaluate(level) * projectileAcceleration.baseValue;
    }
}
