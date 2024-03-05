using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eggSim : MonoBehaviour
{
    interactable interCode;
    public interactable NestInteraction;
    public Vector3 nursePosition;
    Vector3 position;
    public GameObject Player;
    public Vector3 carryPosition;

    GameObject baby;
    Renderer renderers;

    public eventManager eventThing;

    public bool sandBird;

    bool pickedUp;
    public bool collectedBool {
         get {
             return collected;
         }    
     }
    bool collected;

    public Transform nest;
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        interCode = GetComponent<interactable>();
        baby = transform.GetChild(0).gameObject;
        renderers = GetComponent<Renderer>();
        baby.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.JoystickButton0))&&!pickedUp&&interCode.ObjInteractable&&!collected){
            if(!GameData2.finalStand){
            PickUp();}
        }
        else if(pickedUp&&NestInteraction.ObjInteractable){
            ObtainEgg();
        }
    }

    void PickUp(){

        pickedUp = true;
        transform.parent = Player.transform;
        transform.localPosition = carryPosition;
        Destroy(interCode.ui);
        interCode.enabled = false;
        pickedUp=true;
    }

    public void DropEgg(){
        transform.parent = null;
        transform.position = position;
        pickedUp = false;
        collected = false;
        interCode.enabled=true;
        baby.SetActive(false);
        renderers.enabled = true;
        if(eventThing.sandBird){
            GameData2.SandBird=false;
        }
        else if(eventThing.volcBird){
            GameData2.VolcBird=false;
        }
        else if(eventThing.snowBird){
            GameData2.SnowBird = false;
        }
        else if(eventThing.greenBird){
            GameData2.GreenBird = false;
        }
        GameData2.TestEggs();
    }

    public void ObtainEgg(){
        pickedUp=false;
        collected = true;
        transform.parent = nest;
        if(interCode.ui!=null){
        Destroy(interCode.ui.gameObject);}
        interCode.enabled=false;
        transform.localPosition = nursePosition;
        renderers.enabled = false;
        baby.SetActive(true);
        if(eventThing.sandBird){
            GameData2.SandBird=true;
        }
        else if(eventThing.volcBird){
            GameData2.VolcBird=true;
        }
        else if(eventThing.snowBird){
            GameData2.SnowBird = true;
        }
        else if(eventThing.greenBird){
            GameData2.GreenBird = true;
        }
        eventThing.Reset();
    }
}
