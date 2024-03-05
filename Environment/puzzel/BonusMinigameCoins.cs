using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMinigameCoins : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip coincollectsound;
    public BonusMinigameEnd manager;

    private bool collected;
    public int amountOfCoin = 1;
    public bool end;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            if(!collected){
                collected=true;
            manager.GetCoin(amountOfCoin);
            audiosource.PlayOneShot(coincollectsound,0.5f);
            if(end){
                manager.currentTime = 0.5f;
            }
            Destroy(gameObject);}
        }
    }
}
