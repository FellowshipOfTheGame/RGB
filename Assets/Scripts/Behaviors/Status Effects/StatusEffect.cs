using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    public float duration = 1.0f;
    public float intensity = 1.0f;
    //public bool permanent = false;
    public StatusEffectSO statusEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Effect(GameObject other) {
        ShipBHV ship = other.GetComponent<ShipBHV>();
        if (ship != null)
        {
            ship.AddEffect(statusEffect, duration, intensity);
        }
    }

}
