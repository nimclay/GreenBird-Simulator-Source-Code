using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzel : MonoBehaviour
{
    public Material ActiveMat;
    public Material DeActiveMat;
    bool active;

    public bool FinalTrigger;

    [System.Serializable]
    public class Pictures{
        public string pictureName;
        public GameObject[] tiles;
        public string effect;
        public eventComplete eventCode;
    }
    public Pictures[] picture;

    public eventManager[] events;
    public SuperBird lvlCode;
    public GameObject[] AllTiles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            active = !active;
            if(active){
                gameObject.GetComponent<MeshRenderer>().material = ActiveMat;
            }
            else{
                gameObject.GetComponent<MeshRenderer>().material = DeActiveMat;
            }
            if(FinalTrigger&&active){
                Search();
                for(int i = 0; i < AllTiles.Length; i++){
                    AllTiles[i].GetComponent<ButtonPuzzel>().active = false;
                    AllTiles[i].GetComponent<MeshRenderer>().material = DeActiveMat;
                }
            }
        }
    }

    void Search(){
        float activator = 0;
        string effectName = "";
        int number = -1;
        for(int i = 0; i < picture.Length; i++){
            for(int x = 0; x < picture[i].tiles.Length; x++){
                if(picture[i].tiles[x].GetComponent<ButtonPuzzel>().active == true){
                    activator += 1;
                }
            }
            if(activator==picture[i].tiles.Length){
                effectName = picture[i].effect;
                number = i;
            }
            activator = 0;
        }
        StartEffect(effectName,number);
    }

    void StartEffect(string effectName, int puzzelNum){
        if(puzzelNum!=(-1)){
        if(effectName == "heal"){
            for(int i = 0; i<events.Length; i++){
                events[i].health = 3;
                events[i].eventTimeThing+=40;
                events[i].healthBar.fillAmount = 0.33f*events[i].health;
            }
        }
        if(effectName == "lvl"){
            lvlCode.level += 2;
        }
        if(picture[puzzelNum].eventCode!=null){
            picture[puzzelNum].eventCode.CompEvent();
        }
        }
    }
}
