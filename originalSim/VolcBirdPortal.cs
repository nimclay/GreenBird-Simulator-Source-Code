using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcBirdPortal : MonoBehaviour
{

    private bool PortalOn;
    public Material OnMaterial;
    public GameObject[] anchorCrystals;

    public GameObject otherSide;

    public bool UnAnchored;
    public bool FinalStand;
    public eventComplete eventCode;
    // Start is called before the first frame update
    void Start()
    {
        if(UnAnchored){
            PortalOn = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator delay(){
        yield return new WaitForSeconds(0.1f);
        CheckCrystals();
    }
    public void CheckCrystals(){
        bool work = false;
        float count = 0;
        for(int i = 0 ; i < anchorCrystals.Length; i++){
            if(!anchorCrystals[i].GetComponent<AnchorCrystal>().living){
                count += 1;
            }
        }
        if(count == anchorCrystals.Length){
            work = true;
        }
        else{
            work = false;
        }

        if(work){
            if(FinalStand){
                eventCode.CompEvent();
            }
            else{ActivatePortal();}
        }
    }

    void ActivatePortal(){
        GetComponent<Renderer>().material = OnMaterial;
        PortalOn = true;
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            if(PortalOn){
            teleport(other.transform.gameObject);}
        }
    }
    void teleport(GameObject player){
        Vector3 pos = otherSide.transform.position;
        pos.y -=8 ;   
        if(player.transform.parent!=null){
            player = player.transform.parent.gameObject;
        }    
        player.transform.position = pos;
    }
}
