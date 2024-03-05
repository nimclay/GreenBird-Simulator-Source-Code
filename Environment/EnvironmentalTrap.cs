using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalTrap : MonoBehaviour
{
    int x;

    public float eventTime;
    public GameObject EventEffects;
    public ParticleSystem Particles;
    bool eventbool;
    float timer;
    public GameObject dangerCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(eventbool){
            timer+=1*Time.deltaTime;

            if(timer>=eventTime){
                StopEvent();
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            x = Random.Range(0,2);
            Debug.Log("collider");
            if(x==1){
                StartEvent();
            }
        }
    }

    void StartEvent(){
        eventbool=true;
        timer=0;
        EventEffects.SetActive(true);
        StartCoroutine(danger());
        Particles.Play();
    }
    void StopEvent(){
        eventbool=false;
        timer=0;
        EventEffects.SetActive(false);
        Particles.Stop();
        dangerCollider.SetActive(false);
    }

    public IEnumerator danger(){
        yield return new WaitForSeconds(1f);
        dangerCollider.SetActive(true);
    }
}
