using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{

    public float scrollSpeed;
    public float tileSizeY;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeY);
        transform.position = startPosition + Vector3.down * newPosition;
    }
}
