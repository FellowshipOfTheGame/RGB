using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Component for objects that can be damaged.
/// </summary>
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

    /// <summary>
    /// Adds damage to the HealthBHV component.
    /// </summary>
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

    // Define o comportamento de morte do objeto
    private void Kill ()
    {
        // animãção, etc
        Destroy(gameObject);
    }
}
