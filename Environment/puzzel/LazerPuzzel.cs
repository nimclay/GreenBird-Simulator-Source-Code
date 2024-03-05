using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerPuzzel : MonoBehaviour
{
    public TrailRenderer lazerTrail;
    interactable interCode;
    public bool lazerActive;
    public bool reciever;
    public LazerPuzzel recieverObject;
    float timer;
    public float maxTime=10;

    public RaceRings rings;
    public bool LazerComplete;
    public GameObject removeObject;
    // Start is called before the first frame update
    void Start()
    {
        lazerTrail.emitting = false;
        interCode = GetComponent<interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(interCode.ObjInteractable){
            if(reciever){
                if(recieverObject.lazerActive&&recieverObject.LazerComplete){
                    recieverObject.StopEvent();
                    winEvent();
                }
            }
            else{
                if((Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.JoystickButton0))&&!lazerActive){
            lazerTrail.emitting = true;
            lazerActive = true;
            rings.NextRing();}}

        }
        if(lazerActive){
            timer +=1*Time.deltaTime;
            if(timer>=maxTime){
                StopEvent();
            }
        }
        else{
            LazerComplete = false;
        }
    }

    public void StopEvent(){
        lazerActive = false;
        lazerTrail.emitting = false;
        timer = 0;
        rings.ResetRings();
    }
    public void winEvent(){
        removeObject.SetActive(false);
        StopCoroutine(ResetObject());
        StartCoroutine(ResetObject());
    }
    IEnumerator ResetObject(){
        yield return new WaitForSeconds(20f);
        removeObject.SetActive(true);
    }
}
