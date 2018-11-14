using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class defines the base for ship equipments, such as generators and weapons.
/// </summary>
[RequireComponent(typeof(ShipBHV))]
public abstract class EquipmentBHV : MonoBehaviour
{
    [Header("Equipment")]
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
        equipmentData.OnLevelUpdate +=OnUpgrade;
    }

    protected void OnDisable()
    {
        if (equipmentData == null)
        {
            return;
        }
        equipmentData.OnLevelUpdate -= OnUpgrade;
    }

    /// <summary>
    /// This method is called whenever the EquipmentSO attribute data is upgraded, provided the listener is correctly set up.
    /// </summary>
    private void OnUpgrade()
    {
        UpdateEquipmentAttributes();
        Debug.Log("Called UpdateEquip from OnUpgrade for " + equipmentData.name);
    }
    /// <summary>
    /// Gets upgrade cost for the next level of this equipment.
    /// </summary>
    /// <returns>Returns the cost value.</returns>
    public virtual float GetUpgradeCost()
    {
        return equipmentData.GetUpgradeCost();
    }

    /// <summary>
    /// Updates this monobehaviour's attributes to correspond to the respective level of the attribute data.
    /// </summary>
    protected abstract void UpdateEquipmentAttributes();

}
