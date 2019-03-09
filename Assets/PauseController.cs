using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
     public GameObject[] m_PauseItens;


    // Start is called before the first frame update
    void Start()
    {
        //m_PauseItens = GameObject.FindGameObjectsWithTag("PauseRelative");
        //print(m_PauseItens);
    }

    // Update is called once per frame
    void Update()
    {
        if(InputMgr.GetKeyDown(1, InputMgr.eButton.PAUSE))
        {
            if (Time.timeScale != 0)
            {
                foreach (GameObject p in m_PauseItens)
                    p.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
                foreach (GameObject p in m_PauseItens)
                    p.SetActive(false);
            }
        }
    }
}
