using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Generator", menuName = "Generator")]
public class GeneratorSO : EquipmentSO
{
    public EquipmentProperty capacity;
    public EquipmentProperty power;

    public float Capacity ()
    {
        return capacity.baseValue * capacity.curve.Evaluate(level);
    }

    public float Power()
    {
        return power.baseValue * power.curve.Evaluate(level);
    }
}
