using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthBHV))]
public class ProjectileBHV : MonoBehaviour
{
    public float lifetime = 20;
    public bool destroyOnImpact = true;
    protected Rigidbody2D rigidBody;
    protected HealthBHV health;
    protected float speed;
    protected float acceleration;
    public float baseDamage = 1;
    protected float damage;
    protected float intensityMult;
    protected float creationTime;
    protected List<string> tagsToHit = null;
    protected StatusEffect[] statusEffects;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        statusEffects = GetComponents<StatusEffect>();
    }
    // Start is called before the first frame update
    void Start()
    {
        creationTime = Time.time;
        rigidBody.velocity += (Vector2)transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - creationTime > lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rigidBody.velocity += (Vector2)transform.up * acceleration;
    }

    public void Shoot (float speed, float acceleration, float intensityMult, List<string> tags)
    {
        this.speed = speed;
        this.acceleration = acceleration;
        this.tagsToHit = tags;
        this.intensityMult = intensityMult;
        damage = baseDamage * intensityMult;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tagsToHit == null)
        {

        }
        else if (tagsToHit.Contains(collision.tag) )
        {
            Impact(collision);
        }
    }

    protected virtual void Impact (Collider2D other = null)
    {
        if (destroyOnImpact)
        {
            Destroy(gameObject);
        }
        if (other != null)
        {
            HealthBHV otherHealth = other.GetComponent<HealthBHV>();
            if (otherHealth != null)
            {
                otherHealth.TakeDamage(damage);
            }
            ApplyStatusEffects (other);
        }
    }

    private void EndEffect()
    {
        Destroy(gameObject);
    }

    private void ApplyStatusEffects(Collider2D other)
    {
        for (int i = 0; i < statusEffects.Length; i++)
        {
            statusEffects[i].Effect(other.gameObject);
        }
    }


}
