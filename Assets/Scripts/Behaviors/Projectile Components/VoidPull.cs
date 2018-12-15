using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidPull : MonoBehaviour
{
    public float pullVelocity = 1;
    [Min(1.0f)]
    public float centerRadius = 4;
    public float centerRadiusLimit = 1;
    private ProjectileBHV projectile;

    private void Awake()
    {
        projectile = GetComponent<ProjectileBHV>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (projectile == null)
            return;
        if (projectile.TagsToHit.Contains (collision.tag))
        {
            Vector3 distance = (transform.position - collision.transform.position);
            float magnitude = Mathf.Max(distance.magnitude, centerRadiusLimit); //smoothes central point attraction
            collision.transform.position += distance.normalized / ( magnitude * magnitude + centerRadius) * pullVelocity;
        }
    }
}
