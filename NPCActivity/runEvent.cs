using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class runEvent : MonoBehaviour
{
    public bool start;
    public float rTimer=0;
    public float runTime=5;
    public float runDistance=50;
    public TextMeshProUGUI rTimerUI;
    public TextMeshProUGUI DistanceToRun;
    public Image timerBar;
    float timerBarFillPercent;
    float distance;
    float distanceDifference;
    float timerDifference;
    GameObject player;

    public bool race;

    public bool triggerBased;
    bool triggered;
    public string instructions="Escape";

    MusicManager musicmanager;

    

    // Start is called before the first frame update
    void Start()
    {
        rTimerUI.text = "";
        timerBarFillPercent = 1/runTime;
        timerBar.enabled = false;
        DistanceToRun.text = "";
        player = GameObject.FindWithTag("Player");
        musicmanager = FindObjectOfType<MusicManager>().GetComponent<MusicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(start){
            if(triggerBased||race){
                if(triggered==false&&!race){
                    StopEvent();
                }
                //distance = Vector3.Distance(transform.position,player.transform.position);
                //distanceDifference = runDistance-distance;
                //distanceDifference = Mathf.Round(distanceDifference);
                timerDifference = runTime-rTimer;
                timerDifference = Mathf.Round(timerDifference*100f)/100f;
                rTimer+=1*Time.deltaTime;
                
                if(rTimer>=runTime){
                    Debug.Log("stop");
                    unsuccessfull();
                }
            }
            else{
            distance = Vector3.Distance(transform.position,player.transform.position);
            distanceDifference = runDistance-distance;
            distanceDifference = Mathf.Round(distanceDifference);
            timerDifference = runTime-rTimer;
            
            timerDifference = Mathf.Round(timerDifference*100f)/100f;
            rTimer+=1*Time.deltaTime;
            if(distanceDifference<=0){
                StopEvent();
            }
            else if(rTimer>=runTime){
                if(race){GetComponent<RaceRings>().ResetRings();}
                unsuccessfull();
            }}
      
        }
       
    }
    void FixedUpdate(){
        if(start){
            if(rTimer<=runTime){
                timerBar.fillAmount = 1-(timerBarFillPercent*rTimer);
                rTimerUI.text = ""+timerDifference;
            }
            if(triggerBased||race){
                DistanceToRun.text = instructions;
            }
            else if(distanceDifference>=0){
                DistanceToRun.text = ""+distanceDifference;
            }

        }
    }

    public void StartEvent(){
        if(GameData.run_event==false){
        musicmanager.ChangeSound("run");
        GameData.run_event=true;
        start = true;
        rTimer=0;
        if(!race){
        timerBar.enabled=true;}
        DistanceToRun.text = "";
        rTimerUI.text = "";
        }
    }

    public void StopEvent(){
        musicmanager.ChangeSound("default");
         GameData.run_event=false;
         if(GetComponent<NPCTalk>()){
             GetComponent<NPCTalk>().dislikePlayer=false;
         }
        timerBar.enabled=false;
        timerDifference=0;
        start=false;
        rTimer=0;
        DistanceToRun.text = "";
        rTimerUI.text = "";
    }
    void unsuccessfull(){
        musicmanager.ChangeSound("default");
         GameData.run_event=false;
        timerDifference=0;
        timerBar.enabled=false;
        rTimer=0;
        start=false;
        DistanceToRun.text = "";
        rTimerUI.text = "";
        if(GetComponent<RaceRings>()){
            GetComponent<RaceRings>().ResetRings();
        }
        else{
            player.GetComponent<HealthStamManager>().KillPlayer();
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
        Debug.Log("triggered");
        triggered=true;
        if(triggerBased&&GameData.eggType==""){
            start=true;
            musicmanager.ChangeSound("run");
        }}
    }
    void OnTriggerExit(){
        triggered=false;
        if(triggered==true&&triggerBased&&GameData.eggType==""){
        musicmanager.ChangeSound("default");}
    }

    
}
