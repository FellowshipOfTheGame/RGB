using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component defines the behavior and actions of a non-player-controlled ship.
/// </summary>
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
        ship.Fire1(); // tries to shoot
    }
}
