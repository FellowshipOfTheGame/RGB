using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteTrackImage : MonoBehaviour
{
    public float duration;
    [Range(0, 1)]
    public float initialAplha;
    [Range(0, 1)]
    public float finalAlpha = 0;
    public bool resetOnEnable = true;
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

    private void OnEnable()
    {
        if (resetOnEnable)
        {
            Color c = spriteR.color;
            c.a = initialAplha;
            spriteR.color = c;
            timer = 0;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Color c = spriteR.color;
        c.a = initialAplha - (initialAplha - finalAlpha) * (timer / duration);
        spriteR.color = c;
    }
}
