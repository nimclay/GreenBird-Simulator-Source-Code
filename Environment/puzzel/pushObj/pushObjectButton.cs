using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushObjectButton : MonoBehaviour
{
    public GameObject barriers;
    public float time;
    public bool hasTime;

    private float timeCounter;
    private bool counting;

    public MusicManager music;
    public HealthStamManager health;

    bool active;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<interactable>().ObjInteractable){
            if(Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.JoystickButton0)){
                toggle();
            }
        }
        if(counting&&hasTime){
            timeCounter+=1*Time.deltaTime;
            if(timeCounter>=time){
                toggle();
            }
        }
        if(health.currentHealth<=0&&active){
            toggle();
        }
    }
    void StartTimer(){
        timeCounter=0;
        counting=true;
        music.ChangeSound("puzzel");
    }
    void StopTimer(){
        timeCounter=0;
        counting=false;
        music.ChangeSound("default");
    }
    public void toggle(){
        active=!active;
        for(int i=0;i<transform.childCount;i++){
            PushingObstacle obstaclecode = transform.GetChild(i).GetComponent<PushingObstacle>();
            obstaclecode.playable = !obstaclecode.playable;
            barriers.SetActive(obstaclecode.playable);
            
            if(obstaclecode.playable){
                StartTimer();
            }
            else{
                StopTimer();
            }
        }
    }

    
}
