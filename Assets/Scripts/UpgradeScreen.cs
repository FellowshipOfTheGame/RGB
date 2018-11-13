using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScreen : MonoBehaviour
{
    public UIScroller shipScroller;
    public UIScroller[] equipmentScrollers;
    public JointShipController shipController;



    // Start is called before the first frame update
    void Start()
    {
        shipScroller.OnIndexChange += OnShipIndexChange;
        OnShipIndexChange(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Player.Instance.Upgrade(Player.Instance.inventories[shipScroller.currentIndex].equipments[equipmentScrollers[shipScroller.currentIndex].currentIndex]);
            equipmentScrollers[shipScroller.currentIndex].transform.GetChild(equipmentScrollers[shipScroller.currentIndex].currentIndex).GetComponent<UIEqUpgrader>().equipmentData.Upgrade();
        }
    }

    private void SetUpgrade(int shipIndex, int equipmentIndex)
    {
        equipmentScrollers[shipScroller.currentIndex].transform.GetChild(equipmentIndex);
    }

    private void OnShipIndexChange(int indexFrom)
    {
        // Ativa apenas os equipamentos da nave selecionada
        for (int i = 0; i < equipmentScrollers.Length; i++)
        {
            if (i == shipScroller.currentIndex)
            {
                equipmentScrollers[i].gameObject.SetActive(true);
                equipmentScrollers[i].currentIndex = equipmentScrollers[indexFrom].currentIndex;
            }
            else
            {
                equipmentScrollers[i].gameObject.SetActive(false);
            }
        }

    }
}
