using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData2 : MonoBehaviour
{
    public static bool run_event;
    public static bool eggCollected=false;
    public static GameObject egg;
    public static bool NursingEgg;
    public static string eggType="";

    public static bool reloaded;

    public static bool SandBird;
    public static bool VolcBird;
    public static bool SnowBird;
    public static bool GreenBird;
    public static float AmountOfSandBird=0, AmountOfGreenBird=0,AmountOfSnowBird, AmountOfVolcBird;

    public GameObject[] greenBirds;
    public GameObject[] snowBirds;
    public GameObject[] sandBirds;
    public GameObject[] volcBirds;
    public movement movementCode;
    public inventory invcode;
    public static int[] itemAmount = new int[100];
    public static Vector3 StartPos = new Vector3(355,34,146);
    public static Vector3 StartRotation = new Vector3(0,0,0);
    public static bool finalStand;
    public static bool BossFight;
    public bool greenStart=true;

    float time;

    public bool StartSuper;
    void Start(){
        BossFight=false;
        SandBird = false;
        VolcBird = false;
        SnowBird=false;
        reloaded=false;
        eggType="";
        NursingEgg=false;
        egg = null;
        eggCollected=false;
        AmountOfGreenBird=0;
        AmountOfSandBird=0;
        AmountOfSnowBird=0;
        AmountOfVolcBird=0;
        GreenBird = greenStart;
        
        ResetBirds();
        finalStand=false;
        if(StartSuper){
            FinalStand();
        }
    }


    public void GainGreenBird(){
        AmountOfGreenBird+=1;
        for(int i=0;i<=AmountOfGreenBird-1;i++){
            if(i<greenBirds.Length){
            greenBirds[i].SetActive(true);}
        }
    }
    public void GainSandBird(){
        AmountOfSandBird+=1;
        for(int i=0;i<=AmountOfSandBird-1;i++){
            if(i<sandBirds.Length){
            sandBirds[i].SetActive(true);}
        }
    }
    public void GainVolcBird(){
        AmountOfVolcBird+=1;
        for(int i=0;i<=AmountOfVolcBird-1;i++){
            if(i<volcBirds.Length){
            volcBirds[i].SetActive(true);}
        }
    }
    public void GainSnowBird(){
        AmountOfSnowBird+=1;
        for(int i=0;i<=AmountOfSnowBird-1;i++){
            if(i<snowBirds.Length){
                if(snowBirds[i]!=null){
            snowBirds[i].SetActive(true);}}
        }
    }

    public void ResetBirds(){
        for(int i=0;i<5;i++){
            if(volcBirds[i]!=null){
            volcBirds[i].SetActive(false);}
            if(sandBirds[i]!=null){
            sandBirds[i].SetActive(false);}
            if(greenBirds[i]!=null){
            greenBirds[i].SetActive(false);}
            if(snowBirds[i]!=null){
            snowBirds[i].SetActive(false);}
        }
    }

    public static void TestEggs(){
        if(!SandBird&&!GreenBird&&!VolcBird&&!SnowBird){
            FinalStand();
        }
    }
    public static void FinalStand(){
        finalStand=true;
    }
}

