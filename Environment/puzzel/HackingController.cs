using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingController : MonoBehaviour
{

    public bool moveAble = false;
    Rigidbody rbody;

    public eventManager[] EventSystems;

    public MusicManager musicSystem;

    public movement PlayerMove;
    public Camera PlayerCam;
    public Camera thisCam;

    Vector3 startPos;

    public GameObject UI;

    public float speed = 10;
    public float jumpforce = 10;

    public float gravity = -20;
    

    public eventComplete eventCode;
    AudioSource audiosource;
    public AudioClip audioclip;
    public AudioClip completesound;

    public bool changeMusic = true;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        startPos = transform.position;
        audiosource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(PlayerMove.JumpKeyBind)||Input.GetKeyDown(PlayerMove.JumpKeyBindController)){
            rbody.velocity = new Vector3(rbody.velocity.x,0,0);
            rbody.AddRelativeForce(Vector3.up*jumpforce);
        }
    }

    void FixedUpdate(){
        if(moveAble){
        rbody.velocity = new Vector3(Input.GetAxis("Horizontal")*speed,rbody.velocity.y,0);
        rbody.AddRelativeForce(Vector3.up*gravity);}
        
    }

    public void StartThing(){
        PlayerMove.moveAble=false;
        PlayerCam.enabled = false;
        thisCam.enabled = true;
        PlayerMove.rbody.velocity = Vector3.zero;
        moveAble=true;
        UI.SetActive(false);
        for(int i = 0;i<EventSystems.Length;i++){
            if(EventSystems[i]!=null)
            EventSystems[i].playing=false;
        }
        if(changeMusic){
        musicSystem.ChangeSound("puzzel");}
    }

    public void EndThing(){
        UI.SetActive(true);
        moveAble=false;
        PlayerMove.moveAble=true;
        rbody.velocity = Vector3.zero;
        transform.position = startPos;
        PlayerCam.enabled = true;
        thisCam.enabled = false;
        if(eventCode!=null){
        eventCode.CompEvent();}
        for(int i = 0;i<EventSystems.Length;i++){
            if(EventSystems[i]!=null){
            EventSystems[i].playing=true;}
        }
        if(changeMusic){
        musicSystem.ChangeSound("default");}
        audiosource.PlayOneShot(completesound,Variables.volume);
    }

    void OnTriggerEnter(Collider other){
        if(other.tag=="enemy"){
            transform.position = startPos;
            //audiosource.PlayOneShot(audioclip,Variables.volume);
        }
        else if(other.tag=="Finish"){
            EndThing();
        }
    }
}
