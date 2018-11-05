using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBHV : MonoBehaviour
{
    public float health = 1;
    public bool invincible = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TakeDamage (float damage)
    {
        if (invincible)
        {
            return false;
        }
        else
        {
            health -= damage;
            if (health <= 0)
            {
                Kill();
            }
            return true;
        }
    }

    private void Kill ()
    {
        // animãção, etc
        Destroy(gameObject);
    }
}
