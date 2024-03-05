using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHoop : MonoBehaviour
{

    public BallGame BallGameManager;

    public bool isRed;

    public Material ActiveMat;
    public Material DeActiveMat;

    public bool goal;

    AudioSource source;
    public AudioClip clip;
    public AudioClip dieClip;
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
        if(other.tag == "BallThing"){
            
            if(BallGameManager.CurrentBallIsRed){
                if(isRed){
                    if(goal){
                    GetScoreGoal();}
                    else{
                        GetScore();
                    }
                }
                else{
                    RemoveScore();
                }
            }
            else{
                if(isRed){
                    RemoveScore();
                }
                else{
                    if(goal){
                        GetScoreGoal();
                    }
                    else{GetScore();}
                }
            }
        }
    }
    void TurnOff(){
        GetComponent<Collider>().enabled = false;
        transform.parent.transform.GetChild(0).GetComponent<Renderer>().material = DeActiveMat;
    }

    void GetScore(){
        TurnOff();
        BallGameManager.GetScore(1);
        source.PlayOneShot(clip,0.5f);
        
    }

    void RemoveScore(){
        BallGameManager.KillBall();
        source.PlayOneShot(dieClip,0.5f);
        BallGameManager.RemoveScore();
    }

    public void TurnOn(){
        GetComponent<Collider>().enabled = true;
        transform.parent.transform.GetChild(0).GetComponent<Renderer>().material = ActiveMat;
    }
    void GetScoreGoal(){
        BallGameManager.KillBall();
        BallGameManager.GetScore(2);
        source.PlayOneShot(clip,0.5f);
    }
}
