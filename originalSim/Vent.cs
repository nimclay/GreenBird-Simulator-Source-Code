using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    interactable interCode;
    public GameObject pairedVent;
    public Vector3 teleportForce;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        interCode = GetComponent<interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.JoystickButton0))&&interCode.ObjInteractable){
            Player.transform.position = pairedVent.transform.position;
            Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Player.GetComponent<Rigidbody>().AddRelativeForce(teleportForce);
            interCode.triggered=false;
            if(interCode.ui!=null){
                Destroy(interCode.ui.gameObject);
            }
        }
    }
}
