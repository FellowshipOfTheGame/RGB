using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class AutoSelectInputField : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InputField inputField = GetComponent<InputField>();
        inputField.Select();
        inputField.ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
