using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorCrystal : MonoBehaviour
{
    public float health = 3;
    private float currentHealth;

    public AudioClip hitSound;
    public AudioClip breakSound;
    private AudioSource source;
    public float volume = 0.5f;
    public VolcBirdPortal anchor;

    private bool invinsible=false;
    public bool living = true;

    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(){
        if(!invinsible){
            StartCoroutine(hitDelay());
        currentHealth -= 1;
        if(currentHealth <=0){
            source.PlayOneShot(breakSound,volume);
            StartCoroutine(anchor.delay());
            living = false;
            GetComponent<Collider>().enabled = false;
            GetComponent<Renderer>().enabled = false;
        }
        else{
            source.PlayOneShot(hitSound,0.5f);
        }
        }
    }

    IEnumerator hitDelay(){
        invinsible = true;
        yield return new WaitForSeconds(0.2f);
        invinsible = false;
    }
}
