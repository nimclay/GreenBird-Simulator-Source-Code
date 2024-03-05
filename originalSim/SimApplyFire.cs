using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimApplyFire : MonoBehaviour
{
    public StatusSim statusManager;
    public AudioSource source;
    public AudioClip fireSound;
    public float volume = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            statusManager.SetOnFire();
            if(!source.isPlaying){
            source.PlayOneShot(fireSound,volume);}
        }
    }

}
