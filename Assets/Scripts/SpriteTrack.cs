using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteTrack : MonoBehaviour
{
    public float duration = 0;
    public float imageDuration = 0.2f;
    public float frequency = 5f;
    [Range(0, 1)]
    public float initialAlpha = 1.0f;
    public float zPositionOffset = 0f;

    private SpriteRenderer spriteRenderer;
    private float timer = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1 / frequency)
        {
            timer = 0;
            GameObject g = new GameObject();
            g.name = "Track";
            g.transform.position = transform.position + Vector3.forward * zPositionOffset;
            g.transform.rotation = transform.rotation;
            g.transform.localScale = transform.lossyScale;
            g.AddComponent<SpriteRenderer>();
            g.GetComponent<SpriteRenderer>().sprite = spriteRenderer.sprite;
            g.AddComponent<SpriteTrackImage>();
            g.GetComponent<SpriteTrackImage>().duration = imageDuration;
            g.GetComponent<SpriteTrackImage>().initialAplha = initialAlpha;
            Destroy(g, imageDuration);
        }
    }

    private void OnEnable()
    {
        if (duration > 0)
        {
            Invoke("Disable", duration);
        }
    }

    private void Disable()
    {
        enabled = false;
    }

}
