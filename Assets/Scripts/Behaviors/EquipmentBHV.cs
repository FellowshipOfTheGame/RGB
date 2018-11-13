using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentBHV : MonoBehaviour
{
    public EquipmentSO equipmentData;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnEnable()
    { 
        if (equipmentData == null) {
            return;
        }
        UpdateEquipmentAttributes();
        equipmentData.OnUpgrade +=OnUpgrade;
    }

    protected void OnDisable()
    {
        if (equipmentData == null)
        {
            return;
        }
        equipmentData.OnUpgrade -= OnUpgrade;
    }

    protected virtual void OnUpgrade()
    {
        UpdateEquipmentAttributes();
    }

    public virtual float GetUpgradeCost()
    {
        return equipmentData.GetUpgradeCost();
    }

    public virtual void Upgrade()
    {

    }

    protected abstract void UpdateEquipmentAttributes();

}
