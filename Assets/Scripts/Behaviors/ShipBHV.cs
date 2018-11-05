using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Sprite))] // Obriga o objeto a ter uma sprite
[RequireComponent(typeof(HealthBHV))]
//[RequireComponent(typeof(ShipController))] // Obriga a nave a ter um controller (seja de player ou enemy)

public class ShipBHV : MonoBehaviour
{
    public float speed;
    public List<WeaponBHV> weapon1 = new List<WeaponBHV>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire1 () // Fires with main weapon
    {
        foreach(WeaponBHV w in weapon1)
        {
            w.Fire();
        }
    }

    public void AddEffect(StatusEffectSO statusEffect, float duration, float intensity)
    {
        StartCoroutine(statusEffect.RunEffect (gameObject, duration, intensity));
    }

}
