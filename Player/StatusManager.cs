using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{

    public bool poison;
    public bool fire;
    public bool snow;
    public bool lightning;

    public float fireDamageSpeed;
    public float poisonDamageSpeed;

    public Image poisonImg;
    public Image snowImg;
    public Image fireImg;
    public Image lightningImg;

    float poisonDamage;
    float fireDamage;
    float fire_Effect;
    float snowEffect;
    float lightningEffect;

    HealthStamManager healthManager;

    public ParticleSystem poisonEffects;
    public ParticleSystem fireEffects;
    public ParticleSystem lightningEffects;

    public Color poisonColor;
    Color healthColour;

    float stack;
    // Start is called before the first frame update
    void Start()
    {
        healthManager = GetComponent<HealthStamManager>();
        poisonImg.enabled=false;
        healthColour = healthManager.healthBar.color;
        poisonEffects.gameObject.SetActive(false);
        fireEffects.gameObject.SetActive(false);
        lightningEffects.gameObject.SetActive(false);
        fireEffects.Stop();
        poisonEffects.Stop();
        lightningEffects.Stop();
        snowImg.enabled=false;
        fireImg.enabled=false;
        lightningImg.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(poison){
            healthManager.currentHealth-=poisonDamageSpeed*Time.deltaTime;
            if(!poisonEffects.isPlaying){
                poisonEffects.Play();}
            
        }
        if(fire){
            fireDamage+=fireDamageSpeed*Time.deltaTime;
            if(fireDamage>=1){
                fireDamage = 0;
                fire_Effect+=0.5f;
                healthManager.currentHealth-=fire_Effect;
            }
            if(!fireEffects.isPlaying){
                fireEffects.Play();
            }
            if(fire_Effect==1){
            putOutFire();
                    fireDamage=0;
                    fire_Effect=0;
            }

        }
        if(lightning){
            lightningEffect+=1*Time.deltaTime;
            if(lightningEffect>=4){
                StopElectric();
            }
        }
      
    }

    public void PoisonPlayer(){
        poison = true;
        poisonImg.enabled=true;
        poisonEffects.Play();
        poisonEffects.gameObject.SetActive(true);
        healthManager.healthBar.color = poisonColor;
        
    }
    public void CurePoison(){
        poison = false;
        poisonImg.enabled=false;
        poisonEffects.Stop();
        poisonEffects.gameObject.SetActive(false);
        healthManager.healthBar.color = healthColour;
    }


    public void OnFire(){
        fire=true;
        fireImg.enabled=true;
        
        fireEffects.Play();
        fireEffects.gameObject.SetActive(true);
    }

    public void putOutFire(){
        fire_Effect=0;
        fire=false;
        fireDamage=0;
        fireImg.enabled=false;
        fireEffects.Stop();
        fireEffects.gameObject.SetActive(false);
    }

    public void Electrify(){
        lightningEffect=0;
        lightning=true;
        GetComponent<movement>().Electrified=true;
        lightningEffects.Play();
        lightningImg.enabled=true;
        lightningEffects.gameObject.SetActive(true);

    }
    public void StopElectric(){
        lightningEffect=0;
        lightning=false;
        lightningEffects.Stop();
        GetComponent<movement>().Electrified=false;
        lightningImg.enabled=false;
        lightningEffects.gameObject.SetActive(false);
    }
}
