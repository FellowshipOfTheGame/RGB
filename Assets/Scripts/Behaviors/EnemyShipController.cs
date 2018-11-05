using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShipBHV))]
public class EnemyShipController : MonoBehaviour
{
    private ShipBHV ship;
    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<ShipBHV>();
    }

    // Update is called once per frame
    void Update()
    {
        ship.Fire1();
    }
}
