using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="StatusEffect", menuName = "StatusEffect")]
public class StatusEffectSO : ScriptableObject
{
    public enum EffectType
    {
        SLOWDOWN
    }

    public EffectType type;

    public IEnumerator RunEffect(GameObject other, float duration, float intensity)
    {
        switch (type)
        {
            case EffectType.SLOWDOWN:
                foreach (WeaponBHV w in other.GetComponent<ShipBHV>().weapon1)
                {
                    Debug.Log("Slowing Weapon");
                    w.fireRate *= intensity;
                }
                yield return new WaitForSeconds(duration);
                Debug.Log("Slowing Time Expired");
                foreach (WeaponBHV w in other.GetComponent<ShipBHV>().weapon1)
                {
                    Debug.Log("De-Slowing Weapon");
                    w.fireRate /= intensity;
                }
                break;
            default:
                break;
        }
        
    }
}
