using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalStandCode : MonoBehaviour
{
    public GameObject[] EnemyPrefab;

    public bool started;

    bool enemySpawn;

    public GameObject Nest;

    public GameObject[] spawnPoints;

    public float maxEnemy=30;
    public float currentEnemy=0;

    public Fade healthBarFade;

    public bool bossRushMode;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!started){
            if(GameData2.finalStand){started=true;
            StartEnd();}
        }
        else if(started){
            if(enemySpawn==false&&!bossRushMode){
            RandomEnemy();}

        }
    }


    public void RandomEnemy(){
        if(currentEnemy<maxEnemy){
        currentEnemy+=1;
        enemySpawn = true;
        int enemy = Random.Range(0,EnemyPrefab.Length);
        Vector3 spawnPos = GetSpawnPosition();
        Quaternion Rotation = Quaternion.Euler(Vector3.zero);
        Instantiate(EnemyPrefab[enemy],spawnPos,Rotation);
        StartCoroutine(Buffer());
    }}
   public  IEnumerator Buffer(){
        yield return new WaitForSeconds(1.5f);
        enemySpawn = false;
    }
    Vector3 GetSpawnPosition()
    {
        int ran = Random.Range(0,spawnPoints.Length);
        Vector3 pos = spawnPoints[ran].transform.position;
        return pos;
    }
    void StartEnd(){
        healthBarFade.ShowUI();
    }
}
