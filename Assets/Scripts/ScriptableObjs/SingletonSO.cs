using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
                if (!_instance)
                {
                    Debug.LogError("Instance of " + typeof(T) + " could not be loaded.");
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
