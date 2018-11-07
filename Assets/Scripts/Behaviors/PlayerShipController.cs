using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DEPRECATED. USE ShipController BEHAVIOR INSTEAD

[RequireComponent(typeof(ShipBHV))]
public class PlayerShipController : MonoBehaviour
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
        if (Input.GetKey(KeyCode.Space))
        {
            ship.Fire1();
        }
        //transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * ship.speed;
    }
}
