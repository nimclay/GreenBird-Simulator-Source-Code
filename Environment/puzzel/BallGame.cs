using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallGame : MonoBehaviour
{

    public GameObject BlueBallPrefab;
    public GameObject RedBallPrefab;
    private GameObject currentBall;

    public GameObject BlueSpawn;
    public GameObject RedSpawn;
    public bool CurrentBallIsRed;

    public float maxScore;
    private float currentScore;
    public float maxTime;
    private float timer;

    public eventComplete eventCode;

    public BallHoop[] hoops;

    public bool Started;
    private interactable interCode;

    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI score;

    public AudioSource source;

    public eventManager[] EventSystems;

    public MusicManager musicSystem;

    public bool startRed;

    // Start is called before the first frame update
    void Start()
    {
        interCode = GetComponent<interactable>();
        timer = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.JoystickButton0)){
            if(interCode.ObjInteractable){
                if(!Started){
                StartGame();}
            }
        }
        if(Started){
            timer -= 1*Time.deltaTime;
            TimerText.text = ""+(Mathf.Round(timer*100))/100;
            score.text = "Score: " + currentScore + "/" + maxScore;
            if(timer<=0){
                EndEvent(false);
            }

        }
    }

    public void KillBall(){
        CurrentBallIsRed = !CurrentBallIsRed;
        if(currentBall!=null){
            Destroy(currentBall);
        }
        SpawnBall();
    }

    public void GetScore(float score){
        currentScore+=score;
        if(currentScore>=maxScore){
            currentScore = 0;
            EndEvent(true);
        }
    }
    public void RemoveScore(){
        currentScore -= 1;
    }

    void EndEvent(bool win){
        Started = false;
        SpawnBall();
        if(currentBall!=null){
            Destroy(currentBall);
        }
        if(win){
            eventCode.CompEvent();
        }
        TimerText.text = "";
        score.text = "";
        musicSystem.ChangeSound("default");
        for(int i=0;i<hoops.Length;i++){
            hoops[i].TurnOn();
        }
        for(int i = 0;i<EventSystems.Length;i++){
            EventSystems[i].playing=true;
        }
        if(timer >= Variables.SoccerHighScore){
            Variables.SoccerHighScore = timer;
            Variables.HasSoccerHighScore=true;
        }
        

    }
    void StartGame(){
        Started = true;
        timer = maxTime;
        currentScore = 0;
        if(startRed){
            CurrentBallIsRed=true;
        }
        else{
        CurrentBallIsRed = false;}
        for(int i = 0;i<EventSystems.Length;i++){
            EventSystems[i].playing=false;
        }
        musicSystem.ChangeSound("puzzel");
        SpawnBall();
    }

    void SpawnBall(){
        if(currentBall!=null){
            Destroy(currentBall);
        }
        if(CurrentBallIsRed){
            currentBall = Instantiate(RedBallPrefab,RedSpawn.transform.position,Quaternion.Euler(Vector3.zero));
        }
        else{
            currentBall = Instantiate(BlueBallPrefab,BlueSpawn.transform.position,Quaternion.Euler(Vector3.zero));
        }
        for(int i=0;i<hoops.Length;i++){
            hoops[i].TurnOn();
        }
    }
}
