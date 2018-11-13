using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGeneratorBHV : EquipmentBHV
{
    [SerializeField]
    private float maxCapacity;
    [SerializeField]
    private float energy;
    [SerializeField]
    private float power;

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
