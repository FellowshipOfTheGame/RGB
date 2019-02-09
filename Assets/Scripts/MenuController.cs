using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//WE ARE NOT USING THIS ANYMORE!

public class MenuController : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool buttonSelected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputMgr.GetAxis(1, InputMgr.eAxis.VERTICAL) != 0  && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }

    }

    private void OnDisable()
    {
        buttonSelected = false;
    }
}
