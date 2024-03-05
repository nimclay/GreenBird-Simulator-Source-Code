using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public bool sandBird;
    public bool volcBird;

    public GameObject Player;
    NavMeshAgent agent;

    bool attacking;

    Vector3 destination;
    bool hasDestination;
    public GameObject[] destinationPoints;
    Animator animations;

    bool DeActive;
    public float health=5;
    float currentHealth;

    public ParticleSystem Attacks;
    public ParticleSystem HurtEffect;
    bool attackingDistance;
    bool attackCoolDown;
    bool hurtable;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animations = transform.GetChild(0).GetComponent<Animator>();
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        animations.SetBool("Run",true);
        if(sandBird&&attacking){
            if(GameData2.SandBird){
                
                destination = Player.transform.position;
                hasDestination=false;
                
            }
            
        }
        if(volcBird&&attacking){
            if(GameData2.VolcBird){

            }
        }
        if(!attacking){
            
            if(!hasDestination){
                int ran = Random.Range(0,destinationPoints.Length);
            
            if(destinationPoints.Length!=0){
                hasDestination=true;
                destination = destinationPoints[ran].transform.position;}
            }
            
        }
        if (Vector3.Distance (transform.position, destination) <= agent.stoppingDistance) {
            hasDestination=false;
            animations.SetBool("Run",false);
            attackingDistance = true;
        }
        else{
            attackingDistance=false;
        agent.SetDestination(destination);}
        if(attacking&&attackingDistance){
           
            if(!attackCoolDown&&!DeActive){
                Attacks.Play();
                StartCoroutine(AttackDelay());
            }
        }
        if(DeActive){
            agent.SetDestination(transform.position);
        }
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            attacking=true;
        }
    }
    void OnTriggerExit(Collider other){
        if(other.tag=="Player"){
            attacking=false;
        }
    }
    IEnumerator AttackDelay(){
        attackCoolDown = true;
        yield return new WaitForSeconds(2f);
        attackCoolDown = false;
    }

    void OnParticleCollision(){
        if(!DeActive&&!hurtable){
            HurtEffect.Play();
            currentHealth-=1;
            StartCoroutine(hurt());
            if(currentHealth<=0){
                currentHealth=health;
                StartCoroutine(DeActivated());
            }
        }
    }
    IEnumerator DeActivated(){
        DeActive = true;
        yield return new WaitForSeconds(5f);
        DeActive = false;
        hasDestination=false;
    }
    IEnumerator hurt(){
        hurtable = true;
        yield return new WaitForSeconds(0.2f);
        hurtable=false;
    }
}
