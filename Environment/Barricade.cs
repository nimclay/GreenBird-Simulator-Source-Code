using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public GameObject RemoveObject;
    public float maxHealth=5;
    float health=5;

    bool invinsible;
    public ParticleSystem HitEffects;

    private AudioSource source;
    public AudioClip hitSound;
    public float volume = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        RemoveObject = this.gameObject;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(){
        if(!invinsible){
            StartCoroutine(CoolDown());
            HitEffects.Play();
            source.PlayOneShot(hitSound,volume);
            health -= 1;
            if(health<=0){
                StartCoroutine(breakThing());
            }
        }
    }
    IEnumerator CoolDown(){
        invinsible = true;
        yield return new WaitForSeconds(0.3f);
        invinsible = false;
    }
   public  IEnumerator breakThing(){
        yield return new WaitForEndOfFrame();
        health = maxHealth;
        RemoveObject.GetComponent<Renderer>().enabled = false;
        RemoveObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(20f);
        RemoveObject.GetComponent<Renderer>().enabled = true;
        RemoveObject.GetComponent<Collider>().enabled = true;
        
    }
}
