using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaStarter : MonoBehaviour
{

    private bool delayed=false;

    public float MaxTime;


    bool ArenaActive;

    public GameObject[] enemies;
    public GameObject[] enemySpawns;

    public List<GameObject> SpawnedEnemies;
    public float AmountOfEnemies;
    public float maxEnemies;
    private bool enemySpawned=false;

    bool ArenaEnded=false;
    private interactable interCode;

    public GameObject StartThing;
    public float KilledEnemies;

    public GameObject finishObject;
    // Start is called before the first frame update
    void Start()
    {
        interCode = GetComponent<interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(KilledEnemies >= 10){
            EndEvent();
        }
        if(interCode.ObjInteractable&&(Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.JoystickButton0))){
           
                if(!delayed){
                StartCoroutine(Delay());
               
                ArenaActive=!ArenaActive;
                if(!ArenaActive){
                    ArenaEnded = true;
                }
                else{
                    if(interCode.ui!=null){
                        Destroy(interCode.ui);
                    }
                    ArenaEnded=false;
                    GetComponent<Collider>().enabled = false;
                    StartThing.SetActive(false);
                }
            }
            
        }
        if(ArenaActive){
            if(!enemySpawned){
                SpawnEnemy();
            }
        }
        else{
            if(ArenaEnded){
                ArenaEnded=false;

                for(int i = 0; i<SpawnedEnemies.Count;i++){
                    if(SpawnedEnemies[i]!=null){
                        Destroy(SpawnedEnemies[i]);
                    }
                }
                SpawnedEnemies = new List<GameObject>();
            }
        }
    }

    

    IEnumerator Delay(){
        delayed = true;
        yield return new WaitForSeconds(0.2f);
        delayed = false;
    }
    IEnumerator enemyDelay(){
        enemySpawned = true;
        yield return new WaitForSeconds(1f);
        enemySpawned = false;
    }

    private void SpawnEnemy(){
        AmountOfEnemies+=1;
        if(AmountOfEnemies>=maxEnemies+1){
            AmountOfEnemies = maxEnemies;
        }
        else{
        int ran1 = Random.Range(0,enemies.Length);
        int ran2 = Random.Range(0,enemySpawns.Length);
        GameObject spawnedEnemy = Instantiate(enemies[ran1],enemySpawns[ran2].transform.position,Quaternion.Euler(Vector3.zero));
        spawnedEnemy.GetComponent<OrbEnemy>().moving = true;
        SpawnedEnemies.Add(spawnedEnemy);
        StartCoroutine(enemyDelay());}


    }

    void EndEvent(){
        ArenaActive = false;
        GetComponent<Collider>().enabled = true;
        StartThing.SetActive(true);
        KilledEnemies = 0;
        AmountOfEnemies = 0;
        finishObject.SetActive(true);
        SuperBird.levelxp += 600;
        eventManager.score += 1000;
        ArenaEnded=true;
    }
}
