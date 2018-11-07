using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAimPlayerBHV : MonoBehaviour
{
    private GameObject player;
    private GameObject weapons;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        weapons = transform.GetChild(0).gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            weapons.transform.up = player.transform.position - transform.position;
        }
    }
}
