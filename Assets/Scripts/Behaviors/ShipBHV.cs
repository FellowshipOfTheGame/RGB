using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Sprite))] // Obriga o objeto a ter uma sprite
[RequireComponent(typeof(HealthBHV))]
[RequireComponent(typeof(EnergyGeneratorBHV))]
//[RequireComponent(typeof(ShipController))] // Obriga a nave a ter um controller (seja de player ou enemy)

public class ShipBHV : MonoBehaviour
{
    public float speed;
    public float impactDamage = 1f;
    [TagSelector]
    public List<string> tagsToImpact = new List<string>();

    public List<WeaponBHV> weapon1 = new List<WeaponBHV>();

    private EnergyGeneratorBHV generator;

    private void Awake()
    {
        generator = GetComponent<EnergyGeneratorBHV>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //transform.position += transform.up * speed;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tagsToImpact.Contains(collision.tag)) {
            HealthBHV other = collision.GetComponent<HealthBHV>();
            if (other != null)
            {
                other.TakeDamage(impactDamage);
            }
        }
    }

}
