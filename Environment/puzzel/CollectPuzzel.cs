using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPuzzel : MonoBehaviour
{
    //works in conjunction with gatherLocation.cs on the same object as collectItems[] objects;
    public GameObject[] collectItems;
    public GameObject completeBarrier;
    bool puzzelComplete=false;
    bool puzzel;

    public AudioClip completeClip;
    public AudioSource EffectSource;

    public MusicManager musicmanager;
    bool sound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!puzzelComplete){

            for(int i=0;i<collectItems.Length;i++){
                if(collectItems[i].activeSelf==true){
                    puzzel=true;
                }
            }
            
            if(!puzzel){
                puzzelComplete=true;
                completeBarrier.SetActive(false);
            }
            puzzel=false;
            
        }
        else{
           
            if(!sound){
                sound=true;
            musicmanager.ChangeSound("default");
            EffectSource.PlayOneShot(completeClip,1f);StartCoroutine(ResetThing());}
            puzzelComplete=false;
            
        }
        
    }


    public void Reset(){
        puzzelComplete=false;
        completeBarrier.SetActive(true);
        sound=false;
        
    }
    IEnumerator ResetThing(){
        yield return new WaitForSeconds(20f);
        Reset();
    }

    
}
