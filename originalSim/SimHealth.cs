using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SimHealth : MonoBehaviour
{
    public float maxHealth = 5;
    public float maxHealthFinalStand = 20;
    public float currentHealthFinalStand;
    private float currentHealth;

    movement movementCode;
    public GreenBirdAnim animationControl;

    private bool hurtable=false;

    public ParticleSystem HurtEffect;

    public bool finalStand;

    public Fade fadeScreen;
    public TextMeshProUGUI score;
    public TextMeshProUGUI coins; 
    public TextMeshProUGUI achievements; 
    public TextMeshProUGUI WinText;

    public AudioSource endSound;
    public AudioClip clip;
    public FinalStandCode code;

    public Image HealthBar;
    float barFillAmount;
    public SuperBird birdCode;
    public InvertedBoss bossCode;
    public GameObject BossLocation;

    public bool bossMode;

    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        barFillAmount = (1/maxHealthFinalStand);
        currentHealthFinalStand = maxHealthFinalStand;
        movementCode = GetComponent<movement>();
        StartCoroutine(HealthCoolDown(3));
    }

    // Update is called once per frame
    void Update()
    {
        if(finalStand){
            HealthBar.fillAmount = currentHealthFinalStand*barFillAmount;
        }
        if(Input.GetKeyDown(KeyCode.Alpha0)&&hurtable){

            hurtable=false;
            StartCoroutine(EndGame());
        }
    }

    void killed(){
        animationControl.Die();
        
    }


    IEnumerator HealthCoolDown(float time){
        hurtable=false;
        yield return new WaitForSeconds(time);
        if(currentHealthFinalStand>0){
        hurtable = true;}
    }

    void OnParticleCollision(){
        if(hurtable){
            GetHurt();
        }
    }

    public void GetHurt(){
        if(hurtable){
        StartCoroutine(HealthCoolDown(1));
            if(!finalStand){
                HurtEffect.Play();
                currentHealth -= 1;
                if(currentHealth<=0){
                    currentHealth = maxHealth;
                    killed();
                }
            }
            else{
                currentHealthFinalStand-=1;
                HurtEffect.Play();
                if(currentHealthFinalStand<=0){
                    if(birdCode.level>=100&&!bossCode.FightStarted){
                        StartCoroutine(StartBoss());
                    }
                    else{GameData2.finalStand = false;
                    code.started=false;
                    GetComponent<movement>().moveAble=false;
                    StartCoroutine(EndGame());}}
                
            }
        }
    }

    public IEnumerator EndGame(){
        fadeScreen.ShowUI();
        WinText.text = "Game Over";
        yield return new WaitForSeconds(3f);
        loadscene.scene = 1;
        endSound.PlayOneShot(clip,Variables.volume);
        score.text = ""+eventManager.score;
        yield return new WaitForSeconds(1.5f);
        endSound.PlayOneShot(clip,Variables.volume);
        if(!bossMode){
        float AmountOfBirds = GameData2.AmountOfGreenBird + GameData2.AmountOfSandBird + GameData2.AmountOfSnowBird + GameData2.AmountOfVolcBird;
        if(AmountOfBirds>Variables.HighestBirdCount){
            Variables.HighestBirdCount = AmountOfBirds;
        }}
        float Coin = Mathf.Round(eventManager.score/500);
        coins.text = ""+Coin;
        if(!bossMode){
        if(birdCode.level>=Variables.HighestLevel){
            Variables.HighestLevel = birdCode.level;
        }
        yield return new WaitForSeconds(1.5f);
        if(eventManager.score > Variables.highscore){
            Variables.highscore = eventManager.score;
            achievements.text = "NEW HIGH SCORE: "+Variables.highscore;
            endSound.PlayOneShot(clip,Variables.volume);
            yield return new WaitForSeconds(2);
        }
        if(birdCode.level>Variables.level){
            Variables.level = birdCode.level;
            achievements.text = "HIGHEST LEVEL REACHED: "+Variables.level;
            endSound.PlayOneShot(clip,Variables.volume);
            yield return new WaitForSeconds(2);
        }
        if(Variables.HasSoccerHighScore){
            achievements.text = "BEST LAZERBALL TIME: "+Variables.SoccerHighScore;
            endSound.PlayOneShot(clip,Variables.volume);
            yield return new WaitForSeconds(2);
        }
        float TotalBirdCount = GameData2.AmountOfSandBird+GameData2.AmountOfGreenBird+GameData2.AmountOfSnowBird+GameData2.AmountOfVolcBird;
        if(TotalBirdCount>Variables.HighestBirdCount){
            Variables.HighestBirdCount = TotalBirdCount;
            achievements.text = "MOST BIRDS RAISED: "+ TotalBirdCount;
            endSound.PlayOneShot(clip,Variables.volume);
            yield return new WaitForSeconds(2);
        }}
        yield return new WaitForSeconds(2f);
        if(!bossMode){
        Variables.coins += Coin;
        Variables.level += (Mathf.Round((eventManager.score/10000)*100)/100);}
        SaveGame.Save();
        SceneManager.LoadScene(0);
    }

    public IEnumerator WinGame(){
        fadeScreen.ShowUI();
        WinText.text = "You Win!";
        if(!bossMode){
        Variables.Wins+=1;}
        yield return new WaitForSeconds(3f);
        loadscene.scene = 1;
        endSound.PlayOneShot(clip,Variables.volume);
        score.text = ""+eventManager.score;
        yield return new WaitForSeconds(1.5f);
        endSound.PlayOneShot(clip,Variables.volume);
        float AmountOfBirds = GameData2.AmountOfGreenBird + GameData2.AmountOfSandBird + GameData2.AmountOfSnowBird + GameData2.AmountOfVolcBird;
        if(AmountOfBirds>Variables.HighestBirdCount){
            Variables.HighestBirdCount = AmountOfBirds;
        }
        float Coin = Mathf.Round(eventManager.score/500);
        coins.text = ""+Coin;
        if(!bossMode){
        if(birdCode.level>=Variables.HighestLevel){
            Variables.HighestLevel = birdCode.level;
        }
        yield return new WaitForSeconds(1.5f);
        if(eventManager.score > Variables.highscore){
            Variables.highscore = eventManager.score;
            achievements.text = "NEW HIGH SCORE";
            endSound.PlayOneShot(clip,Variables.volume);
            yield return new WaitForSeconds(2);
        }
        if(birdCode.level>Variables.level){
            Variables.level = birdCode.level;
            achievements.text = "HIGHEST LEVEL REACHED";
            endSound.PlayOneShot(clip,Variables.volume);
            yield return new WaitForSeconds(2);
        }
        if(Variables.HasSoccerHighScore){
            achievements.text = "BEST LAZERBALL TIME";
            endSound.PlayOneShot(clip,Variables.volume);
            yield return new WaitForSeconds(2);
        }
        float TotalBirdCount = GameData2.AmountOfSandBird+GameData2.AmountOfGreenBird+GameData2.AmountOfSnowBird+GameData2.AmountOfVolcBird;
        if(TotalBirdCount>Variables.HighestBirdCount){
            Variables.HighestBirdCount = TotalBirdCount;
            achievements.text = "MOST BIRDS RAISED: "+ TotalBirdCount;
            endSound.PlayOneShot(clip,Variables.volume);
            yield return new WaitForSeconds(2);
        }}
        if(!bossMode){
        achievements.text = "Amount Of Wins Updated";
        endSound.PlayOneShot(clip,Variables.volume);
        yield return new WaitForSeconds(2f);}
        else{
            achievements.text = "Bonus coins: 20";
            Variables.coins += 20;
            endSound.PlayOneShot(clip,Variables.volume);
            yield return new WaitForSeconds(2f);
        }
        Variables.coins += Coin;
        Variables.level += (Mathf.Round((eventManager.score/10000)*100)/100);
        SaveGame.Save();
        SceneManager.LoadScene(0);
    }

    public void HealPlayer(){
        currentHealthFinalStand = maxHealthFinalStand;
        currentHealth = maxHealth;
    }

    IEnumerator StartBoss(){
        bossCode.FightStarted = true;
        transform.position = BossLocation.transform.position;
        HealPlayer();
        HealthBar.fillAmount = 1;
        yield return new WaitForEndOfFrame();

    }
    public void boss(){
        StartCoroutine(StartBoss());
    }
}
