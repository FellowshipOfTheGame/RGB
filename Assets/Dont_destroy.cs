using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dont_destroy : MonoBehaviour
{
    void Awake()
    {
        if(SceneManager.GetActiveScene().name=="BattleFinal")
            DontDestroyOnLoad(this.gameObject);
    }
}
