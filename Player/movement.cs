using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    public Vector3 SpawnLocation;
    Vector3 LastPosition;
    public bool dashJump;

    public bool Electrified;
    public bool moveAble=true;

    public bool dashing;
    float dashTimer;

    HealthStamManager manager;
    [Header("Player")]
    public GameObject PlayerBody;
    public Rigidbody rbody;
    public Transform cam;
    bool stopaddingvelocity;
    public float speed = 10.0f;
    public float additionalspeed;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
    Vector3 targetVelocity;
    public float turnsmoothtime = 0.05f;
    float turnsmoothvelocity;
    float jumps=2;
    public float maxjumps=2;
    public float jumpForce = 10;
    public float doubleJumpForce = 3;
    public bool grounded;
    [Header("Effects")]
    public ParticleSystem DashingEffects;
    public ParticleSystem teleporterEffect;

    [Header("Abilities")]
    public bool skating;
    bool skatemove=true;
    

    public bool dashAbility=true;

    public bool teleportAbility=true;
    public bool spike=true;
    public bool sprintAbility=true;
    public bool FlyAbility=true;

    bool gliding;

    public bool bonusMode;
    public bool sprinting;


    bool spiking;

    [Header("SpeedModifiers")]
    public float bonusSpeed;
    public float visionSpeed = -3;
    public float UpgradedSpeed;

    [Header("Sound")]
    public AudioSource soundEffectSource;
    public AudioClip jumpSound;
    public AudioClip teleportSound;


    [Space(10)]
    [Header("keycodes")]
    public KeyCode FlyKeyBind = KeyCode.Z;
    public KeyCode FlyKeyBindController = KeyCode.JoystickButton10; //left stick click (L3)

    public KeyCode SprintKeyBind = KeyCode.LeftShift;
    public KeyCode SprintKeyBindController = KeyCode.JoystickButton6; //L2

    public KeyCode JumpKeyBind = KeyCode.Space;
    public KeyCode JumpKeyBindController = KeyCode.JoystickButton1; //X

    public KeyCode TeleportKeyBind = KeyCode.Alpha1;
    public KeyCode TeleportKeyBindController = KeyCode.JoystickButton4; //L1

    public KeyCode TeleportKeyBindBack = KeyCode.Alpha2;
    public KeyCode TeleportKeyBindBackController = KeyCode.JoystickButton5; //R1

    public KeyCode AttackKeyBind = KeyCode.Mouse0;
    public KeyCode AttackKeyBindController = KeyCode.JoystickButton7; //R2

    public bool menu = false;
    // Start is called before the first frame update
    void Start()
    {
        SpawnLocation=transform.position;
        LastPosition=SpawnLocation;
        jumps = maxjumps;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        //modify stats
        if(!menu){
        
        speed += Variables.bonusMoveSpeed*0.7f;
        jumpForce += Variables.bonusJumpHeight;
        doubleJumpForce += (Variables.bonusJumpHeight/2);}



        soundEffectSource = GetComponent<AudioSource>();
        manager = GetComponent<HealthStamManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float targetAngle = Mathf.Atan2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"))*Mathf.Rad2Deg+cam.eulerAngles.y;
        if(Electrified){
            targetAngle = Mathf.Atan2(-Input.GetAxis("Horizontal"),-Input.GetAxis("Vertical"))*Mathf.Rad2Deg+cam.eulerAngles.y;;
        }
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnsmoothvelocity, turnsmoothtime);
        if(gliding){
            targetAngle = Mathf.Rad2Deg+cam.eulerAngles.y+120;
            float targetAngle2 = Mathf.Rad2Deg+cam.eulerAngles.x;
            float targetAngle3 = Mathf.Rad2Deg+cam.eulerAngles.z;
            float angle2 = Mathf.SmoothDampAngle(transform.eulerAngles.x,targetAngle2,ref turnsmoothvelocity, turnsmoothtime);
            float angle3 = Mathf.SmoothDampAngle(transform.eulerAngles.z,targetAngle3,ref turnsmoothvelocity, turnsmoothtime);
            transform.rotation = Quaternion.Euler(targetAngle2,targetAngle+180,0);
        }
        if(moveAble){
        if(Input.GetAxis("Horizontal")!=0||Input.GetAxis("Vertical")!=0){
           if(!gliding){
                transform.rotation = Quaternion.Euler(0f,angle,0f);
                }}
    
        if(Input.GetKeyDown(KeyCode.Escape)){
            Cursor.visible = !Cursor.visible;
            if(Cursor.visible){
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }
            else{
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }
            
        }
        if((Input.GetKeyDown(JumpKeyBind)||Input.GetKeyDown(JumpKeyBindController))){
            if(moveAble){
            if(dashing){
                rbody.velocity = new Vector3(rbody.velocity.x,0,rbody.velocity.z);
                dashing=false;
                dashTimer=0;
                DashingEffects.Stop();
                rbody.AddRelativeForce(0,jumpForce*1.3f,jumpForce);
            }
            else if(skating){
                
                    rbody.AddForce(0,jumpForce,0);
                    jumps-=1;
                    skatemove=false;
                
            } else if(jumps>=1){
                if(stopaddingvelocity){
            //rbody.velocity = new Vector3(rbody.velocity.x,0,rbody.velocity.y);
            }
            else{rbody.velocity=Vector3.zero;}
            if(jumps==3){
                grounded=false;
                jumps-=1;
                rbody.AddForce(new Vector3(0,jumpForce,0));
                soundEffectSource.Stop();
            soundEffectSource.PlayOneShot(jumpSound,Variables.volume+0.1f);
            }
            else if(jumps==2){
               grounded=false;
                jumps-=1;
                if(maxjumps>=3){
                rbody.AddForce(new Vector3(0,doubleJumpForce,0));}
                else{rbody.AddForce(new Vector3(0,jumpForce,0));}
                if(!spike){jumps=0;}
                soundEffectSource.Stop();
            soundEffectSource.PlayOneShot(jumpSound,Variables.volume+0.1f);
            }
            else if(jumps==1){
                if(spike){
                    grounded=false;
                    jumps-=1;
                    rbody.AddForce(cam.forward*jumpForce*2);
                    soundEffectSource.Stop();
            soundEffectSource.PlayOneShot(jumpSound,Variables.volume+0.1f);
                    spiking=true;
                    StopCoroutine("Spike");
                    StartCoroutine("Spike");
                }
                else{
                    grounded=false;
                    jumps-=1;
                    //rbody.AddForce(new Vector3(0,doubleJumpForce,0));
                }
            }}
          }
        }
        if(dashAbility||bonusMode){
            if(manager!=null){
            if(manager.currentDashes>=1){
                    manager.LoseStam();
                    DashingEffects.Play();
                    dashing=true;
                }}
            
            
        }
        if(sprintAbility){
            
        if((Input.GetKeyDown(SprintKeyBind)||Input.GetKeyDown(SprintKeyBindController))&&!dashing){
           
            
                if(bonusMode){
                    if(sprintAbility){
                        
                    sprinting=!sprinting;
                    
                    stopaddingvelocity=!stopaddingvelocity;}}}
                }
            
        if(dashing){
            dashTimer+=1*Time.deltaTime;
            
           if(dashTimer>=0.5f){
                dashing=false;
                dashTimer=0;
            }

        }
        if(FlyAbility){
        if(Input.GetKeyDown(FlyKeyBind)||Input.GetKeyDown(FlyKeyBindController)){
            gliding = !gliding;
        }}}

    }

    void FixedUpdate(){
        if(moveAble){
            if(gliding){
                rbody.AddForce(0,-transform.eulerAngles.x*0.2f,0);

                rbody.AddRelativeForce(0,2,0);
            }
            else{
            float Horizontal = Input.GetAxis("Horizontal");
            if(Horizontal<0){
                    Horizontal*=-1;
            }
            float Vertical = Input.GetAxis("Vertical");
            if(Vertical<0){
                    Vertical*=-1;}
            if(!stopaddingvelocity&&!spiking){
                if(!dashing){
                    targetVelocity = new Vector3(0, 0, Horizontal + Vertical);
                    targetVelocity = transform.TransformDirection(targetVelocity);
                    targetVelocity = Vector3.ClampMagnitude(targetVelocity, 1);
                    targetVelocity *= speed+additionalspeed+bonusSpeed+UpgradedSpeed;
                    // Apply a force that attempts to reach our target velocity
                    Vector3 velocity = rbody.velocity;
                    Vector3 velocityChange = (targetVelocity - velocity);
                    velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                    velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                    velocityChange.y = 0;
                    
                    rbody.AddForce(velocityChange, ForceMode.VelocityChange);}}
            else if(stopaddingvelocity){
                rbody.AddRelativeForce(0,0,1);
            }
            rbody.AddForce(new Vector3 (0, -gravity * rbody.mass, 0));
            if(dashing){
                rbody.AddRelativeForce(Vector3.forward*120);
                rbody.velocity = new Vector3(rbody.velocity.x,0,rbody.velocity.z);
            }
            if((skating&&skatemove)||sprinting){
                float speedthing = Horizontal+Vertical;
                speedthing = Mathf.Clamp(speedthing,0,1);
                rbody.AddRelativeForce(Vector3.forward*(Horizontal+Vertical)*speed*2f);
                float runspeed = 15+(bonusSpeed+visionSpeed+UpgradedSpeed);
                runspeed *=1.2f;
                if(rbody.velocity.magnitude > runspeed)
                {
                    rbody.velocity = rbody.velocity.normalized * runspeed;
                }
                
            }}

        }
        if(Input.GetAxis("Horizontal")!=0||Input.GetAxis("Vertical")!=0){
            additionalspeed+=0.1f*Time.deltaTime;
            if(additionalspeed>=(speed*0.4f)){
                additionalspeed=(speed*0.4f);
            }
        }
        else{
            additionalspeed-=10*Time.deltaTime;
            if(additionalspeed<=0){
                additionalspeed=0;
            }
        }

    }

    void OnTriggerEnter(Collider other){
        if(other.tag!="Player"&&other.tag!="RaceRing"&&other.tag!="gatherlocation"){
            jumps = maxjumps;
            spiking = false;
            if(skating){
                skatemove=true;
            }
            grounded=true;}
    }

    public IEnumerator teleport(){
        soundEffectSource.PlayOneShot(teleportSound,0.5f);
        teleporterEffect.Play();
        yield return new WaitForSeconds(1.2f);
        transform.position=SpawnLocation;
    }
    public IEnumerator teleportBack(){
        soundEffectSource.PlayOneShot(teleportSound,0.5f);
        teleporterEffect.Play();
        yield return new WaitForSeconds(1.2f);
        transform.position=LastPosition;
        

    }

    public IEnumerator Spike(){
        yield return new WaitForSeconds(0.8f);
        spiking=false;
    }
    public bool speedBoostActive;
    public IEnumerator dashSpeedBoost(float timer, float speed,TrailRenderer trail){
        speedBoostActive = false;
        trail.emitting = true;
        //cam.NeonOn();
        bonusSpeed = speed;
        yield return new WaitForSeconds(timer);
        speedBoostActive = true;
        bonusSpeed = 0;
        trail.emitting = false;
    }
    public void StartDashPanel(float timer,float speed,TrailRenderer trail){
        lastCoroutine = StartCoroutine(dashSpeedBoost(timer,speed,trail));
        
        
    }
    public void StopDashPanel(Coroutine ruitine){
        StopCoroutine(ruitine);
    }
    public Coroutine lastCoroutine;
    
}
