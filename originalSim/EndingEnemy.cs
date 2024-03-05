using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EndingEnemy : MonoBehaviour
{
    public GameObject[] targets;
    bool moving;
    public float health=3;
    public ParticleSystem Hurt;
    public NavMeshAgent agent;
    GameObject target;
    public GameObject Player;

    bool noMoreBirds = false;

    public bool meleeEnemy = true;
    public bool bomber;
    bool attacking = false;
    public float attackInitialDelay = 0.5f;
    public float attackCoolDown = 2;
    public ParticleSystem MeleeAttack;

    public FinalStandCode code;

    public bool OnlyPlayer;
    public SuperBird superBird;
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameData2.BossFight){
        if(!moving&&!OnlyPlayer){
            GetDestination();
        }
        else{
            if(!OnlyPlayer){
            if(target!=null){
            if(Vector3.Distance (transform.position, target.transform.position) <= agent.stoppingDistance+3){
                target.SetActive(false);
                Die();
            }}}
        }
        if(noMoreBirds||OnlyPlayer){
            agent.SetDestination(Player.transform.position);
            if(Vector3.Distance (transform.position, Player.transform.position) <= agent.stoppingDistance){
                if(!attacking){
                    attack();
                }
            }
            if(attacking){
                agent.isStopped = true;
            }
            else{
                agent.isStopped = false;
            }
        }}
    }
    bool invinsible;
    void OnParticleCollision(GameObject other){
        if(!invinsible){
            StartCoroutine(hurtDelay());
            health -=1;
            superBird.GainHyper();
            SuperBird.levelxp += 10;
            Hurt.Play();
            if(health<=0){
                Die();
                health = 10;
            
            }
        }
    }

    public IEnumerator hurtDelay(){
        invinsible = true;
        yield return new WaitForSeconds(0.2f);
        invinsible = false;
    }

    void Die(){
        code.currentEnemy -=1;
        eventManager.score +=100;
        SuperBird.levelxp += 50;
        Destroy(this.gameObject);
    }

    void GetDestination(){
        if(!noMoreBirds){
        moving = true;
        List<GameObject> ActiveTargets = new List<GameObject>();
        for(int i = 0; i < targets.Length; i++){
            if(targets[i]!=null){
            if(targets[i].activeSelf){
                ActiveTargets.Add(targets[i]);
            }}

        }
        if(ActiveTargets.Count!=0){
        int ran = Random.Range(0,ActiveTargets.Count);
        target = ActiveTargets[ran];
        agent.SetDestination(target.transform.position);}
        else{
            noMoreBirds = true;
        }}
    }

    void attack(){
        if(!attacking){
        StartCoroutine(AttackDelay());}
    }

    IEnumerator AttackDelay(){
        attacking=true;
        yield return new WaitForSeconds(attackInitialDelay);
        if(meleeEnemy){
         MeleeAttack.Play();attacking=true;}
         else{if(bomber){
            MeleeAttack.Play();
         }
         }
        yield return new WaitForSeconds(attackCoolDown);
        attacking = false;
        if(bomber){
            Die();
        }
    }
}
