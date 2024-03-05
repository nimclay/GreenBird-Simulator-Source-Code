using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierButton : MonoBehaviour
{
    [SerializeField] private GameObject RemoveBarrier;
    [SerializeField] private float MaxTime;
    [SerializeField] private AudioClip clip;
    private AudioSource audiosource;
    [SerializeField] private Material pressedMat;
    [SerializeField] private eventManager eventData;
    private Material DefaultMat;
    public bool noTimer;

    private bool activated=false;
    // Start is called before the first frame update
    void Start()
    {
        DefaultMat = GetComponent<Renderer>().material;
        audiosource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(eventData!=null){if(eventData.BirdActive==false){
            activated = false;}
        }
    }

    void OnTriggerEnter(Collider other){
        if(!activated){
            activated = true;
            Pressed();
        }
    }
    void Pressed(){
        audiosource.PlayOneShot(clip,0.5f);
        GetComponent<Renderer>().material = pressedMat;
        if(RemoveBarrier!=null){
        RemoveBarrier.SetActive(false);}
        if(!noTimer){
        StartCoroutine(Timer());}
    }

    void Reset(){
        GetComponent<Renderer>().material = DefaultMat;
        activated = false;
        if(RemoveBarrier!=null){
        RemoveBarrier.SetActive(true);}
    }
    IEnumerator Timer(){
        yield return new WaitForEndOfFrame();
        while(activated){
        yield return new WaitForSeconds(MaxTime);}
        Reset();
    }
}
