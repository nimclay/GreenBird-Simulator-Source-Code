using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RaceRings : MonoBehaviour
{
   
    
    [System.Serializable]
    public class Ring{
        public GameObject ringOBJ;
        public float timerAmount=3;
    }

    public Ring[] rings;

    public Material ringActive;
    public Material ringDeActive;

    runEvent eventData;

    public AudioSource audiosource;
    public AudioClip completesound;
    public AudioClip ringcollectsound;

    int currentRing=-1;

    public GameObject GatherTextBox;

    public string ItemName;
    public int itemAmount=1;

    public inventory inventoryCode;

    public bool lazerpuzzel=false;
    public LazerPuzzel lazer;

    public bool eventThing=false;
    public eventComplete eventCode;
    public eventManager[] managers;

    public float TimeThing=2;
    private float time;
    public TextMeshProUGUI TimerText;
    private bool Going;

    void Start(){
        if(!lazerpuzzel||!eventThing){
            time = TimeThing;
        eventData = GetComponent<runEvent>();}
        ResetRings();
    }

    void Update(){
        if(Going){
            if(eventThing){
            time-=1*Time.deltaTime;
            TimerText.text = ""+(Mathf.Round(time*100))/100;
            if(time<=0){
                StopEvent();
            }}
        }
    }


    public void ResetRings(){
        currentRing=-1;
        for(int i=0;i<rings.Length;i++){
            if(rings[i].ringOBJ.activeSelf){
                rings[i].ringOBJ.SetActive(false);
            }
        }
    }

    public void NextRing(){
        if(!Going){
            Going = true;
            if(eventThing){
            TimerText.text = ""+time;}
        }
        if(eventThing){
        for(int i=0; i<managers.Length;i++){
            managers[i].playing = false;
        }
        }
        currentRing+=1;
        if(currentRing>=1){
        audiosource.PlayOneShot(ringcollectsound,0.5f);}
        if(currentRing>=rings.Length){
            ResetRings();
            RaceComplete();
            audiosource.PlayOneShot(completesound,1f);
            if(!lazerpuzzel&&!eventThing){
            eventData.StopEvent();}
            return;
        }
        else{
        if(rings[currentRing]!=null){
        rings[currentRing].ringOBJ.GetComponent<Renderer>().material = ringActive;
        rings[currentRing].ringOBJ.SetActive(true);}
        if(currentRing+1<=rings.Length-1){
        if(rings[currentRing+1]!=null){
        rings[currentRing+1].ringOBJ.GetComponent<Renderer>().material = ringDeActive;
        rings[currentRing+1].ringOBJ.SetActive(true);}}
        if(currentRing-1>=0){
            rings[currentRing-1].ringOBJ.SetActive(false);
            time+=rings[currentRing-1].timerAmount;
        }
        if(!lazerpuzzel&&!eventThing){if(eventData.start==true){eventData.rTimer-=rings[currentRing].timerAmount;}}
        }
       
        
        
        
    }

    public void RaceComplete(){
        Going = false;
        if(!lazerpuzzel&&!eventThing){
        inventoryCode.collectItem(false);
        for(int i=0;i<inventoryCode.items.Length;i++){
            if(inventoryCode.items[i].itemName==ItemName){
                inventoryCode.items[i].itemAmount+=itemAmount;
            }
        }
        }
        else{
            if(eventThing){
                if(eventThing){
                    for(int i=0; i<managers.Length;i++){
                        managers[i].playing = true;
                    }}
                eventCode.CompEvent();
                TimerText.text = "";
            }
            else{
            lazer.LazerComplete=true;}
            ResetRings();
        }
    }

   
    void StopEvent(){
        Going = false;
        if(eventThing){
                if(eventThing){
                    for(int i=0; i<managers.Length;i++){
                        managers[i].playing = true;
                    }}
            }
        time = TimeThing;
        TimerText.text = "";
            ResetRings();

    }
}