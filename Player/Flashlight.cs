using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{   
    public Light FlashLight;
    bool active;
    // Start is called before the first frame update
    void Start()
    {
        FlashLight = GetComponent<Light>();
        FlashLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)||Input.GetKeyDown(KeyCode.JoystickButton11)){
            active=!active;
            FlashLight.enabled = active;
        }
    }
}
