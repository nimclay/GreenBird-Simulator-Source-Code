using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimNPCTalk : MonoBehaviour
{

    [System.Serializable]
    public class Dialogue{
        public string SpeachName;
        public string speach;
        public response Responses;
        public Options option;
        [Header("ShopKeeper")]
        public bool LevelUpSpeedGuy;
        public bool ResetDifficultyGuy;
        public bool HealBirdsGuy;
        public eventManager managerCode;
        public CoinManager money;
        public movement moveCode;
        public float requiredCoins=10;
    }
    [System.Serializable]
    public class response{
        public string ResponseUp1;
        public string ResponseDown2;
        public string ResponseLeft3;
        public string ResponseRight4;

        public eventComplete eventCode;
        public RaceRings race;
        [Range(1,4)]
        public int CorrectResponse;
        [Range(1,4)]
        public int ExitResponse;
        
    }
    [System.Serializable]
    public class Options{
        public string option1Up;
        public string option2Down;
        public string option3Left;
        public string option4Right;
    }
    [Header("Dialogue")]
    public Dialogue[] NPCChat;
    [Space(10)]
    [Header("Visuals")]
    public Fade DialogueTBFade;
    public TextMeshProUGUI ResponseText1;
    public TextMeshProUGUI ResponseText2;
    public TextMeshProUGUI ResponseText3;
    public TextMeshProUGUI ResponseText4;
    public TextMeshProUGUI DialogueBox;
    public TextMeshProUGUI NameTag;
    [Header("option display")]
    public GameObject ResponseUp1Display;
    public GameObject ResponseDown2Display;
    public GameObject ResponseLeft3Display;
    public GameObject ResponseRight4Display;
    [Space(10)]
    [Header("Setting")]
    public string NPCName;
    public movement moveCode; //call moveCode.moveAble=false;
    public CameraMove camCode; //call camCode.camMoveAble=false;
    public Transform CamTarget;
    public AudioSource audiosource;
    public AudioClip TalkSound;
    public AudioClip ExitSound;
    public AudioClip CompleteSound;

    private Transform CamOldTarget;
    private int currentSpeach;
    private bool Selectable=true;
    private bool UpPressable = true;
    private bool DownPressable = true;
    private bool LeftPressable = true;
    private bool RightPressable = true;
    private bool talking=false;

    private bool onResponse = false;

    private int responseNum;
    private interactable interCode;
    

    public bool ScoreGuy;
    public Color scoreColor;

    // Start is called before the first frame update
    void Start()
    {
        interCode = GetComponent<interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!talking&&interCode.ObjInteractable){
            if(Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.JoystickButton0)){
                StartSpeach();
            }
        }

        if(talking&&Selectable){
            if((Input.GetKeyDown(KeyCode.UpArrow)||Input.GetAxis("NumPad Y")>0)&&UpPressable){
                    SelectOption(1);
                }
                else if((Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetAxis("NumPad X")<0)&&LeftPressable){
                    SelectOption(3);
                }
                else if((Input.GetKeyDown(KeyCode.DownArrow)||Input.GetAxis("NumPad Y")<0)&&DownPressable){
                    SelectOption(2);
                }
                else if((Input.GetKeyDown(KeyCode.RightArrow)||Input.GetAxis("NumPad X")>0)&&RightPressable){
                    SelectOption(4);
                }
        }
    }

    private void StartSpeach(){
        DialogueTBFade.ShowUI();
        talking = true;
        NameTag.text = NPCName;
        Selectable=true;
        moveCode.moveAble = false;
        moveCode.rbody.isKinematic = true;
        CamOldTarget = camCode.target;
        camCode.target = CamTarget;
        camCode.camMoveAble = false;
        currentSpeach=0;
        RightPressable = true;
        LeftPressable = true;
        UpPressable = true;
        DownPressable=true;
        onResponse = false;
        SetText();

    }
    private void SetText(){
        if(ScoreGuy){
            DialogueBox.text = "High Score: <color=purple>"+Variables.highscore + "</color>\n" + "Highest Level: <color=purple>"+Variables.level+"</color>\n"
            +"Most Birds Grown: <color=purple>"+Variables.HighestBirdCount+"</color>\n"+"Best LaserBall Time: <color=purple>"+ Variables.SoccerHighScore+"</color>\n"
            +"Number Of Wins: <color=purple>"+ Variables.Wins+"</color>\n";
        }
        //set the responses up
        if(NPCChat[currentSpeach].option.option1Up==""){
            ResponseUp1Display.SetActive(false);
            UpPressable=false;}
        else{
            ResponseUp1Display.SetActive(true);
            UpPressable=true;}

        if(NPCChat[currentSpeach].option.option2Down==""){
            ResponseDown2Display.SetActive(false);
            DownPressable=false;}
        else{
            ResponseDown2Display.SetActive(true);
            DownPressable=true;}
        
        if(NPCChat[currentSpeach].option.option3Left==""){
            ResponseLeft3Display.SetActive(false);
            LeftPressable=false;}
        else{
            ResponseLeft3Display.SetActive(true);
            LeftPressable=true;}

        if(NPCChat[currentSpeach].option.option4Right==""){
            ResponseRight4Display.SetActive(false);
            RightPressable=false;}
        else{
            ResponseRight4Display.SetActive(true);
            RightPressable=true;}
        //set up the text
       
        ResponseText1.text = NPCChat[currentSpeach].option.option1Up;
        ResponseText2.text = NPCChat[currentSpeach].option.option2Down;
        ResponseText3.text = NPCChat[currentSpeach].option.option3Left;
        ResponseText4.text = NPCChat[currentSpeach].option.option4Right;
        if(!ScoreGuy){
        DialogueBox.text =  NPCChat[currentSpeach].speach;}
        if(onResponse){
            ResponseRight4Display.SetActive(false);
            ResponseLeft3Display.SetActive(false);
            ResponseUp1Display.SetActive(false);
            RightPressable = false;
            LeftPressable = false;
            UpPressable = false;
            ResponseDown2Display.SetActive(true);
            ResponseText2.text = "next";
            if(!ScoreGuy){
            if(responseNum == 1){
            DialogueBox.text = NPCChat[currentSpeach].Responses.ResponseUp1;}
            else if(responseNum == 2){
            DialogueBox.text = NPCChat[currentSpeach].Responses.ResponseDown2;}
            else if(responseNum == 3){
            DialogueBox.text = NPCChat[currentSpeach].Responses.ResponseLeft3;}
            else if(responseNum == 4){
                
            DialogueBox.text = NPCChat[currentSpeach].Responses.ResponseRight4;}}
        }
    }
    private void SelectOption(int option){
        if(Selectable){
            if(!onResponse){
            responseNum = option;}
            Selectable = false;
            if(onResponse){
                if(responseNum==NPCChat[currentSpeach].Responses.CorrectResponse){
                    if(NPCChat[currentSpeach].Responses.eventCode!=null){
                            NPCChat[currentSpeach].Responses.eventCode.CompEvent();
                            ExitSpeach();

                    }
                    else if(NPCChat[currentSpeach].Responses.race!=null){  
                            NPCChat[currentSpeach].Responses.race.NextRing();
                            ExitSpeach();    
                    }
                    
                }
                else if(responseNum==NPCChat[currentSpeach].Responses.ExitResponse){
                    ExitSpeach();
                    return;
                }
                currentSpeach += 1;
                if(currentSpeach >= NPCChat.Length){
                    ExitSpeach();
                }
            }
            else{
                if(responseNum==NPCChat[currentSpeach].Responses.CorrectResponse){
                    if(NPCChat[currentSpeach].money!=null){
                            if(NPCChat[currentSpeach].money.amountOfCoins>=NPCChat[currentSpeach].requiredCoins){
                                NPCChat[currentSpeach].money.LoseCoins(NPCChat[currentSpeach].requiredCoins);
                                if(NPCChat[currentSpeach].LevelUpSpeedGuy){
                                    NPCChat[currentSpeach].moveCode.UpgradedSpeed += 0.5f;
                                }
                                else if(NPCChat[currentSpeach].HealBirdsGuy){
                                    NPCChat[currentSpeach].managerCode.health=3;
                                    NPCChat[currentSpeach].managerCode.healthBar.fillAmount = 1;
                                }
                                else if(NPCChat[currentSpeach].ResetDifficultyGuy){
                                    NPCChat[currentSpeach].managerCode.difficulty = 0;
                                }
                            }
                            else{
                                responseNum = 3;
                            }}}
            }
            onResponse = !onResponse;
            StartCoroutine(talkDelay());
            SetText();
        }
        
    }
    private IEnumerator talkDelay(){
        Selectable = false;
        yield return new WaitForSeconds(0.5f);
        Selectable = true;
    }
    private void ExitSpeach(){
        DialogueTBFade.hideUI();
        NameTag.text = NPCName;
        moveCode.moveAble = true;
        camCode.target = CamOldTarget;
        camCode.camMoveAble = true;
        moveCode.rbody.isKinematic = false;
        currentSpeach=0;
        responseNum=0;
        onResponse = false;
        RightPressable = true;
        LeftPressable = true;
        UpPressable = true;
        DownPressable=true;
        talking = false;
    }
}
