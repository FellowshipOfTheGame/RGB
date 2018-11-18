using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base component for a ship gameObject.
/// </summary>


[RequireComponent(typeof(Sprite))] // Obriga ship a ter componente
[RequireComponent(typeof(HealthBHV))] // Obriga ship a ter componente
[RequireComponent(typeof(EnergyGeneratorBHV))] // Obriga ship a ter componente
//[RequireComponent(typeof(ShipController))] // Obriga a nave a ter um controller (seja de player ou enemy)

public class ShipBHV : MonoBehaviour
{
    [Header("Ship")]
    public float speed;
    public float impactDamage = 1f;
    [TagSelector]
    public List<string> tagsToImpact = new List<string>();

    [Header("Weapons")]
    public List<WeaponBHV> weapon1 = new List<WeaponBHV>();

    private EnergyGeneratorBHV generator;

    private void Awake()
    {
        generator = GetComponent<EnergyGeneratorBHV>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Starts ship with initial velocity
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Tries to fire with main weapon or set of weapons.
    /// </summary>
    public void Fire1 () // Fires with main weapon
    {
        foreach(WeaponBHV w in weapon1)
        {
            w.Fire();
        }
    }
    /// <summary>
    /// Have a Status Effect added to the ship.
    /// </summary>
    /// <param name="statusEffect">A status effect data which describes the status behavior.</param>
    /// <param name="duration">Duration, in seconds, of the added status effect.</param>
    /// <param name="intensity">Intensity of the effect.</param>
    public void AddEffect(StatusEffectSO statusEffect, float duration, float intensity)
    {
        // TODO: Debug to check if problems won't arise from destroying object before coroutine is over
        StartCoroutine(statusEffect.RunEffect (gameObject, duration, intensity));
    }

    // Called when ship collides
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // On Impact, deals damage to the other object
        if (tagsToImpact.Contains(collision.tag)) {
            HealthBHV other = collision.GetComponent<HealthBHV>();
            if (other != null)
            {
                other.TakeDamage(impactDamage);
            }
        }
    }

}
