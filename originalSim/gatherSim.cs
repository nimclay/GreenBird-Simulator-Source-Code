using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gatherSim : MonoBehaviour
{
    public eventComplete eventCode;
    interactable interCode;

    public ParticleSystem collectEffect;
    public AudioSource soundSource;
    public AudioClip collectSound;

    public GameObject removeThing;
    public float gatherTime;

    public bool noeffects;

    public bool noXP = false;


    private bool objActive=true;
    public float EXPAmount=80;

    public bool hasDelay = true;

    public bool startOff=false;
    public bool alwaysAccessible;


    public bool coinThing;
    public CoinManager coinManager;
    public float coinAmount = 1;
    [Header("HealthThings")]
    [SerializeField] private bool HealthItem;
    [SerializeField] private int GainHealth=1;
    [SerializeField] private eventManager[] events;

    [Header("Scorething")]
    [SerializeField] private float Score;
    [SerializeField] private bool Scorething;
    // Start is called before the first frame update
    void Start()
    {
        interCode = GetComponent<interactable>();
        if(startOff){
            DeActivate();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(interCode.ObjInteractable&&objActive){
            if((Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.JoystickButton0))){
                if(!interCode.Player.GetComponent<SuperBird>().supermode||alwaysAccessible){
                Effects();
                DeActivate();
                if(Scorething){
                    eventManager.score+=Score;
                }
                if(HealthItem){
                    for(int i = 0; i<events.Length; i++){
                        if(GainHealth==(-1)){
                            events[i].LoseLife();
                        }
                        else{events[i].health += GainHealth;
                        if(events[i].health>3){
                            events[i].health = 3;
                        }
                        events[i].timer=0;
                        events[i].healthBar.fillAmount = 0.33f*events[i].health;}
                    }
                }
                if(coinThing){
                    coinManager.GainCoins(coinAmount);
                }
                if(!noXP){
                SuperBird.levelxp += EXPAmount;}
                eventCode.CompEvent();}
            }
        }
    }
    void Effects(){
        if(!noeffects){
        collectEffect.Play();
        soundSource.PlayOneShot(collectSound,Variables.volume);}
    }

    void DeActivate(){
        objActive=false;
        if(removeThing!=null){
            removeThing.SetActive(false);}
        if(hasDelay){
            GetComponent<Collider>().enabled = false;}
        interCode.triggered=false;
        interCode.ObjInteractable=false;
        if(hasDelay){
            if(startOff){
                float time = Random.Range(0,gatherTime*2);
                StartCoroutine(Activate(time));
            }
            else{StartCoroutine(Activate(gatherTime));}}
        else{if(removeThing!=null){
            removeThing.SetActive(true);}
        GetComponent<Collider>().enabled = true;
        objActive=true;}
    }
    public IEnumerator Activate(float time){
        yield return new WaitForSeconds(time);
        if(removeThing!=null){
            removeThing.SetActive(true);}
        GetComponent<Collider>().enabled = true;
        objActive=true;
        
    }

}
