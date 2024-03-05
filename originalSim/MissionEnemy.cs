using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionEnemy : MonoBehaviour
{

    public eventComplete completeCode;

    public float maxHealth = 3;
    private float currentHealth;

    public GameObject removeObject;

    Collider col;

    public ParticleSystem AttackEffect;
    public ParticleSystem hitEffect;
    public float attackDelay=2;

    private bool attacking=false;
    private bool inRange;

    public float hitDelay = 0.15f;

    public float resetTimer = 10;
    private bool invinsible;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange){
            if(!attacking){
                Attack();
            }
        }
    }


    void Attack(){
        if(!attacking){
            attacking = true;
            AttackEffect.Play();
            StartCoroutine(AttackDelay());
        }
    }
    IEnumerator AttackDelay(){
        attacking=true;
        yield return new WaitForSeconds(attackDelay);
        attacking = false;
    }
    IEnumerator killdelay(){
        invinsible = true;
        yield return new WaitForSeconds(hitDelay);
        invinsible = false;
    }

    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            inRange=true;
        }
    }
    void OnTriggerExit(Collider other){
        if(other.tag=="Player"){
            inRange = false;
        }
    }

    void OnParticleCollision(){
        if(!invinsible){
            invinsible = true;

            StartCoroutine(killdelay());
            currentHealth -= (1+Variables.bonusDamage);
            hitEffect.Play();
            if(currentHealth<=0){
                KillThing();
            }
        }
    }

    void KillThing(){
        removeObject.SetActive(false);
        col.enabled=false;
        inRange=false;
        attacking=false;
        invinsible=false;
        completeCode.CompEvent();
        currentHealth = maxHealth;
        eventManager.score += 30;
        SuperBird.levelxp += 100;

        StartCoroutine(Reset());
    }

    IEnumerator Reset(){
        yield return new WaitForSeconds(resetTimer);
        removeObject.SetActive(true);
        col.enabled=true;

    }
}
