using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static bool run_event;
    public static bool eggCollected=false;
    public static GameObject egg;
    public static bool NursingEgg;
    public static string eggType="";

    public static bool reloaded;


    public static float AmountOfSandBird=0, AmountOfGreenBird=0;

    public GameObject[] greenBirds;
    public movement movementCode;
    public inventory invcode;
    public static int[] itemAmount = new int[100];
    public static Vector3 StartPos = new Vector3(355,34,146);
    public static Vector3 StartRotation = new Vector3(0,0,0);

    float time;

    // Start is called before the first frame update
    void Start()
    {
       run_event=false;
       movementCode.gameObject.transform.position = StartPos; 
       if(reloaded){reloaded=false;
       UpdateGame();}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void quest1(){

    }
    public void UpdateGame(){
        for(int i=0;i<greenBirds.Length;i++){
            if(i<AmountOfGreenBird){
                greenBirds[i].SetActive(true);
            }
        }
        if(AmountOfGreenBird>=1){
            //movementCode.dashJump = true;
        }
        for(int i=0;i<invcode.items.Length;i++){
            invcode.items[i].itemAmount = itemAmount[i];
        }

    }

    public void saveGame(){
        for(int i=0;i<invcode.items.Length;i++){
            itemAmount[i] = invcode.items[i].itemAmount;
        }
        StartPos = movementCode.gameObject.transform.position;
    }
}
