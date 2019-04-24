using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSwitch : MonoBehaviour
{
    // Initialization 
    private Light myLight;

    private void Start()
    {
        myLight = GetComponent<Light>();
    }

    // Update when spacebar is press. 
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            myLight.enabled = !myLight.enabled;
        }
    }

}
