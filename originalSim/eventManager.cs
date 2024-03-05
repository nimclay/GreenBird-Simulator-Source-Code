using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class eventManager : MonoBehaviour
{
    public float difficulty=0;
    float variance=0;
    
    //ui
    public Image timerBar;
    public Image healthBar;
    public Image completeBar;
    public TextMeshProUGUI eventText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;

    public GameObject EntireUI;
    public float health = 5;
    float completionScore;

    public float TimeDegration=0;
    float currentEventTime;
    bool activeEvent;
    public float timer;

    public bool greenBird;
    public bool sandBird;
    public bool snowBird;
    public bool volcBird;

    public eggSim eggCode;

    public Fade HurtingBar;

    public static float score;
    [System.Serializable]
    public class followUpEventThings{
        public string EventName="return object";
        public float TimeStandard=40;
        public float ScoreStandard=100;
        public eventComplete followUpEventCode;

    }
    [System.Serializable]
    public class Events{
        public string EventName="get thing";
        public float TimeStandard=40;
        public float ScoreStandard=100;
        public eventComplete completionRequirements;
        public bool followUpEvent;
        public followUpEventThings things;
        public float EXP = 100;
       
    }

    public Events[] eventList;
    eventComplete completion;

    public float eventTimeThing;
    float scoreThing;
    float EXPThing;
    string EventText="";
    bool followevent=false;
    int currentEvent;


    bool active=true;
    public GameData2 gameData;
    public bool BirdActive = true;
    public bool playing = true;
    float healthFillPercent;
    // Start is called before the first frame update
    void Start()
    {
        if(greenBird){
        eggCode.ObtainEgg();}
        else{
            EntireUI.SetActive(false);
            StopCoroutine("buffer");
        }
        TimeDegration += Variables.bonusTimeDegradation;
        health = 3+Variables.bonusHealth;
        healthFillPercent = 1/health;
        healthBar.fillAmount = 1;
        score=0;
    }
    float timerBarFillPercent;
    // Update is called once per frame
    void Update()
    {
        scoreText.text = ""+score;
        if(active){
        float fill = 1-(timerBarFillPercent*timer);
        float displayTime = eventTimeThing - timer;
        timerBar.fillAmount = fill;
        eventText.text = EventText;
        
        timeText.text = ""+Mathf.Round(displayTime);
        
        if(activeEvent){
            if(playing){
            timer += 1*Time.deltaTime;
            if(timer<10){
                HurtingBar.occilating = false;
            }
            if(timer>=eventTimeThing-10){
                HurtingBar.occilating = true;
            }
           
            

            if(timer>=eventTimeThing){
                activeEvent=false;
                
                LoseLife();
            }
            if(completion.eventCompleted||Input.GetKeyDown(KeyCode.V)){
                CompleteEvent();
            }}
        }
        else{
            HurtingBar.occilating = false;
        }}
    }


    void RandomEvent(float difficulty,float variance,bool follow){
        StopCoroutine("buffer");
        int num = Random.Range(0,10000000);
        Random.InitState(num);
        int randomEventNum = Random.Range(0,eventList.Length);
        if(follow){followevent=true;
        eventTimeThing = eventList[currentEvent].things.TimeStandard - (TimeDegration*difficulty) - Random.Range(difficulty-variance,difficulty+variance);
        scoreThing = eventList[currentEvent].things.ScoreStandard*(eventList[randomEventNum].TimeStandard/eventTimeThing);
        completion = eventList[currentEvent].things.followUpEventCode;
        timerBarFillPercent = 1/eventTimeThing;
        EventText = eventList[currentEvent].things.EventName;
        }
        else{
        currentEvent = randomEventNum;
        eventTimeThing = eventList[randomEventNum].TimeStandard - (TimeDegration*difficulty) - Random.Range(difficulty-variance,difficulty+variance);
        scoreThing = eventList[randomEventNum].ScoreStandard*(eventList[randomEventNum].TimeStandard/eventTimeThing);
         EXPThing = eventList[currentEvent].EXP*(eventList[randomEventNum].TimeStandard/eventTimeThing);
        completion = eventList[randomEventNum].completionRequirements;
        timerBarFillPercent = 1/eventTimeThing;
        EventText = eventList[randomEventNum].EventName;}}
       
        

    void NewEvent(){
        StopCoroutine("buffer");
        if(!activeEvent){
        int num = Random.Range(0,10000000);
        Random.InitState(num);
        difficulty+=(1+Variables.bonusDifficulty);
        variance = difficulty*1.2f;
       
        
        RandomEvent(difficulty,variance,false);
        
        activeEvent=true;}
    }

    void CompleteEvent(){
        score += scoreThing;
        timer = 0;
        SuperBird.levelxp += EXPThing;
        activeEvent=false;
        if(followevent){followevent=false;eventTimeThing=0;
            EventText = "no objective";
        eventBuffer(0,false);GainCompletion();}
        else{
        if(!eventList[currentEvent].followUpEvent){
            eventTimeThing=0;
            EventText = "no objective";
        eventBuffer(0,false);GainCompletion();}
        
        else{eventBuffer(0.5f,true);}}
        
        
        
    }
    void eventBuffer(float time,bool bonus){
        float buf = Random.Range(3,10);
        activeEvent = false;
       StopCoroutine("buffer");
        if(time!=0){
        StartCoroutine(buffer(time,bonus));}
        else{StartCoroutine(buffer(buf,bonus));}

    }

    public IEnumerator buffer(float time,bool bonus){

        yield return new WaitForSeconds(time);
        if(!bonus){
        NewEvent();}
        else{
            RandomEvent(difficulty,variance,true);activeEvent=true;
        }
    }
    public void LoseLife(){
        health-=1;
        HurtingBar.occilating = false;
        if(health<=0){
            health = 0;
            BirdDies();
        }
        healthBar.fillAmount = healthFillPercent*health;
        timer=0;
        activeEvent=false;
        EventText = "no objective";
        timeText.text = "0";
        followevent=false;
        HurtingBar.occilating = false;
        eventBuffer(5,false);
    }
    void GainCompletion(){
        completionScore+=1;
        if(completionScore>=5){
            completionScore=5;
            SuperBirdThing();
        }
        completeBar.fillAmount = 0.2f*completionScore;
    }

    public void Reset(){
        health = 3;
        completionScore = 0;
        completeBar.fillAmount = 0.2f*completionScore;
        healthBar.fillAmount = healthFillPercent*health;
        timeText.text = "0";
        EntireUI.SetActive(true);
        eventBuffer(0,false);
    }

    void BirdDies(){
        HurtingBar.occilating = false;
        completionScore = 0;
        EntireUI.SetActive(false);
        timer = 0;
        followevent=false;
        activeEvent=false;
        active=false;
        eggCode.DropEgg();
        StartCoroutine(NoBirds());
    }
    IEnumerator NoBirds(){
        BirdActive = false;
        yield return new WaitForSeconds(0.2f);
        BirdActive = true;
    }
    void SuperBirdThing(){
        score += 1000;
        health = 3;
        completionScore = 0;
        float percentage = 0.2f;
        difficulty+=3;
        healthBar.fillAmount = 1;
        completeBar.fillAmount = percentage*completionScore;
        if(greenBird){
            gameData.GainGreenBird();
        }
        else if(sandBird){
            gameData.GainSandBird();
        }
        else if(volcBird){
            gameData.GainVolcBird();
        }
        else if(snowBird){
            gameData.GainSnowBird();
        }

    }
}
