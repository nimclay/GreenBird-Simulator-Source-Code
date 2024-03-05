using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerButton : MonoBehaviour
{
    public Material ActiveMat;
    public Material DeActiveMat;
    public bool actived=true;

    public GameObject PowerLight;
    public PowerManager power;

    bool activatable=true;
    AudioSource source;
    public AudioClip clip;
    public float volume = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
      source = GetComponent<AudioSource>();
      
        StartTurnOff();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter(Collider other){
        if(other.tag=="Player"&&activatable){
            actived = !actived;
            StartCoroutine(activateDelay());
            if(!actived){
                StopCoroutine("off");
            }
            else{StartTurnOff();}
            Activate();
        }
     }

     IEnumerator activateDelay(){
        activatable = false;
        yield return new WaitForSeconds(1f);
        activatable = true;
     }

     void Activate(){
      source.PlayOneShot(clip,0.5f);
        if(actived){
                gameObject.GetComponent<MeshRenderer>().material = ActiveMat;
                PowerLight.GetComponent<MeshRenderer>().material = ActiveMat;
            }
            else{
                gameObject.GetComponent<MeshRenderer>().material = DeActiveMat;
                PowerLight.GetComponent<MeshRenderer>().material = DeActiveMat;

            }
            power.Check();
     }

     void StartTurnOff(){
        int offTime = time();
        StartCoroutine(off(offTime));
     }

     IEnumerator off(int time){
        yield return new WaitForSeconds(time);
        TurnOff();
     }

     int time(){
        int timeValue = 0;
        timeValue = Random.Range(80,260);
        return timeValue;
     }

     void TurnOff(){
      source.PlayOneShot(clip,0.5f);
        actived = false;
        gameObject.GetComponent<MeshRenderer>().material = DeActiveMat;
        PowerLight.GetComponent<MeshRenderer>().material = DeActiveMat;
        power.Check();
     }
}
