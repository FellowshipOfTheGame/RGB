using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScreen : MonoBehaviour
{
    public UIScroller shipScroller;
    public UIScroller equipmentScroller;
    public JointShipController shipController;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            equipmentScroller.transform.GetChild(equipmentScroller.currentIndex).GetComponent<UIEqUpgrader>().Upgrade(shipController.ships[shipScroller.currentIndex]);
        }
    }

    private void SetUpgrade(int shipIndex, int equipmentIndex)
    {
        equipmentScroller.transform.GetChild(equipmentIndex);
    }
}
