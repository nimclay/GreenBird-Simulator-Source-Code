using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gatherLocation : MonoBehaviour
{
    [System.Serializable]
    public class possibleItems{
        public string ItemName;
        public float ItemRarity;
    }
    // for the item raritys, make the most common first, then if there is two items, make the second one unit smaller than the first, then keep the rest the same
    //This ensures that the first item does not have a higher chance of occuring than it should, and the rarity for that item sorts itself out with more lower number rarity items
    // checks if the number is larger than 100 - rarity
    
    
    public possibleItems[] GatherableItems;

    interactable interactionCode;
    public GameObject GatherTextBox;
    string gatheredobject = "";
    bool gatherable=true;
    public float maxGatherItems=5;
    float currentGatherableitems;
    public float Gather_CoolDown;
    float timer;
    bool deactivated=false;
    HealthStamManager movementcode;

    public AudioSource audiosource;
    public AudioClip gatherclip;
    public ParticleSystem collectParticles;
    // Start is called before the first frame update
    void Start()
    {
        interactionCode = GetComponent<interactable>();
        movementcode = GetComponent<interactable>().Player.GetComponent<HealthStamManager>();
        if(GatherTextBox!=null){
        GatherTextBox.SetActive(false);}
        currentGatherableitems=maxGatherItems;
        gatherable=true;
    }

    // Update is called once per frame
    void Update()
    {
        if(interactionCode.ObjInteractable&&gatherable&&!deactivated){
            if((Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.JoystickButton0))){
                gatherable=false;
                GatherMaterial();
            }
        }
        if(deactivated){
            timer+=1*Time.deltaTime;
            if(timer>=Gather_CoolDown){
                timer = 0;
                Activate();
            }
        }
    }


    void GatherMaterial(){
        
        currentGatherableitems-=1;
        if(currentGatherableitems<=0){
            DeActivate();
        }
       collectItem(true);
       StartCoroutine(collectDelay());
       
    }
    public IEnumerator textBox(){
        yield return new WaitForEndOfFrame();
        if(GatherableItems.Length!=0&&GatherTextBox!=null){
        GatherTextBox.SetActive(true);
        GatherTextBox.transform.GetChild(0).GetComponent<Text>().text = "Found " + gatheredobject;}
        yield return new WaitForSeconds(1f);
        gatherable=true;
        if(GatherTextBox!=null){
        gatheredobject="";
        GatherTextBox.SetActive(false);}
    }
    void Activate(){
        GetComponent<interactable>().enabled=true;
        deactivated=false;
        currentGatherableitems=maxGatherItems;
        transform.GetChild(0).gameObject.SetActive(true);
        if(GetComponent<ParticleSystem>()){GetComponent<ParticleSystem>().Play();}
    }
    void DeActivate(){
        GetComponent<interactable>().enabled=false;
        GetComponent<interactable>().imageCreated=false;
        if(GetComponent<ParticleSystem>()){GetComponent<ParticleSystem>().Stop();}
        if(GetComponent<interactable>().ui!=null){Destroy(GetComponent<interactable>().ui);}
        transform.GetChild(0).gameObject.SetActive(false);
        deactivated=true;


    }
    public void collectItem(bool PlaySound){
        collectParticles.Play();
        if(PlaySound){
        audiosource.PlayOneShot(gatherclip,Variables.volume);}
    }
    IEnumerator collectDelay(){
        yield return new WaitForSeconds(0.5f);
        gatherable = true;
    }
}
