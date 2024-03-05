using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InvertedBoss : MonoBehaviour
{
    public GameObject[] PerchPositions;
    public GameObject NonPerchedPosition;
    
    public ParticleSystem[] attacks;
    
    Rigidbody rbody;

    public string[] Specialattack;


    bool perched=true;
    bool middle;
    float timer;

    public eventComplete[] perchObjectives;
    public GameObject[] perchObjectivesObjects;

    public GameObject[] enemySpawns;
    public GameObject Minion;

    private int currentPerch = 0;
    public bool FightStarted;
    bool attackable=true;

    public float maxHealth = 20;
    private float currentHealth;
    public SimHealth healthCode;

    private bool invinsible;
    private bool invinsibleTwo;

    private bool FightActive;
    private bool EnemySpawned;
    public ParticleSystem hurtEffect;

    public GameObject Player;
    public GameObject PlayerMoveLocation;
    public ParticleSystem PlayerTeleport;
    public AudioSource PlayerTeleportSound;
    public AudioClip TeleportClip;
    public MusicManager music;

    public ParticleSystem StompEffect;
    public ParticleSystem StompWarning;

    public GameObject FirePillars;
    public GameObject FireWarning;

    public SimHealth Playerhealth;

    public bool BossRushMode;
    public SuperBird superBird;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FightStarted){
            if(!FightActive){
            StartFight();}
            if(perched){
                if(perchObjectives[currentPerch].eventCompleted){
                NextStageGround();
                }
                else if(!EnemySpawned){
                    SpawnMinions();
                }
            }
            if(!perched){
                if(attackable){
                    Attack();
                }
            }
        }
    }

    
    public void Attack(){
        if(attackable){
            int currentAttack = Random.Range(0,attacks.Length);
            int SpecialAttack = Random.Range(1,2);
            if(SpecialAttack==1){
                int RandomAttack = Random.Range(0,Specialattack.Length);
                Invoke(Specialattack[RandomAttack], 0);
            }
            attacks[currentAttack].Play();
            StartCoroutine(AttackDelay(currentAttack));
        }
    }
    IEnumerator AttackDelay(int attack){
        attackable = false;
        yield return new WaitForSeconds(5f);
        attackable = true;
    }

    void StartFight(){
        if(!FightActive){
            GameData2.BossFight=true;
            invinsibleTwo=true;
            music.ChangeSound("Boss");
            Playerhealth.HealPlayer();
        perchObjectivesObjects[currentPerch].SetActive(true);FightActive=true;perched=true;}
    }

    void NextStagePerch(){
        perched = true;
        Playerhealth.HealPlayer();
        invinsibleTwo = true;
        for(int i =0;i<attacks.Length;i++){
            attacks[i].Stop();
        }
        transform.LookAt(new Vector3(NonPerchedPosition.transform.position.x,transform.position.y,NonPerchedPosition.transform.position.z));
        StartCoroutine(NextStage());
        
    }
    IEnumerator NextStage(){
        yield return new WaitForSeconds(1f);
        currentPerch +=1;
        if(currentPerch>=perchObjectives.Length){
            DestroyBoss();
        }
        else{
        transform.position = PerchPositions[currentPerch].transform.position;
             transform.LookAt(new Vector3(NonPerchedPosition.transform.position.x,transform.position.y,NonPerchedPosition.transform.position.z));
        yield return new WaitForSeconds(1f);
        PlayerTeleport.Play();
        PlayerTeleportSound.PlayOneShot(TeleportClip,0.5f);
        yield return new WaitForSeconds(1f);
        Player.transform.position = PlayerMoveLocation.transform.position;
        Player.GetComponent<movement>().rbody.velocity = Vector3.zero;
        perchObjectivesObjects[currentPerch].SetActive(true);EnemySpawned = false;}
    }
    void NextStageGround(){
        perched=false;
        currentHealth = maxHealth+((currentPerch-1)*5);
        invinsibleTwo = false;
        transform.position = NonPerchedPosition.transform.position;
        perchObjectivesObjects[currentPerch].SetActive(false);
    }
    void DestroyBoss(){
        healthCode.StartCoroutine("WinGame");
        Destroy(gameObject);    
    }

    void SpawnMinions(){
        EnemySpawned=true;
        for(int i=0;i<enemySpawns.Length;i++){
            Instantiate(Minion,enemySpawns[i].transform.position,Quaternion.Euler(Vector3.zero));
        }
    }

    void TakeDamage(){
        StartCoroutine(HurtDelay());
        currentHealth-=(1+Variables.bonusDamage);
        hurtEffect.Play();
        superBird.GainHyper();
        if(currentHealth<=0){
            NextStagePerch();
        }
    }
    IEnumerator HurtDelay(){
        invinsible = true;
        yield return new WaitForSeconds(0.3f);
        invinsible=false;
    }
    void OnParticleCollision(){
        if(!invinsible&&!invinsibleTwo){
            invinsible = true;
            TakeDamage();
        }
    }
     public void Jump(){
        rbody.AddRelativeForce(Vector3.up * rbody.mass*1000);
        StartCoroutine(JumpCo());
        StompEffect.Stop();
    }
    IEnumerator JumpCo(){
        StompWarning.Play();
        yield return new WaitForSeconds(1f);
        StompEffect.Play();
    }

    public void FireArea(){
        StopCoroutine("FireEffect");
        StartCoroutine(FireEffect());
    }
    IEnumerator FireEffect(){
        FireWarning.SetActive(true);
        yield return new WaitForSeconds(1f);
        FirePillars.SetActive(true);
        FireWarning.SetActive(false);
        yield return new WaitForSeconds(3.5f);
        FirePillars.SetActive(false);
    }
}
