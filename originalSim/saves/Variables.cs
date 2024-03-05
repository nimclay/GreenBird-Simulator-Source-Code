using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    public static float coins;
    public static float level;
    public static float days;
    public static bool[] unlockedskins; 

    //high scores
    public static float SoccerHighScore;
    public static float highscore;
    public static float HighestBirdCount;
    public static float HighestLevel;

    public static bool HasSoccerHighScore;
    public static float Wins;

    public static bool WorldSaved;


    public static float volume = 0.5f;
    public static float musicVolume = 0.8f;



    public static float xsensitivity = 400f;
    public static float ysensitivity = 400f;
    public static float camdistance = 6;

    public static Material trailMaterial;
    public static Material AuraMaterial;


    //player stats
    public static float bonusMoveSpeed=0;
    public static float bonusDamage=0;
    public static float bonusJumpHeight=0;

    //mission stats
    public static float bonusMissionTime=0;
    public static float bonusHealth=0;
    public static float bonusTimeDegradation=0;
    public static float bonusDifficulty=0;

    public static int unlockskin;



    public static float dayvar1 = 0;
    public static float dayvar2 = 0;
    public static float dayvar3 = 0;
    public static float dayvar4 = 0;
    public static float dayvar5 = 0;

    public static float futurevar1 = 0;
    public static float futurevar2 = 0;
    public static float futurevar3 = 0;


    public static bool postProcessing = true;

    // Start is called before the first frame update
    void Start()
    {
        HasSoccerHighScore=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetSkin(int num){
        unlockedskins[num] = true;
    }
}
