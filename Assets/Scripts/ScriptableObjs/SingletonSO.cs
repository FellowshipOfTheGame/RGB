using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// To load resources by default (so that they can be found): Edit -> Project Settings -> Player -> (Tab) Other Settings -> Preloaded Assets
/// </summary>
/// <typeparam name="T"></typeparam>

public abstract class SingletonSO<T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
                
                //if (!_instance) _instance = Resources.LoadAll<T>("Assets/Data/Config").FirstOrDefault();
                if (!_instance)
                {
                    Debug.LogError("Instance of " + typeof(T) + " could not be loaded.");
                    FindObjectOfType<Text>().text = "" + typeof(T); //FIXME: debughack
                }
                else
                {
                    Debug.Log("Singleton created. Typeof: " + typeof(T).ToString());
                }
            }
            return _instance;
        }
        protected set
        {
            _instance = value;
        }
    }

    private void Awake()
    {
        Debug.Log("Singleton " + typeof(T) + " awaking.");
        //Instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
        //if (Instance != null)
        //{
        //    if (Instance != this)
        //    {
        //        DestroyImmediate(this);
        //    }
        //}
        //else
        //{
        //    Instance = this;
        //}
    }
}
