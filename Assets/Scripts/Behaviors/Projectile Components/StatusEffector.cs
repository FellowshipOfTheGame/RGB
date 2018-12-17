using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component defines the methods and attributes to add a StatusEffect to a target.
/// </summary>
public class StatusEffector : MonoBehaviour
{
    [Header("Status Effect")]
    public StatusEffectSO statusEffect;
    [Header("Properties")]
    public float duration = 1.0f;
    public float intensity = 1.0f;

    //public bool permanent = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Adds StatusEffect defined by the statusEffect property to the target.
    /// </summary>
    /// <param name="other">Target game object.</param>
    public void Effect(GameObject other) {
        if (enabled == false)
        {
            return;
        }
        SpaceObjBHV spaceObj = other.GetComponent<SpaceObjBHV>();
        if (spaceObj != null)
        {
            spaceObj.AddEffect(statusEffect, duration, intensity);
        }
    }

}
