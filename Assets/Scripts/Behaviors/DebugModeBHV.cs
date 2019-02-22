using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugModeBHV : MonoBehaviour
{
    private void Awake()
    {
        if (!GameConfig.Instance.debugMode)
        {
            gameObject.SetActive(false);
        }
    }
}
