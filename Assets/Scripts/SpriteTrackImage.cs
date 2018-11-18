using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteTrackImage : MonoBehaviour
{
    public float duration;
    public float initialAplha;
    private SpriteRenderer spriteR;
    private float timer = 0;
    

    private void Awake()
    {
        spriteR = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Color c = spriteR.color;
        c.a = initialAplha - initialAplha * (timer / duration);
        spriteR.color = c;
    }
}
