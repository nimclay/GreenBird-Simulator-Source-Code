using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyBehaviour : MonoBehaviour
{
    Vector3 position;
    Vector3 Rotation;
    public string enemyType;
    public NavMeshAgent agent;
    Transform Player;

    MusicManager musicmanager;

    bool activated;
    // Start is called before the first frame update
    void Start()
    {
        position = transform.localPosition;
        Player = GameObject.FindWithTag("Player").transform;
        Rotation = new Vector3(transform.rotation.x,transform.rotation.y,transform.rotation.z);
        musicmanager=GameObject.FindObjectOfType<MusicManager>().GetComponent<MusicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameData.eggType==enemyType&&!activated){
            StartAgro();
        }
        

        if(activated){
            agent.SetDestination(Player.position);
            if(GameData.eggType==""){
                StopAgro();
            }
        }
    }

    void StartAgro(){
        agent.SetDestination(Player.position);
        agent.isStopped=false;
        musicmanager.ChangeSound("run");
        GetComponent<interactable>().enabled=true;
        activated=true;
    }

    void StopAgro(){
        agent.isStopped=true;
        musicmanager.ChangeSound("default");
        activated=false;
        transform.localPosition = position;
        transform.rotation = Quaternion.Euler(Rotation);
    }

    
}
