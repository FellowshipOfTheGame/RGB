using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentSO : ScriptableObject
{
    [System.Serializable]
    public struct EquipmentProperty
    {
        public float baseValue;
        public AnimationCurve curve;
    }

    [Header("Description")]
    public string equipmentName;
    public Sprite sprite;

    [Header("Settings")]
    public int level = 1;
    public int maxLevel = 10;
    public EquipmentProperty upgradeCost;

    

    public delegate void UpgradeDelegate ();
    public event UpgradeDelegate OnUpgrade;

    public float GetUpgradeCost ()
    {
        return upgradeCost.baseValue * upgradeCost.curve.Evaluate(level + 1);
    }

    public virtual void Upgrade()
    {
        level++;
        if (level > maxLevel)
        {
            level = maxLevel;
        }
        if (OnUpgrade == null)
        {
            Debug.Log("Delegate null");
        } else
        {
            OnUpgrade();
        }
    }

}
