using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEqUpgrader : MonoBehaviour
{
    public EquipmentSO equipmentData;
    public Text title;
    public Text cost;
    public Image image;

    private void Awake()
    {
        transform.parent.GetComponent<UIScroller>().OnIndexChange += OnIndexChange;
        if (equipmentData != null) equipmentData.OnUpgrade += OnUpgrade;
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

    private void UpdateDescription ()
    {
        if (equipmentData == null)
        {
            return;
        }
        title.text = equipmentData.equipmentName + " Level " + equipmentData.level;
        if (equipmentData.level < equipmentData.maxLevel)
        {
            string s = System.String.Format("{0:n0}", (equipmentData.upgradeCost.baseValue * equipmentData.upgradeCost.curve.Evaluate(equipmentData.level + 1)));
            cost.text = "$" + s;
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
