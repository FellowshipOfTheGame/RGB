using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGeneratorBHV : MonoBehaviour
{
    public int level;
    public float baseCapacity;
    public AnimationCurve capacityCurve;
    public float basePower;
    public AnimationCurve powerCurve;
    public float baseCost;
    public AnimationCurve costCurve;

    [SerializeField]
    private float maxCapacity;
    [SerializeField]
    private float energy;
    [SerializeField]
    private float power;

    // Start is called before the first frame update
    void Start()
    {
        maxCapacity = capacityCurve.Evaluate(level) * baseCapacity;
        energy = maxCapacity;
        power = powerCurve.Evaluate(level) * basePower;
    }

    // Update is called once per frame
    void Update()
    {
        maxCapacity = capacityCurve.Evaluate(level) * baseCapacity; // FIXME: remove - debug only
        power = powerCurve.Evaluate(level) * basePower; // FIXME: remove - debug only
        energy += power;
        if (energy > maxCapacity)
        {
            energy = maxCapacity;
        }
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
