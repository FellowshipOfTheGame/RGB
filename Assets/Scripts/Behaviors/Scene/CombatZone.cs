using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatZone : MonoBehaviour
{
    public bool destroyAll = true;
    public bool destroyShip = false;
    public bool destroyProjectile = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool destroy = false;
        if (destroyAll)
        {
            destroy = true;
        }
        else
        {
            if (destroyShip && collision.GetComponent<ShipBHV>() != null)
            {
                destroy = true;
            }
            if (destroyProjectile && collision.GetComponent<ProjectileBHV>() != null)
            {
                destroy = true;
            }
        }
        if (destroy)
        {
            Destroy(collision.gameObject);
        }
    }
}
