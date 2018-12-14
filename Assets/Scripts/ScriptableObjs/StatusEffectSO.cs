using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Defines Status Effect types and their respective IEnumerators for coroutines.
/// </summary>

// TODO: verify - shouldn't this code be in a monobehaviour instead? Or must the IEnumerators be in a persistent object?
[CreateAssetMenu(fileName ="StatusEffect", menuName = "StatusEffect")]
public class StatusEffectSO : ScriptableObject
{
    public enum EffectType
    {
        SLOWDOWN
    }

    public EffectType type;

    /// <summary>
    /// Defines coroutine to run Status Effects on the target game object.
    /// </summary>
    /// <param name="other">Target game object.</param>
    /// <param name="duration">Effect duration.</param>
    /// <param name="intensity">Effect intensity modifier.</param>
    /// <returns></returns>
    public IEnumerator RunEffect(GameObject other, float duration, float intensity)
    {
        switch (type)
        {
            case EffectType.SLOWDOWN:
                //START EFFECT
                foreach (WeaponBHV w in other.GetComponent<ShipBHV>().weapon1)
                {
                    Debug.Log("Slowing Weapon");
                    w.fireRate *= intensity;
                }
                if (other.GetComponent<ProjectileBHV>() != null)
                {
                    other.GetComponent<ProjectileBHV>().Speed *= intensity;
                }
                //WAIT
                yield return new WaitForSeconds(duration);
                //CEASE EFFECT
                Debug.Log("Slowing Time Expired");
                foreach (WeaponBHV w in other.GetComponent<ShipBHV>().weapon1)
                {
                    Debug.Log("De-Slowing Weapon");
                    w.fireRate /= intensity;
                }
                if (other.GetComponent<ProjectileBHV>() != null)
                {
                    other.GetComponent<ProjectileBHV>().Speed /= intensity;
                }

                break;
            default:
                break;
        }
        
    }
}
