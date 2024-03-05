using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Predator : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject nest;
    public GameObject Player;
    Vector3 startPos;
    float distance;
    bool moving;
    bool home;
    public GameObject Den;
    interactable interCode;
    public eventManager[] Birds = new eventManager[4];

    float health = 5;

    public ParticleSystem Hurt;
    bool invinsible;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPos = transform.position;
        interCode = GetComponent<interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!moving){
       moving=true;
        agent.SetDestination(nest.transform.position);}

        if(Input.GetKeyDown(KeyCode.E)&&interCode.ObjInteractable){
           // home = true;
            //agent.SetDestination(Den.transform.position);
        }
        if(home&&Vector3.Distance (transform.position, Den.transform.position) <= agent.stoppingDistance+10){
            moving=false;
            home = false;
            
        }
        if(!home&&Vector3.Distance(transform.position,nest.transform.position) <= agent.stoppingDistance+1){
            home = true;
            agent.SetDestination(Den.transform.position);
            HurtBird();
        }

        if(GameData2.finalStand){
           // gameObject.SetActive(false);
        }
    }

    void OnParticleCollision(GameObject other){
        if(!home&&!invinsible){
            StartCoroutine(hurtDelay());
        health -=1;
        Hurt.Play();
        SuperBird.levelxp += 50;
        if(health<=0){
            health = 5;
            home = true;
            agent.SetDestination(Den.transform.position);
        }}
    }

    public IEnumerator hurtDelay(){
        invinsible = true;
        yield return new WaitForSeconds(0.2f);
        invinsible = false;
    }

    private void HurtBird(){
        List<int> list= new List<int>(); 
        for(int i=0;i<Birds.Length;i++){
            if(Birds[i].eggCode.collectedBool){
                list.Add(i);

            }
        }

        int x = Random.Range(0,list.Count);
        if(list.Count!=0){
            if(Birds[list[x]].playing){
        Birds[list[x]].LoseLife();}}
        
    }
}
