using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIEqUpgrader : MonoBehaviour
{
    public UIScroller shipScroller;
    public JointShipController shipController;
    public string eqDescription;
    public Text title;
    public Text cost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Confirm ()
    {
        ShipBHV ship = shipController.ships[shipScroller.currentIndex];
        Upgrade(ship);
    }

    public abstract void Upgrade(ShipBHV ship);
}
