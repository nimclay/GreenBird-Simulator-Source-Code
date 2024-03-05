using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SkinButtons : MonoBehaviour
{
    public SkinManager manager;
    public bool next;
    public bool prev;
    public bool equip;
    public bool purchase;
    public bool perform;
    public bool time;
    public float timeSpeed;

    private bool pressable = true;
    public bool resetGameData;

    public AudioSource source;
    public AudioClip clip;


    public PostProcessLayer lay;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"&&pressable){
            StartCoroutine(buffer());
            DoThing();
        }
    }
    IEnumerator buffer(){
        pressable = false;
        yield return new WaitForSeconds(0.2f);
        pressable = true;
    }
    void DoThing(){
        source.PlayOneShot(clip,Variables.volume);
        if(prev){
            manager.Prev();
        }
        else if(purchase){
            manager.Purchase();
        }
        else if(next){
            manager.Next();
        }
        else if(equip){
            manager.Equip();
        }
        else if(resetGameData){
            SaveGame.reset();
        }
        else if(perform){
            Variables.postProcessing = !Variables.postProcessing;
            lay.enabled = Variables.postProcessing;
        }
        else if(time){
            Time.timeScale = timeSpeed;
        }
    }
    
}
