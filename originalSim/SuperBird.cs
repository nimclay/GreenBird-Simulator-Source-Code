using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SuperBird : MonoBehaviour
{
    public GameData2 gameData;
    public bool supermode=false;
    public Material materialsHead;
    public Renderer head;
    public ParticleSystem SuperEffects;

    public ParticleSystem[] Attacks;
    public ParticleSystem[] StrongAttack;
    public ParticleSystem[] attacksUnPowered;

    public AudioSource AttackSound;
    public AudioClip Clip;
    int combo=0;
    int specialcombo=0;

    bool attacking;

    public int level=100;

    public static float levelxp;

    public TextMeshProUGUI LevelNumber;
    public Image EXPBar;


    public Image BonusVision;
    public GameObject Visionfilter;
    float visionAmount;
    public float maxVision;
    bool seeable=false;
    bool visionpressed;
    private float visionfill;

    private bool visionAbility;

    movement moveCode;
    public MusicManager music;

    

    public int LevelForSpike = 1;
    public int LevelForSprint = 10;
    public int LevelForVision = 15;
    public int LevelForFly = 30;

    public bool StartSuper;

    bool hyperMode;

    public float hyperfill;
    public float maxHyper=25;
    private float hyperfillpercent;
    public ParticleSystem hyper;
    public Image HyperBar;
    // Start is called before the first frame update
    void Start()
    {
        levelxp=0;
        level = 0;
        
        visionfill = (1/maxVision);
        moveCode = GetComponent<movement>();
        if(StartSuper){
            level = 100;
        }
        CheckLevel();
        visionAmount = maxVision;
        hyperfillpercent = 1/maxHyper;
        
        StopHyper();
    }

    // Update is called once per frame
    void Update()
    {
        if(levelxp>=1000){
            levelxp -= 1000;
            level +=1;
            CheckLevel();
        }
        LevelNumber.text = ""+level;
        EXPBar.fillAmount = levelxp*0.001f;
        if((GameData2.finalStand&&!supermode)){
            supermode = true;
            ActivateSuper();
        }

        if((Input.GetKey(moveCode.AttackKeyBind)||Input.GetKey(moveCode.AttackKeyBindController))){
            if(supermode){
                AttackSuper();
            }
            else{
                Attack();
                
            }
        }
        if((Input.GetKeyDown(moveCode.SprintKeyBind)||Input.GetKeyDown(moveCode.SprintKeyBindController))){
           if(seeable&&visionAbility){
                visionpressed = !visionpressed;
                if(Visionfilter.activeSelf == !visionpressed){
                    Visionfilter.SetActive(visionpressed);
                    if(visionpressed){
                    moveCode.visionSpeed = 6;}
                    else{
                    moveCode.visionSpeed = -6;}
                }}
            
        }
        if(visionpressed){
            visionAmount -= 1*Time.deltaTime;
            if(visionAmount<=0){
                visionAmount = 0;
                seeable = false;
                visionpressed = false;
                Visionfilter.SetActive(visionpressed);
                moveCode.visionSpeed = -3;
            }
        }
        
        if(visionAmount<=maxVision&&!visionpressed){
            visionAmount+=1*Time.deltaTime;
            if(visionAmount>=maxVision){
                visionAmount = maxVision;
                if(!moveCode.sprinting){
                seeable = true;
            }
        }
        }
        if(hyperMode){
            hyperfill-=2*Time.deltaTime;
            if(hyperfill<=0){
                StopHyper();
            }
        }
        if(supermode){
        HyperBar.fillAmount = hyperfill*hyperfillpercent;}
        BonusVision.fillAmount = visionfill*visionAmount;
        
    }

    void ActivateSuper(){
        GetComponent<SimHealth>().finalStand = true;
        if(!StartSuper){
        music.ChangeSound("run");}
        Material[] myMaterials2;
        myMaterials2 = head.materials;
        myMaterials2[2] = materialsHead;
        head.materials = myMaterials2;
        SuperEffects.Play();
        combo=0;
    }
    void Attack(){
        if(!attacking){
            StartCoroutine(AttackDelay());
            attacksUnPowered[combo].Stop();
        attacksUnPowered[combo].Play();
        AttackSound.PlayOneShot(Clip,Variables.volume+(0.5f*(0.5f*combo)-0.2f));
        combo+=1;
        if(combo>=attacksUnPowered.Length){
            combo = 0;
        }
        }
    }
    void AttackSuper(){
        if(!attacking){
            float delay=0.4f;
        Attacks[combo].Stop();
            Attacks[combo].Play();
            AttackSound.PlayOneShot(Clip,Variables.volume+(0.5f*(0.5f*combo)-0.2f));
            combo+=1;

            if(combo>=Attacks.Length){
                combo = 0;
                delay = 0.8f;
            }
            if(hyperMode){
                delay = 0.3f;
            }
            StartCoroutine(AttackDelaySuper(delay));}
    }
    void AttackSuperSpecial(){
        if(!attacking){
        Attacks[combo].Stop();
        float delay = 0.4f;
        StrongAttack[specialcombo].Stop();
            StrongAttack[specialcombo].Play();
            specialcombo+=1;
            if(specialcombo>=StrongAttack.Length){
                specialcombo = 0;
                delay=1f;
                
            }
            if(specialcombo==2){
                combo=2;
            }
            StartCoroutine(AttackDelaySuper(delay));}
    }

    public IEnumerator AttackDelay(){
        attacking = true;
        yield return new WaitForSeconds(0.5f);
        attacking=false;
    }
    public IEnumerator AttackDelaySuper(float delay){
        attacking = true;
        yield return new WaitForSeconds(delay);
        attacking=false;
    }

    void CheckLevel(){
        if(level>=LevelForSpike){
            moveCode.spike = true;
        }
        if(level>=LevelForSprint){
            moveCode.sprintAbility = true;
        }
        if(level>=LevelForFly){
            moveCode.FlyAbility = true;
        }
        if(level>=LevelForVision){
            visionAbility = true;
        }
    }

    void ActivateHyper(){
        hyperMode = true;
        hyper.Play();
    }
    void StopHyper(){
        hyperMode = false;
        hyper.Stop();
        hyperfill = 0;
    }

    public void GainHyper(){
        Debug.Log("didthing");
        if(!hyperMode){
        hyperfill += 1;
        Debug.Log("GainedHyper");
        if(hyperfill>=maxHyper){
            hyperfill = maxHyper;
            ActivateHyper();
        }}
    }

}
