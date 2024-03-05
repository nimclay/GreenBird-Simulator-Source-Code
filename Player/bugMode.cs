using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bugMode : MonoBehaviour
{
    public bool moveAble=false;
    public Transform cam;
    Rigidbody rbody;

    
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
    Vector3 targetVelocity;
     public float turnsmoothtime = 0.1f;
    float turnsmoothvelocity;

    //float jumps=2;
    public float maxjumps=2;
    public float jumpForce = 10;
    public float doubleJumpForce = 3;
    public bool grounded;
    public float turnSpeed=10;
    public float moveSpeed = 100;
    float speed;

    public float MaxSpeed=30;
    float maxSpeed;

    public float boostSpeed=50;
    public float boostForce=15000;

    public TrailRenderer BoostEffects;

    float turn;
    public float turnForceNorm=40;
    public float driftForce=7000;

    bool drift;
    public ParticleSystem[] boostEffects;
    public ParticleSystem driftEffect;

    public WheelCollider[] wheels;
    float timer;

    float duration;

    bool driftable=true;
    bool boosting;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        maxSpeed = MaxSpeed;
        speed = moveSpeed;
        turn = turnSpeed;
        rbody.centerOfMass += new Vector3(0, -0.10f,0); 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot.x = ClampAngle(rot.x,-60,60);
        rot.z = ClampAngle(rot.z,-60,60);
        transform.rotation = Quaternion.Euler(rot);
        float targetAngle = Mathf.Atan2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"))*Mathf.Rad2Deg+cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnsmoothvelocity, turnsmoothtime);
        if(!drift){
            
       transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, Input.GetAxis("Horizontal") * turnForceNorm* Time.deltaTime, 0f));
       if(Input.GetKey(KeyCode.E)){
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f,0f, -turnForceNorm*5* Time.deltaTime));
       }
       if(Input.GetKey(KeyCode.Q)){
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f,0f, turnForceNorm*5* Time.deltaTime));
       }
        }
       
        if(moveAble){
            if(Input.GetKey(KeyCode.LeftShift)){
                maxSpeed = boostSpeed;
               BoostEffects.emitting=true;
               if(!driftEffect.isPlaying){
                driftEffect.Play();}
            }
            if(Input.GetKeyUp(KeyCode.LeftShift)){
                speed = moveSpeed;
                maxSpeed = MaxSpeed;
                BoostEffects.emitting=false;
                driftEffect.Stop();
            }
            if(Input.GetKey(KeyCode.Space)&&driftable){
                turn = driftForce;
                rbody.drag=0.2f;
                drift = true;
            }
            if(Input.GetKeyDown(KeyCode.Space)&&driftable){
                for(int i=0; i<wheels.Length;i++){
                    WheelFrictionCurve sf = wheels[i].sidewaysFriction;
                    sf.stiffness = 0;
                    wheels[i].sidewaysFriction = sf;
                }
                if(!driftEffect.isPlaying){
                driftEffect.Play();}
            }
            if(Input.GetKeyUp(KeyCode.Space)&&driftable){
                turn = turnSpeed;
                rbody.drag = 0;
                drift = false;
                for(int i=0; i<wheels.Length;i++){
                    WheelFrictionCurve sf = wheels[i].sidewaysFriction;
                    sf.stiffness = 2;
                    wheels[i].sidewaysFriction = sf;
                }
                Boost(3);
                StartCoroutine(DriftBoost(duration));
                
            }
            if(drift){
                timer+=1*Time.deltaTime;
                if(timer>=3){
                    Boost(2);
                    duration = 2;
                }
                else if(timer>=2f){
                    Boost(1);
                    duration = 1.2f;
                }
                else if(timer>=1f){
                    Boost(0);
                    duration = 0.5f;
                }
            }
        }
    }
    
    void FixedUpdate(){
        if(moveAble){
                float Horizontal = Input.GetAxis("Horizontal");
             
                float Vertical = Input.GetAxis("Vertical");


        rbody.AddForce(new Vector3 (0, -gravity *4* rbody.mass, 0));
        rbody.AddRelativeForce(0,0,Vertical*speed);
        if(drift){
        //rbody.AddTorque(transform.up * Horizontal*turn);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, Input.GetAxis("Horizontal") * turnForceNorm*2* Time.deltaTime, 0f));
        }
       
        if(rbody.velocity.magnitude > maxSpeed)
            {
                rbody.velocity = rbody.velocity.normalized * maxSpeed;
            }
        if(Input.GetKey(KeyCode.LeftShift)||boosting){
                rbody.AddRelativeForce(boostForce*Vector3.forward);
            }
         if(Input.GetKey(KeyCode.Space)){
               rbody.AddRelativeForce(speed*-Input.GetAxis("Horizontal"),speed*0.5f,0); 
            }
    }
}
public void Boost(int degree){
        for(int i=0;i<boostEffects.Length;i++){
            if(i!=degree){
            boostEffects[i].Stop();}
        }
        if(degree!=3){
        boostEffects[degree].Play();}
    }

public IEnumerator DriftBoost(float duration2){
    duration = 0;
    maxSpeed = boostSpeed;
    BoostEffects.emitting=true;
    timer=0;
    driftable=false;
    boosting=true;
    if(!driftEffect.isPlaying){
    driftEffect.Play();}
    yield return new WaitForSeconds(duration2);
    speed = moveSpeed;
    maxSpeed = MaxSpeed;
    BoostEffects.emitting=false;
    driftEffect.Stop();
    driftable=true;
    boosting=false;
}

float ClampAngle(float angle, float from, float to)
     {
         // accepts e.g. -80, 80
         if (angle < 0f) angle = 360 + angle;
         if (angle > 180f) return Mathf.Max(angle, 360+from);
         return Mathf.Min(angle, to);
     }
}
