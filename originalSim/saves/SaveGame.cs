using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class SaveGame : MonoBehaviour
{
    public static bool[] unlockedSkins;
    public static bool gameStarted;
    public bool resetData;
    public bool UnlockAll;
    // Start is called before the first frame update
    void Start()
    {
        if(UnlockAll){
            UnlockEverything();
            gameStarted = true;
        }
        
        if(PlayerPrefs.GetFloat("CamSensx")==0){
            PlayerPrefs.SetFloat("CamSensx",400);
        }
        if(PlayerPrefs.GetFloat("CamSensy")==0){
            PlayerPrefs.SetFloat("CamSensy",400);
        }
        if(PlayerPrefs.GetFloat("CamDistance")==0){
            PlayerPrefs.SetFloat("CamDistance",6);
        }
        if(PlayerPrefs.GetInt("started")==1){
            gameStarted = true;
        }
        if(!gameStarted){
            reset();
            StartCoroutine(StartGame());}
        else{
            gameStarted = false;
        }
        
        if(resetData){
           reset();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Save(){
        PlayerPrefs.SetFloat("CoinCount", Variables.coins);
        PlayerPrefs.SetFloat("PlayerLevel",Variables.level);
        PlayerPrefs.SetFloat("dayScore",Variables.days);
        PlayerPrefs.SetFloat("HighScore",Variables.highscore);
        PlayerPrefs.SetFloat("SoccerHighScore",Variables.SoccerHighScore);
        PlayerPrefs.SetFloat("BirdAmounts",Variables.HighestBirdCount);
        PlayerPrefs.SetFloat("HighLevel",Variables.HighestLevel);
        PlayerPrefs.SetFloat("Wins",Variables.Wins);
        PlayerPrefs.SetFloat("CamSensx",Variables.xsensitivity);
        PlayerPrefs.SetFloat("CamSensy",Variables.xsensitivity);
        PlayerPrefs.SetFloat("CamDistance",Variables.camdistance);

        PlayerPrefs.SetFloat("dayvar1",Variables.dayvar1);
        PlayerPrefs.SetFloat("dayvar2",Variables.dayvar2);
        PlayerPrefs.SetFloat("dayvar3",Variables.dayvar3);
        PlayerPrefs.SetFloat("dayvar4",Variables.dayvar4);
        PlayerPrefs.SetFloat("dayvar5",Variables.dayvar5);

        PlayerPrefs.SetFloat("futurevar1",Variables.futurevar1);
        PlayerPrefs.SetFloat("futurevar2",Variables.futurevar2);
        PlayerPrefs.SetFloat("futurevar3",Variables.futurevar3);
        if(Variables.WorldSaved){
            PlayerPrefs.SetInt("SavedWorld",1);
        }
        else{
            PlayerPrefs.SetInt("SavedWorld",0);
        }
    }
    public static void reset(){
        unlockedSkins = new bool[40];
        unlockedSkins[0] = true;
        SaveSkins(unlockedSkins);
        PlayerPrefs.SetFloat("CoinCount", 0);
        PlayerPrefs.SetFloat("PlayerLevel",0);
        PlayerPrefs.SetFloat("dayScore",0);
        PlayerPrefs.SetFloat("HighScore",0);
        PlayerPrefs.SetFloat("SoccerHighScore",0);
        PlayerPrefs.SetFloat("BirdAmounts",0);
        PlayerPrefs.SetFloat("HighLevel",0);
        PlayerPrefs.SetFloat("Wins",0);
        PlayerPrefs.SetInt("SavedWorld",0);
        PlayerPrefs.SetFloat("CamSensx",400);
        PlayerPrefs.SetFloat("CamSensy",400);
        PlayerPrefs.SetFloat("CamDistance",6);

        PlayerPrefs.SetFloat("dayvar1",0);
        PlayerPrefs.SetFloat("dayvar2",0);
        PlayerPrefs.SetFloat("dayvar3",0);
        PlayerPrefs.SetFloat("dayvar4",0);
        PlayerPrefs.SetFloat("dayvar5",0);

        PlayerPrefs.SetFloat("futurevar1",0);
        PlayerPrefs.SetFloat("futurevar2",0);
        PlayerPrefs.SetFloat("futurevar3",0);
        Variables.coins = 0;
        Variables.level = 0;
        Variables.unlockedskins = unlockedSkins;
        Variables.SoccerHighScore=0;
        Variables.highscore=0;
        Variables.HighestBirdCount=0;
        Variables.HasSoccerHighScore=false;
        Variables.Wins=0;
        Variables.WorldSaved=false;
    }

    public static string pack(bool[] Array, string delimiter) {
        string temp ="";
        for (int i=0;i<Array.Length;i++){
            if (i < Array.Length-1)
                temp += Array[i]+delimiter;
            else
                temp += Array[i];
        }
        return temp;
    }
    public static bool[] unpack(string str, string delimiter){
        string[] substrings = str.Split(delimiter);
        bool[] array = new bool[substrings.Length];
        for(int i = 0; i < array.Length; i++){
            array[i] = bool.Parse(substrings[i]);
        }
        return array;
    }
    public static void SaveSkins(bool[] skins){
        bool[] unlockSkins = new bool[skins.Length];
        for(int i = 0; i < skins.Length; i ++){
            unlockSkins[i] = skins[i];
        }
        unlockedSkins = unlockSkins;
        Variables.unlockedskins = unlockedSkins;
        string SkinUnlocks = "";
        SkinUnlocks = pack(unlockedSkins,",");
        PlayerPrefs.SetString("unlockedskins",SkinUnlocks);

    }
   

    public static void Load(){
        Variables.coins = PlayerPrefs.GetFloat("CoinCount");
        Variables.level = PlayerPrefs.GetFloat("PlayerLevel");
        Variables.days = PlayerPrefs.GetFloat("dayScore");
        Variables.highscore = PlayerPrefs.GetFloat("HighScore");
        Variables.unlockedskins = unpack(PlayerPrefs.GetString("unlockedskins"),",");
        Variables.SoccerHighScore = PlayerPrefs.GetFloat("SoccerHighScore");
        Variables.HasSoccerHighScore=false;
        Variables.HighestBirdCount = PlayerPrefs.GetFloat("BirdAmounts");
        Variables.HighestLevel = PlayerPrefs.GetFloat("HighLevel");
        Variables.Wins = PlayerPrefs.GetFloat("Wins");
        Variables.xsensitivity = PlayerPrefs.GetFloat("CamSensx");
        Variables.ysensitivity = PlayerPrefs.GetFloat("CamSensy");
        Variables.camdistance = PlayerPrefs.GetFloat("CamDistance");

        Variables.dayvar1 = PlayerPrefs.GetFloat("dayvar1");
        Variables.dayvar2 = PlayerPrefs.GetFloat("dayvar2");
        Variables.dayvar3 = PlayerPrefs.GetFloat("dayvar3");
        Variables.dayvar4 = PlayerPrefs.GetFloat("dayvar4");
        Variables.dayvar5 = PlayerPrefs.GetFloat("dayvar5");

        Variables.futurevar1 = PlayerPrefs.GetFloat("futurevar1");
        Variables.futurevar2 = PlayerPrefs.GetFloat("futurevar2");
        Variables.futurevar3 = PlayerPrefs.GetFloat("futurevar3");
        if(PlayerPrefs.GetInt("SavedWorld")==1){
            Variables.WorldSaved=true;
        }
        else{
            Variables.WorldSaved=false;
        }
    }

    IEnumerator StartGame(){
        yield return new WaitForSeconds(0.5f);
        gameStarted = true;
        PlayerPrefs.SetInt("started",1);
    }

    void UnlockEverything(){
        unlockedSkins = new bool[40];
        for(int i=0;i<unlockedSkins.Length;i++){
            unlockedSkins[i] = true;
        }
        SaveSkins(unlockedSkins);
        PlayerPrefs.SetFloat("CoinCount", 10000);
        PlayerPrefs.SetFloat("PlayerLevel",200);
        PlayerPrefs.SetFloat("dayScore",0);
        PlayerPrefs.SetFloat("HighScore",200000);
        PlayerPrefs.SetFloat("SoccerHighScore",0);
        PlayerPrefs.SetFloat("BirdAmounts",10);
        PlayerPrefs.SetFloat("HighLevel",200);
        PlayerPrefs.SetFloat("Wins",1);
        PlayerPrefs.SetInt("SavedWorld",1);
        PlayerPrefs.SetFloat("CamSensx",400);
        PlayerPrefs.SetFloat("CamSensy",400);
        PlayerPrefs.SetFloat("CamDistance",6);
        PlayerPrefs.SetInt("started",1);
    Load();
    }
}
