using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class eggBehaviour : MonoBehaviour
{
    interactable interactionCode;
    public GameObject Player;

    [System.Serializable]
    public class ObjectiveItems{
        public string itemName;
        public int itemAmount;
    }

    public GameData GameData;
    [System.Serializable]
    public class Objectives{
        public ObjectiveItems[] objectiveItems;
    }

    public GameObject hatchling;

    public Objectives[] objectives;
    Vector3 position;

    public string eggtype;

    public AudioClip GrownSound;
    public AudioSource GrownSource;


    bool eggCollected;
    public bool raising;
    public TextMeshProUGUI objectiveText;
    inventory InventoryCode;
    int currentNursing=0;

    bool Collectable;

    bool growth3grown;
    // Start is called before the first frame update
    void Start()
    {
        interactionCode = GetComponent<interactable>();
        objectiveText.text="";
        InventoryCode = Player.GetComponent<inventory>();
        hatchling.SetActive(false);
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(interactionCode.ObjInteractable){
            if(raising){
                SetText();
            }
            if((Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.JoystickButton0))&&Collectable){
                HarvestSoul();
            }
            else if((Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.JoystickButton0))&&GameData.NursingEgg&&!growth3grown){
                RaiseObjective();
            }
            
            if((Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.JoystickButton0))&&!eggCollected&&!GameData.NursingEgg){
                pickupEgg();
            }
            
        }
        else{
            if(raising){objectiveText.text="";}
        }
    }

    public void DropEgg(){
        transform.parent = null;
        transform.position=position;
        eggCollected=false;
        GameData.eggCollected=false;
        GameData.egg=null;
        GameData.eggType="";
        GameData.NursingEgg=false;
    }

    void HatchEgg(){

    }
    void Growth1(){
        hatchling.SetActive(true);
        GetComponent<Renderer>().enabled=false;
    }
    void Growth2(){
        hatchling.transform.localScale = new Vector3(hatchling.transform.localScale.x*1.5f,hatchling.transform.localScale.y*1.5f,hatchling.transform.localScale.z*1.5f);
        hatchling.transform.localPosition = new Vector3(hatchling.transform.localPosition.x,hatchling.transform.localPosition.y,hatchling.transform.localPosition.z+0.002f);
        
    }
    void Growth3(){
        hatchling.transform.localScale = new Vector3(hatchling.transform.localScale.x*1.5f,hatchling.transform.localScale.y*1.5f,hatchling.transform.localScale.z*1.5f);
        hatchling.transform.localPosition = new Vector3(hatchling.transform.localPosition.x,hatchling.transform.localPosition.y,hatchling.transform.localPosition.z+0.002f);
        Collectable=true;
        objectiveText.text="";
        growth3grown=true;
    }

    void pickupEgg(){
        if(!GameData.eggCollected){
        eggCollected=true;
        GameData.eggCollected=true;
        GameData.eggType = eggtype;
        GameData.egg = gameObject;
        transform.parent = Player.transform;
        transform.localPosition = new Vector3(-0.11f,1.8f,0);
        Image uimg = GetComponent<interactable>().ui;

        Destroy(uimg);}

    }

    public void raise(){
        raising=true;
        GameData.eggType="";
        GetComponent<interactable>().imageCreated=false;
        GetComponent<interactable>().marker=false;
    }

    void RaiseObjective(){
        bool completed=false;
        bool thing=false;
        
        // check first if every item in the objective list is obtained in the right amount
        for(int i=0;i<InventoryCode.items.Length;i++){
            if(thing==true){break;}
           for(int x=0;x<objectives[currentNursing].objectiveItems.Length;x++){
               if(InventoryCode.items[i].itemName==objectives[currentNursing].objectiveItems[x].itemName){
                   if(InventoryCode.items[i].itemAmount>=objectives[currentNursing].objectiveItems[x].itemAmount){
                       completed = true;
                   }
                   else{
                       completed = false;
                       Debug.Log("failure");
                       thing=true;
                       break;
                   }
               }
              
           }
        }
        if(completed){
            for(int i=0;i<InventoryCode.items.Length;i++){
                for(int x=0;x<objectives[currentNursing].objectiveItems.Length;x++){
                    if(InventoryCode.items[i].itemName==objectives[currentNursing].objectiveItems[x].itemName){
                        if(InventoryCode.items[i].itemAmount>=objectives[currentNursing].objectiveItems[x].itemAmount){
                            InventoryCode.items[i].itemAmount-=objectives[currentNursing].objectiveItems[x].itemAmount;
                        }
                    }
                   
                   
                }
            }

            currentNursing+=1;
            if(currentNursing>3){
            finishGrowing();
            }
            else if(currentNursing==3){
                currentNursing=0;
                Growth3();
            }
            else if(currentNursing==2){
                Growth2();
            }
            else if(currentNursing==1){
                Growth1();
            }

        else{
            Debug.Log("failed");
        }
            Debug.Log("Completed");
            return;
        }
        

    }

    void SetText(){
        objectiveText.text = "";
        string textthing = "";
        if(!growth3grown){

        for(int i=0;i<objectives[currentNursing].objectiveItems.Length;i++){
            textthing += objectives[currentNursing].objectiveItems[i].itemName + " " + objectives[currentNursing].objectiveItems[i].itemAmount+".";
        }
         textthing = textthing.Replace(".","\n");
         objectiveText.text = textthing;}
    }

    void finishGrowing(){
        raising=false;
        GameData.eggCollected=false;
        GameData.egg=null;
        GameData.NursingEgg=false;
        GameData.eggType="";
        GameData.saveGame();
        GameData.UpdateGame();
        Destroy(gameObject);
    }
    public void HarvestSoul(){
        if(eggtype=="desert"){
            GameData.AmountOfSandBird+=1;
        }
        else if(eggtype=="green"){
            GameData.AmountOfGreenBird+=1;
        }
        GrownSource.PlayOneShot(GrownSound,1f);
        finishGrowing();
    }
}
