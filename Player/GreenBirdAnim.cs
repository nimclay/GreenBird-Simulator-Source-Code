using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBirdAnim : MonoBehaviour
{
    Animator anim;
    public movement playerCode;
    bool dead;
    public HealthStamManager healthCode;
    bool skating=false;

    public bool Sim=false;

    public AudioSource WalkingSource;
    public AudioClip walkingClip;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if(!dead){
            skating = playerCode.skating;
                anim.SetBool("skate",skating);
            
        if(!playerCode.dashing){
       anim.SetBool("jump",!playerCode.grounded);
        
        anim.SetBool("skate",skating);
       }
       else{
           anim.SetBool("jump",false);
       }
       anim.SetBool("Dash",playerCode.dashing);
        if((Input.GetAxis("Vertical")!=0||Input.GetAxis("Horizontal")!=0)&&playerCode.grounded&&!playerCode.dashing&&!playerCode.skating){
            if(!anim.GetBool("Dying")){
            anim.SetBool("Run",true);
            anim.SetBool("jump",false);}
        }
        else{
            anim.SetBool("Run",false);
        }
        }
        if(playerCode.sprinting){
            anim.SetBool("Run",false);
            anim.SetBool("Sprint",playerCode.sprinting);
        }
        else{
            anim.SetBool("Sprint",playerCode.sprinting);
        }

        if(anim.GetBool("Run")==true){
            if(!WalkingSource.isPlaying&&playerCode.moveAble){
                WalkingSource.PlayOneShot(walkingClip,Variables.volume+0.2f);
            }
        }
        else{
            WalkingSource.Stop();
        }
    }

    public void Die(){
        StartCoroutine(dying());
        anim.SetBool("Run",false);
        anim.SetBool("jump",false);
    }

    public IEnumerator dying(){
        dead=true;
        playerCode.moveAble=false;
        anim.SetBool("jump",false);
        anim.SetBool("Dying",true);
        anim.SetBool("Run",false);
        yield return new WaitForSeconds(2.5f);
        playerCode.moveAble=true;
        anim.SetBool("Dying",false);
        dead=false;
        if(!Sim){
        healthCode.KillPlayer2();}
    }
}
