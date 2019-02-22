using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component adds an Autoaim behaviour that targets the player.
/// </summary>
public class WeaponAimPlayerBHV : MonoBehaviour
{
    private GameObject player;
    private GameObject weapons;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<ShipController>().gameObject; //FIXME: fix slow code
        weapons = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // TODO: use an event to listen to weapon Fire() and trigger aimming
            weapons.transform.up = player.transform.position - transform.position;
        }
    }
}
