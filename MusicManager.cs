using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    AudioSource audiosource;

    [System.Serializable]
    public class soundEffect{
        public string soundName;
        public AudioClip soundClip;
    }
    public soundEffect[] sounds;

    public string lastSound = "default";


    


    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        ChangeSound(lastSound);
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void ChangeSound(string soundname){
        //audiosource.Stop();
        lastSound = soundname;
        for(int i=0;i<sounds.Length;i++){
            if(sounds[i].soundName==soundname){
                //audiosource.clip = sounds[i].soundClip;
                StartCoroutine(StartFade(i,1f,0));
                //audiosource.PlayScheduled(2f);
            }
        }
    }


    public IEnumerator StartFade(int arrayNumber, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audiosource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audiosource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        currentTime=0;
        float targetVolume2=Variables.musicVolume;
        float start2 = 0;
        audiosource.Stop();
        audiosource.clip = sounds[arrayNumber].soundClip;
        audiosource.PlayScheduled(2f);
        yield return new WaitForSeconds(0.2f);
        while (currentTime < duration){
            currentTime += Time.deltaTime;
            audiosource.volume = Mathf.Lerp(start2,targetVolume2,currentTime/duration);
            yield return null;
        }
        yield break;
    }
}
