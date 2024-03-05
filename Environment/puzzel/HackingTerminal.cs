using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingTerminal : MonoBehaviour
{

    interactable interCode;
    public HackingController hack;
    // Start is called before the first frame update
    void Start()
    {
        interCode = GetComponent<interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(interCode.ObjInteractable){
            if((Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.JoystickButton0))){
                    if((!interCode.Player.GetComponent<SuperBird>().supermode&&interCode.Player.GetComponent<movement>().moveAble)){
                        hack.StartThing();
                    }
            }
        }
    }
}
