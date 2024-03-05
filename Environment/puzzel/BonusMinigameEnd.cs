using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BonusMinigameEnd : MonoBehaviour
{
    public TextMeshProUGUI CurrentCoinCount;
    public Image TimerBar;
    private float imagefillpercent;

    public Fade TimesUp;
    public float maxTime = 30;
    public float currentTime;

    public int coinCount;
    // Start is called before the first frame update
    void Start()
    {
        imagefillpercent = 1/maxTime;
        currentTime=maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime-=1*Time.deltaTime;
        if(currentTime<=0){
            currentTime = 10000;
            StartCoroutine(endLevel());
        }
        else{
            TimerBar.fillAmount = currentTime*imagefillpercent;
        }
    }

    public void GetCoin(int coins){
        coinCount+=coins;
        CurrentCoinCount.text = "Coins: "+coinCount;
    }

    IEnumerator endLevel(){
        TimesUp.ShowUI();
        yield return new WaitForSeconds(2f);
        loadscene.scene = 1;
        Variables.coins += coinCount;
        SaveGame.Save();
        SceneManager.LoadScene(0);
    }

}
