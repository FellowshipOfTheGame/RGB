using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceObjBHV : MonoBehaviour
{
    /// <summary>
    /// Have a Status Effect added to the ship.
    /// </summary>
    /// <param name="statusEffect">A status effect data which describes the status behavior.</param>
    /// <param name="duration">Duration, in seconds, of the added status effect.</param>
    /// <param name="intensity">Intensity of the effect.</param>
    public void AddEffect(StatusEffectSO statusEffect, float duration, float intensity)
    {
        // TODO: Debug to check if problems won't arise from destroying object before coroutine is over
        StartCoroutine(statusEffect.RunEffect(gameObject, duration, intensity));
    }
}
