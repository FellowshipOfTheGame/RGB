using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This component will define time-dependent, auto-scaling of a game object.
/// </summary>
public class AutoExpandBHV : MonoBehaviour
{
    public enum Mode
    {
        SUM,
        MULTIPLY
    }
    [Header("Expand Rate")]
    public float expandRateX = 1.02f;
    public float expandRateY = 1.02f;
    [Header("Max Scale")]
    public float maxScaleX = 10.0f;
    public float maxScaleY = 10.0f;
    //TODO: define minScale, to allow for scale contractions
    [Header("Mode")]
    [Tooltip("Determines if the rate will be added or multiplied to the current scale.")]
    public Mode expandMode = Mode.MULTIPLY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x > maxScaleX || transform.localScale.y > maxScaleY)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        switch (expandMode) {
            case Mode.SUM:
                transform.localScale += new Vector3(expandRateX, expandRateY, 0f);
                break;
            case Mode.MULTIPLY:
                transform.localScale = new Vector2(expandRateX*transform.localScale.x, expandRateY*transform.localScale.y);
                break;
        }
    }
}
