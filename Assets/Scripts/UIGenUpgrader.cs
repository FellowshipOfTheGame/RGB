using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIGenUpgrader : UIEqUpgrader
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnergyGeneratorBHV generator = shipController.ships[shipScroller.currentIndex].GetComponent<EnergyGeneratorBHV>();
        int c = (int)(generator.baseCost * generator.costCurve.Evaluate(generator.level + 1));
        title.text = eqDescription + generator.level;
        string s = System.String.Format("{0:n0}", c);
        cost.text = "$" + s;
    }

    public override void Upgrade(ShipBHV ship)
    {
        EnergyGeneratorBHV generator = ship.GetComponent<EnergyGeneratorBHV>();
        int cost = (int)(generator.baseCost * generator.costCurve.Evaluate (generator.level + 1));
        if (shipController.money >= cost);
        {
            shipController.money -= cost;
            generator.level++;
        }

    }
}
