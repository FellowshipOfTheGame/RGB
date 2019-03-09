using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generator-specific equipment data.
/// </summary>
[CreateAssetMenu(fileName = "Generator", menuName = "Generator")]
[System.Serializable]
public class GeneratorSO : EquipmentSO
{
    public EquipmentProperty capacity;
    public EquipmentProperty power;

    public float Capacity ()
    {
        return capacity.baseValue * capacity.curve.Evaluate(Level);
    }

    public float Power()
    {
        return power.baseValue * power.curve.Evaluate(Level);
    }
}
