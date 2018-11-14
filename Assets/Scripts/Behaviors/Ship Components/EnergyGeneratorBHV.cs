using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines specific behaviours for the equipment type: EnergyGenerator. They include values of 'power' and 'capacity'.
/// </summary>
public class EnergyGeneratorBHV : EquipmentBHV
{
    [Header("Attributes - alternative to data-defined")]
    public float maxCapacity;
    public float energy;
    public float power;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        energy = maxCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        //if (DemoManager.debugMode) UpdateEquipmentAttributes(); // FIXME: remove - debug only
        energy += power;
        if (energy > maxCapacity)
        {
            energy = maxCapacity;
        }
    }

    protected override void UpdateEquipmentAttributes()
    {
        if (equipmentData == null)
        {
            return;
        }
        maxCapacity = ((GeneratorSO)equipmentData).Capacity();
        power = ((GeneratorSO)equipmentData).Power();
    }

    /// <summary>
    /// Consumes energy from the generator.
    /// </summary>
    /// <param name="energyValue">Ammount of energy to be deducted from current value.</param>
    /// <returns>Returns whether that ammount of energy was avaiable or not.</returns>
    public bool Consume (float energyValue)
    {
        if (energyValue > energy)
        {
            return false;
        }
        energy -= energyValue;
        return true;
    }

}
