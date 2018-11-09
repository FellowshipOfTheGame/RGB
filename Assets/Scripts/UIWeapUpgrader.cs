using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIWeapUpgrader : UIEqUpgrader
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WeaponSO weapon = shipController.ships[shipScroller.currentIndex].weapon1[0].weaponSettings;
        title.text = eqDescription + weapon.level;
        string s = String.Format("{0:n0}", (weapon.upgradeCost.baseValue * weapon.upgradeCost.curve.Evaluate(weapon.level + 1)));
        cost.text = "$" + s;
    }

    public override void Upgrade(ShipBHV ship)
    {
        WeaponSO weapon = ship.GetComponentInChildren<WeaponBHV>().weaponSettings;
        int cost = (int)(weapon.upgradeCost.baseValue * weapon.upgradeCost.curve.Evaluate(weapon.level + 1));
        if (shipController.money >= cost) ;
        {
            shipController.money -= cost;
            weapon.Upgrade();
        }
    }
}
