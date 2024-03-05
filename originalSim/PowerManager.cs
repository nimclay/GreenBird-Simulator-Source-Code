using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{

    public PowerButton[] PowerButtons;

    bool activor;

    public GameObject invertedEnemy;

    public float enemyCount = 0;
    public float maxEnemy = 5;

    public GameObject[] SpawnLocations;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activor){
            SpawnEnemy();
        }
    }

    public void Check(){
        if(!activor){
            float unpowered = 0;
            for(int x = 0; x < PowerButtons.Length; x++){
                if(PowerButtons[x].actived == false){
                    unpowered += 1;
                }
            }
            if(unpowered == PowerButtons.Length){
                Activate();
            }
        }
        else{
            float powered = 0;
            for(int i = 0 ; i < PowerButtons.Length; i++){
                if(PowerButtons[i].actived == true){
                    powered += 1;
                }
            }
            if(powered == PowerButtons.Length){
                DeActivate();
            }
        }
    }

    void DeActivate(){
        activor = false;
    }

    void Activate(){
        activor = true;
    }

    void SpawnEnemy(){
        if(enemyCount!=maxEnemy){
            enemyCount += 1;
            Vector3 position = Pos();
            GameObject enemy = Instantiate(invertedEnemy, position,invertedEnemy.transform.rotation);
            if(enemyCount >= maxEnemy){
                enemyCount = maxEnemy;
            }
        }
    }
    Vector3 Pos(){
        int ran = Random.Range(0,SpawnLocations.Length);
        Vector3 pos = new Vector3(SpawnLocations[ran].transform.position.x,SpawnLocations[ran].transform.position.y,SpawnLocations[ran].transform.position.z);
        return pos;
    }
}
