using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoExpandBHV : MonoBehaviour
{
    public enum Mode
    {
        SUM,
        MULTIPLY
    }

    public float expandRateX = 1.02f;
    public float expandRateY = 1.02f;
    public float maxScaleX = 10.0f;
    public float maxScaleY = 10.0f;
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
