using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBHV : MonoBehaviour
{
    public float lifetime = 20;
    public bool destroyOnImpact = true;
    protected Rigidbody2D rigidBody;
    protected float speed;
    protected float acceleration;
    protected float creationTime;
    protected List<string> tagsToHit = null;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        creationTime = Time.time;
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
        rigidBody.velocity = Vector2.up * speed;
        speed += acceleration;
    }

    public void Shoot (float speed, float acceleration, List<string> tags)
    {
        this.speed = speed;
        this.acceleration = acceleration;
        this.tagsToHit = tags;
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
    }

    private void EndEffect()
    {
        Destroy(gameObject);
    }

}
