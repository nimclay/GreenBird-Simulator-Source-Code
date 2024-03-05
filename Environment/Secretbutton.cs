using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secretbutton : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    private AudioSource audiosource;
    [SerializeField] private Material pressedMat;
    private Material DefaultMat;

    private bool activated=false;

    public FacilityBarriers facility;
    // Start is called before the first frame update
    void Start()
    {
        DefaultMat = GetComponent<Renderer>().material;
        audiosource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other){
        if(!activated){
            activated = true;
            Pressed();
        }
    }
    void Pressed(){
        audiosource.PlayOneShot(clip,0.5f);
        GetComponent<Renderer>().material = pressedMat;
        facility.ButtonHasBeenPressed();
        
    }
   
}
