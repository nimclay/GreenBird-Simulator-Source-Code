using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
    interactable interactionCode;
    GameObject egg;

    public bool nursingEgg;
    // Start is called before the first frame update
    void Start()
    {
        interactionCode=GetComponent<interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(interactionCode.ObjInteractable){
            if(Input.GetKeyDown(KeyCode.E)){

            
            if(GameData.eggCollected&&!GameData.NursingEgg){
                NurseEgg();
            }
            else{if(nursingEgg){

            }
            }

            }
        }
    }
    void NurseEgg(){
        nursingEgg=true;
        egg = GameData.egg;
        egg.transform.parent = gameObject.transform;
        egg.transform.localPosition = new Vector3(0,0.25f,0);
        egg.GetComponent<eggBehaviour>().raise();
        egg.transform.rotation = Quaternion.Euler(new Vector3(-90,0,0));
        GameData.NursingEgg=true;
        GameData.eggCollected=false;
    }
}
