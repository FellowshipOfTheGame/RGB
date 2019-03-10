using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the base behavior of a projectile. It can be used with 0 damage and a StatusEffector, as a power-up (buff/debuff).
/// </summary>
[RequireComponent(typeof(HealthBHV))]
public class ProjectileBHV : SpaceObjBHV
{
    [Tooltip("Time, in seconds, until it expires.")]
    public float lifetime = 20;
    [Tooltip("Whether it is destroyed by any effective collision.")]
    public bool destroyOnImpact = true;
    [Tooltip("Base damage of the projectile. It's effective damage takes into account the weapon damage.")]
    public float baseDamage = 1;


    protected float speed;
    public float Speed
    {
        get => speed;
        set => speed = value;
    }
    protected float acceleration;
    protected float damage;
    protected float intensityMult;
    protected float creationTime;
    protected List<string> tagsToHit = null;
    public List<string> TagsToHit
    {
        get => tagsToHit;
        protected set => tagsToHit = value;
    }

    // Components
    protected Rigidbody2D rigidBody;
    protected HealthBHV health;
    protected StatusEffector[] statusEffectors;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        statusEffectors = GetComponents<StatusEffector>();
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

    /// <summary>
    /// Sets projectile attributes so it can behave correctly on instantiation.
    /// </summary>
    /// <param name="speed">Initial speed in the direction of the transform.up.</param>
    /// <param name="acceleration">Acceleration in the direction of the transform.up.</param>
    /// <param name="intensityMult">Value for the base damage to be multiplied by.</param>
    /// <param name="tags">Tags taken into account when checking for collisions.</param>
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

    /// <summary>
    /// Specifies the behavior after a successful collision.
    /// </summary>
    protected virtual void Impact (Collider2D other = null)
    {
        int shipColor;
        if (destroyOnImpact)
        {
            Destroy(gameObject);
        }
        if (other != null)
        {
            HealthBHV otherHealth = other.GetComponent<HealthBHV>();
            if (otherHealth != null)
            {
                if (other.tag == "Enemy")
                {
                    if (this.name.Contains("Att"))
                        shipColor = 0;
                    else if (this.name.Contains("Def"))
                        shipColor = 1;
                    else
                        shipColor = 2;
                    otherHealth.TakeDamage(damage, shipColor);
                }
                else
                    otherHealth.TakeDamage(damage);
            }
            ApplyStatusEffects (other);
        }
    }

    /// <summary>
    /// Applies any status effects if the projectile has StatusEffector components.
    /// </summary>
    private void ApplyStatusEffects(Collider2D other)
    {
        for (int i = 0; i < statusEffectors.Length; i++)
        {
            statusEffectors[i].Effect(other.gameObject);
        }
    }

    /// <summary>
    /// Terminates the object.
    /// </summary
    public void EndEffect()
    {
        Destroy(gameObject);
    }

}
