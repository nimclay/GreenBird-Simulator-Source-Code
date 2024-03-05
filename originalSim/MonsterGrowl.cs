using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGrowl : MonoBehaviour
{
    public AudioClip Growl;
    AudioSource audiosource;

    bool soundPlayed;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlaySound();
    }


    void PlaySound(){
        if(!soundPlayed){
            soundPlayed = true;
            float time = randomNum();
            StartCoroutine(GrowlCo(time));
        }
    }

    float randomNum(){
        float num = Random.Range(20,50);
        return num;
    }
    IEnumerator GrowlCo(float time){
        yield return new WaitForSeconds(time);
        float volume = Mathf.Clamp(Variables.volume*2,0,1.5f);
        audiosource.PlayOneShot(Growl,volume);
        soundPlayed = false;
    }
}
