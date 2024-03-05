using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interactable : MonoBehaviour
{
    public GameObject Player;
    float distance;
    public float interactableDistance = 10f;

    public bool ObjInteractable;

    public bool triggerBased;
    public bool triggered;


    public Image ui;
    public Image prefabui;

    public bool marker;


    public bool imageCreated=false;

    Vector3 Offset = new Vector3(0,1.5f,0);

    Transform Canvas;
    bool act=true;
    // Start is called before the first frame update
    void Start()
    {
        Canvas = FindObjectOfType<Canvas>().transform;
        if(GetComponent<gatherLocation>()){
            GetComponent<gatherLocation>().enabled=false;
            act=false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(act==false){
            if(GetComponent<gatherLocation>()){
                GetComponent<gatherLocation>().enabled=true;
            }
        }
        if(!triggerBased){
        distance=Vector3.Distance(transform.position,Player.transform.position);
        if(distance<=interactableDistance){
            ObjInteractable=true;
        }
        else{
            ObjInteractable=false;
        }
        }
        else{
            ObjInteractable=triggered;
        }

        if(marker){

        
        if(ObjInteractable&&!imageCreated&&Player.GetComponent<movement>().moveAble){
            imageCreated=true;
            ui=Instantiate(prefabui,Canvas).GetComponent<Image>();
        }
        else{
            
            if(ui!=null){
                if(!ObjInteractable||!Player.GetComponent<movement>().moveAble){
                    Destroy(ui.gameObject);
                    imageCreated=false;
                }
                else{
                    ui.transform.position=Camera.main.WorldToScreenPoint(transform.position+Offset);
                }
            }
        }}

        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            triggered=true;
        }
    }
    void OnTriggerExit(Collider other){
        triggered=false;
    }
    


}
