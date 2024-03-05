using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InvertEnemy : MonoBehaviour
{
    public GameObject Player;
    public PowerManager power;

    private NavMeshAgent agent;
    bool attackable = true;

    public float maxHealth = 2;
    private float currentHealth;
    private bool invinsible = false;

    public GameObject shield;

    public ParticleSystem attack;
    public ParticleSystem hurt;

    public float attackDelay = 1f;
    public float initialDelay = 0.5f;
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
        NavMeshHit hit;
 NavMesh.SamplePosition(Player.transform.position, out hit, 20, NavMesh.AllAreas);
 Vector3 finalPosition = hit.position;
agent.SetDestination(finalPosition);
        if(Vector3.Distance (transform.position, Player.transform.position) <= agent.stoppingDistance){
            agent.SetDestination(transform.position);
            Attack();
        }
    }

    void killed(){
        if(power!=null){
        power.enemyCount -= 1;}
        SuperBird.levelxp += 100;
        StopCoroutine(attackCoolDown());
        StopCoroutine(IFrames());
        Destroy(gameObject);
    }

    void Attack(){
        if(attackable){
           
            StartCoroutine(attackCoolDown());
        }
    }
    IEnumerator attackCoolDown(){
        attackable = false;
        yield return new WaitForSeconds(initialDelay);
        attack.Play();
        yield return new WaitForSeconds(attackDelay);
        attackable = true;
    }

    void TakeDamage(){
        if(!invinsible){
            invinsible = true;
        currentHealth -= (1+Variables.bonusDamage);
        hurt.Play();
        StartCoroutine(IFrames());
        
        if(currentHealth<=0){
            killed();
        }}
    }
    IEnumerator IFrames(){
        invinsible = true;
        shield.SetActive(true);
        yield return new WaitForSeconds(2f);
        invinsible = false;
        shield.SetActive(false);
    }

    void OnParticleCollision(GameObject other){
        if(!invinsible){
        TakeDamage();}
    }

}
