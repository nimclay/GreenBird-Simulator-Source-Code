using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDefence : MonoBehaviour
{
    public GameObject DefenceObject;

    private interactable interCode;
    // Start is called before the first frame update
    void Start()
    {
        interCode = GetComponent<interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(interCode.ObjInteractable&&(Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.JoystickButton0))){
            DefenceObject.SetActive(true);
            if(interCode.ui!=null){
                Destroy(interCode.ui);
            }
            gameObject.SetActive(false);
        }
    }

    
}
