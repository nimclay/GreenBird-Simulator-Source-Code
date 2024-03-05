using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbEnemy : MonoBehaviour
{
    public float health = 3;
    private bool invinsible;
    private float invinsibilitytimer;
    public GameObject Player;
    Rigidbody rbody;
    float speed=1;
    Vector3 PlayerDirection;
    float distance;
    Vector3 normalized;
    public bool knockbackguy, swordguy, explosionguy;
    public ParticleSystem Attack;
    public float weaponCoolDown=1f;
    bool attackDelay;
    float counter;
    public ParticleSystem hurtEffects;

    bool dashing;
    float dashtimer;

    public bool moving;
    bool ability;
    int ranNumAbility;
    public ArenaStarter arenaCode;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();}

    // Update is called once per frame
    void Update()
    {
        if(moving){
        if(dashing){
            dashtimer+=1*Time.deltaTime;
            if(dashtimer>=2){
                dashing=false;
                dashtimer=0;
            }
        }

        if(invinsible){
            invinsibilitytimer+=1*Time.deltaTime;
            if(invinsibilitytimer>=0.5f){
                invinsible=false;
                invinsibilitytimer=0;}}

        PlayerDirection = Player.transform.position - transform.position;
        distance = Vector3.Distance(Player.transform.position, transform.position);
        
        //knockbackguy behaviour
        if(knockbackguy){
            speed = 20;
            if(distance<=10&&Attack.isPlaying==false&&!attackDelay){
            attackDelay=true;Attack.Play();}
            if(distance>=80){
                teleporttoplayer();
            }
        }

        // sword guy behaviour
        else if(swordguy){
            if(distance<=8&&Attack.isPlaying==false&&!attackDelay){
                attackDelay=true;Attack.Play();}
            if(distance>=30){
                if(!ability){
                ranNumAbility = Random.Range(1,1000);}
                speed=5;
                rbody.drag=3;
            }
            else{
                if(25>=distance&&distance>=10){
             
                    if(!dashing){
                    DashForward();}
                }
                else if(distance<=9){
                    if(!dashing){
                        DashSide();}
                }
            }
            if(ranNumAbility==5){ability=true;}
        }

        //explosionguy behaviour
        else if(explosionguy){
            if(distance>=20){speed=1;}
        else{speed=1;}
         if(distance<=10&&Attack.isPlaying==false&&!attackDelay){
            attackDelay=true;Attack.Play();}
        }

        //attack reset
        if(attackDelay){counter+=1*Time.deltaTime;
            if(counter>=weaponCoolDown){counter=0;attackDelay=false;}}

        transform.LookAt(Player.transform.position);

        //ability management
        if(ability){
            if(swordguy){
                StartCoroutine(SwordGuyAbility());
            }
            else if(explosionguy){

            }
            else if(knockbackguy){

            }
        }}
    }
    void FixedUpdate(){
        if(moving){
        rbody.AddForce(new Vector3(PlayerDirection.x*speed,0,PlayerDirection.z*speed));
        
        if(rbody.velocity.magnitude>10){
            rbody.AddForce(new Vector3(rbody.velocity.normalized.x*-speed,0,rbody.velocity.normalized.z*-speed));
        }}
        
    }

    void OnParticleCollision(GameObject other){
        if(!invinsible){
        invinsible=true;
        GetHurt(1+Variables.bonusDamage);
        }
    }
   
    void GetHurt(float damage){
        health -=damage;
        hurtEffects.Play();
        if(health<=0){
            KillEnemy();}
    }
    void KillEnemy(){
        arenaCode.AmountOfEnemies -= 1;
        arenaCode.KilledEnemies +=1;
      
        Destroy(gameObject);
    }

    void DashForward(){
        dashing=true;rbody.AddRelativeForce(Vector3.forward*rbody.mass*speed);
    }
    void DashSide(){
        dashing=true;
        float side = Random.Range(1,2);
        if(side==1){
            rbody.AddRelativeForce(Vector3.left*rbody.mass*speed);}
        else if(side==2){
            rbody.AddRelativeForce(Vector3.right*rbody.mass*speed);}
    }
    void teleporttoplayer(){transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y+5,Player.transform.position.z);}

    IEnumerator SwordGuyAbility(){
        ability = false;
        yield return new WaitForEndOfFrame();
        moving=false;
        yield return new WaitForSeconds(0.5f);
        rbody.AddRelativeForce(Vector3.right*rbody.mass*speed);
        yield return new WaitForSeconds(1f);
        rbody.AddRelativeForce(Vector3.forward*rbody.mass*speed);
        rbody.AddRelativeForce(Vector3.left*rbody.mass);
        moving = true;
        yield return new WaitForEndOfFrame();
    }
    
}
