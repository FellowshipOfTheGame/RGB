using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EnergyDisplay : MonoBehaviour
{

    public Gradient gradient;
    public Color deactivatedColor;
    public EnergyGeneratorBHV generator;


    [SerializeField] // For debug inspection
    private float rectHeight = 0;
    private float lastRectHeight = -1;
    [SerializeField] // For debug inspection
    private List<Image> energySticks = null;
    [SerializeField] // For debug inspection
    private List<float> stickHeights = new List<float>();
    private List<Color> stickColors = new List<Color>();


    // Start is called before the first frame update
    void Start()
    {
        energySticks = (transform.GetComponentsInChildren<Image>()).ToList();
        //Invoke("SetupColors", 2*Time.deltaTime);
        StartCoroutine(SetupAfterEndOfFrame());
    }

    void SetupColors()
    {
        foreach (Image img in energySticks)
        {
            float height = (img.rectTransform.localPosition.y + rectHeight / 2) / rectHeight;
            if (height > 1) height = 1;
            if (height < 0) height = 0;
            stickHeights.Add(height);
            stickColors.Add(gradient.Evaluate(height));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //if (lastRectHeight != rectHeight)
        //{
        //    lastRectHeight = rectHeight;
        //    rectHeight = GetComponent<RectTransform>().rect.size.y;
        //    if (rectHeight == lastRectHeight)
        //    {
        //        SetupColors();
        //    }
        //}
        
        if (generator == null)
        {
            for (int i = 0; i < stickHeights.Count; i++)
            {
                energySticks[i].color = deactivatedColor;
            }
            return;
        }

        float energyLevel = generator.energy / 370;//generator.maxCapacity;
        for (int i = 0; i < stickHeights.Count; i++)
        {
            if (energyLevel >= stickHeights[i])
            {
                energySticks[i].color = stickColors[i];
            }
            else
            {
                energySticks[i].color = deactivatedColor;
            }
        }
    }

    IEnumerator SetupAfterEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        rectHeight = GetComponent<RectTransform>().rect.size.y;
        SetupColors();
    }

}
