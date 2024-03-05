using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCTalk : MonoBehaviour
{
    [System.Serializable]
    public class objectiveItem{
        public string itemName;
        public int itemAmount;
    }
    [System.Serializable]
    public class conversation{
        public objectiveItem[] items;
        public string talkingText;
        public string option1;
        public string option2;
        public string option3;
        public string option4;
        public int[] wrongOption;
        public bool important;
        public bool objective;
        public string wrongOptionText;
        public bool Race=false;
    }
    [System.Serializable]
    public class npc_Chat{
       public conversation[] conversations;
       bool conversationComplete;
    }
    public npc_Chat[] npcChat;

  

    public GameObject NPCChatBox;
    public GameObject objectiveTextBox;
    public TextMeshProUGUI objectiveText;

    public TextMeshProUGUI npcNameTag;
    public TextMeshProUGUI currentText;
    public string npcName;
    public TextMeshProUGUI option1Text;
    public TextMeshProUGUI option2Text;
    public TextMeshProUGUI option3Text;
    public TextMeshProUGUI option4Text;

    public GameObject[] optionObj;

    float ConversationLength;

    int currentConversation=0;

    bool chatting;
    public bool dislikePlayer=false;
    float dislikeCounter;
    public movement PlayerCode;
    public CameraMove camCode;
    interactable Interaction;

    bool chooseable=false;
    float counter=0;

    int currentSpeach = 0;



    runEvent runaway;
    GameData gameVariables;
    // Start is called before the first frame update
    void Start()
    {
        option1Text.text = "";
        option2Text.text = "";
        option3Text.text = "";
        option4Text.text = "";
        npcNameTag.text = "";
        currentText.text="";
        NPCChatBox.SetActive(false);
        
        Interaction=GetComponent<interactable>();
        runaway=GetComponent<runEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dislikePlayer){
            dislikeCounter+=1*Time.deltaTime;
            if(dislikeCounter>=15){
                dislikeCounter=0;
                dislikePlayer=false;
            }
        }
        if((Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.JoystickButton0))&&!dislikePlayer&&!chatting&&Interaction.ObjInteractable){
            if(GameData.run_event==false){
            chatting=true;
            PlayerCode.rbody.velocity = Vector3.zero;}
        }
        if(chatting){
            ConversationLength = npcChat[currentSpeach].conversations.Length;
            NPCChatBox.SetActive(true);
            currentText.text = npcChat[currentSpeach].conversations[currentConversation].talkingText;
            if(!chooseable){
                counter+=1*Time.deltaTime;
                if(counter>=0.5f){
                    counter=0;
                    chooseable=true;
                }
            }
            PlayerCode.moveAble=false;
            camCode.camMoveAble=false;
            if(npcChat[currentSpeach].conversations[currentConversation].objective&&!objectiveTextBox.activeSelf){
                objectiveTextBox.SetActive(true);
            }
            else{
                if(npcChat[currentSpeach].conversations[currentConversation].objective==false&&objectiveTextBox.activeSelf){
                    objectiveTextBox.SetActive(false);
                }
            }
            //activate possible choices
            if(npcChat[currentSpeach].conversations[currentConversation].option1!="null"){
                optionObj[0].SetActive(true);
                    option1Text.text=npcChat[currentSpeach].conversations[currentConversation].option1;
                }
            else{optionObj[0].SetActive(false);}
            if(npcChat[currentSpeach].conversations[currentConversation].option2!="null"){
                optionObj[1].SetActive(true);
                    option2Text.text=npcChat[currentSpeach].conversations[currentConversation].option2;
                }
            else{optionObj[1].SetActive(false);}
            if(npcChat[currentSpeach].conversations[currentConversation].option3!="null"){
                optionObj[2].SetActive(true);
                    option3Text.text=npcChat[currentSpeach].conversations[currentConversation].option3;
                }
            else{optionObj[2].SetActive(false);}
            if(npcChat[currentSpeach].conversations[currentConversation].option4!="null"){
                optionObj[3].SetActive(true);
                    option4Text.text=npcChat[currentSpeach].conversations[currentConversation].option4;
                }
            else{optionObj[3].SetActive(false);}
            NPCChatBox.SetActive(true);
            npcNameTag.text = npcName;
            if(chooseable){
                if((Input.GetKeyDown(KeyCode.UpArrow)||Input.GetAxis("NumPad Y")>0)&&optionObj[0].activeSelf){
                    ChooseOption(1);
                }
                else if((Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetAxis("NumPad X")<0)&&optionObj[1].activeSelf){
                    ChooseOption(2);
                }
                else if((Input.GetKeyDown(KeyCode.DownArrow)||Input.GetAxis("NumPad Y")<0)&&optionObj[2].activeSelf){
                    ChooseOption(3);
                }
                else if((Input.GetKeyDown(KeyCode.RightArrow)||Input.GetAxis("NumPad X")>0)&&optionObj[3].activeSelf){
                    ChooseOption(4);
                }
            }
            


        }
    }
    void setObjectiveText(){
        string text="";
        for(int i=0;i<npcChat[currentSpeach].conversations[currentConversation].items.Length;i++){
            text = text + npcChat[currentSpeach].conversations[currentConversation].items[i].itemName+ " " + npcChat[currentSpeach].conversations[currentConversation].items[i].itemAmount + ".";
        }
        text = text.Replace(".","\n");
        objectiveText.text = text;

       
    }

    void ChooseOption(float option){
        chooseable=false;
        bool wrong=false;
        Input.ResetInputAxes();
        
        for(int i=0;i<npcChat[currentSpeach].conversations[currentConversation].wrongOption.Length;i++){
            if(npcChat[currentSpeach].conversations[currentConversation].Race&&npcChat[currentSpeach].conversations[currentConversation].wrongOption[i]==option){
                GetComponent<RaceRings>().NextRing();
                runaway.StartEvent();
                StopTalking();
                return;
            }
            else if(npcChat[currentSpeach].conversations[currentConversation].wrongOption[i]==option){
                wrong=true;
            }
        }
        if(wrong){
            if(npcChat[currentSpeach].conversations[currentConversation].important){
                StopTalking();
            }
            else{BadChoice();}
        }
        else{
            if(npcChat[currentSpeach].conversations[currentConversation].important){
                currentSpeach+=1;
            }
            currentConversation+=1;
            if(currentConversation>ConversationLength-1){
                currentConversation=0;
                StopTalking();
            }
        }
        if(npcChat[currentSpeach].conversations[currentConversation].objective){
            setObjectiveText();
        }
    }

    void BadChoice(){
        runaway.StartEvent();
        NPCChatBox.SetActive(false);
        dislikePlayer=true;
        chatting = false;
        PlayerCode.moveAble=true;
        camCode.camMoveAble=true;
        currentConversation = 0;
    }
    void StopTalking(){
        NPCChatBox.SetActive(false);
        chatting = false;
        PlayerCode.moveAble=true;
        camCode.camMoveAble=true;
        currentConversation = 0;
        objectiveTextBox.SetActive(false);
        
    }
}
