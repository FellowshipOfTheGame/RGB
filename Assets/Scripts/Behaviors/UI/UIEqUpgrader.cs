using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Handles upgrade UI for each individual equipment data.
/// </summary>
public class UIEqUpgrader : MonoBehaviour
{
    [Header("Equipment")]
    public EquipmentSO equipmentData;
    [Header("Internal References")]
    public Text title;
    public Text cost;
    public Image image;

    private void Awake()
    {
        transform.parent.GetComponent<UIScroller>().OnIndexChange += OnIndexChange;
        if (equipmentData != null) equipmentData.OnLevelUpdate += OnUpgrade;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (equipmentData != null) image.sprite = equipmentData.sprite;
        UpdateDescription();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        transform.parent.GetComponent<UIScroller>().OnIndexChange -= OnIndexChange;
        if (equipmentData != null) equipmentData.OnLevelUpdate -= OnUpgrade;
    }

    private void UpdateDescription ()
    {
        if (equipmentData == null)
        {
            return;
        }
        title.text = equipmentData.equipmentName + " Level " + equipmentData.Level;
        if (equipmentData.Level < equipmentData.maxLevel)
        {
            string s = System.String.Format("{0:n0}", (equipmentData.GetUpgradeCost()));
            cost.text = "$ " + s;
        }
        else
        {
            cost.text = "MAXED!";
        }

    }

    private void OnIndexChange (int indexFrom = 0)
    {
        UpdateDescription();
    }

    private void OnUpgrade()
    {
        UpdateDescription();
    }
}
