using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityBarriers : MonoBehaviour
{

    public GameObject barriers;
    public GameObject lights;
    public AudioSource source;
    public AudioClip clip;
    public int numOfButtonsPressed;
    public int maxbuttons = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonHasBeenPressed(){
        numOfButtonsPressed+=1;
        if(numOfButtonsPressed>=maxbuttons){
            numOfButtonsPressed=maxbuttons;
            RemoveBarrier();
        }
    }

    void RemoveBarrier(){
        source.PlayOneShot(clip,1f);
        barriers.SetActive(false);
        lights.SetActive(true);
    }
}
