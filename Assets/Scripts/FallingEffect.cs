using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEffect : MonoBehaviour
{

    public float fallingSpeed;
    [SerializeField]
    private float destroyTreshold;

    // Update is called once per frame
    void Update()
    {

        transform.position += Vector3.down * fallingSpeed;
        if (transform.position.y < destroyTreshold) Destroy(gameObject);
    }
}
