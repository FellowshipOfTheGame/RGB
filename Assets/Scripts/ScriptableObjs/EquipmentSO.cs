using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Base class for ship equipment data.
/// </summary>
[System.Serializable]
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

    [Header("Children Equipments")]
    public List<EquipmentSO> childrenEquipments;

    [Header("Settings")]
    [SerializeField]
    private int level = 1;
    public int maxLevel = 10;
    [SerializeField]
    private EquipmentProperty upgradeCost = new EquipmentProperty();


    public delegate void UpgradeDelegate ();
    public event UpgradeDelegate OnLevelUpdate;

    public int Level
    {
        get
        {
            return level;
        }
        protected set
        {
            level = Mathf.Min(value, maxLevel);
            OnLevelUpdate?.Invoke();
        }
    }

    public int GetUpgradeCost ()
    {
        float cost = upgradeCost.baseValue * upgradeCost.curve.Evaluate(level + 1);
        cost = (int)(cost / 100) * 100;
        return (int)cost;
    }

    public void Upgrade()
    {
        Level++;
        foreach (EquipmentSO c in childrenEquipments)
        {
            c.Upgrade();
        }
    }

    public void SetLevel (int level)
    {
        Level = level;
        foreach (EquipmentSO c in childrenEquipments)
        {
            c.SetLevel(level);
        }
    }

}
