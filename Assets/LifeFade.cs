using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthBHV))]
[RequireComponent(typeof(SpriteRenderer))]
public class LifeFade : MonoBehaviour
{
    private SpriteRenderer sprite;
    private HealthBHV health;
    private float startHealth;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        health = GetComponent<HealthBHV>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startHealth = health.health;
    }

    // Update is called once per frame
    void Update()
    {
        if (health.health > 0)
        {
            Color c = sprite.color;
            c.a = health.health / startHealth;
            sprite.color = c;
        }
    }

}
