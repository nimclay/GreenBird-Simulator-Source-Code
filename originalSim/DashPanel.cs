using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPanel : MonoBehaviour
{
    public StatusSim status;
    public float speedBoost = 5;
    public GameObject Player;

    public static bool active=true;
    public float timer=1;

    public CameraMove cam;

    public TrailRenderer dashTrail;

    movement MoveCode;

    public AudioClip clip;
    AudioSource audiosource;

    
    // Start is called before the first frame update
    void Start()
    {
        MoveCode = Player.GetComponent<movement>();
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Delay(){
        active = false;
        yield return new WaitForSeconds(0.2f);
        active = true;
    }
    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            if(active){
                audiosource.PlayOneShot(clip,Variables.volume);
                StartCoroutine(Delay());
                if(status.OnFire){
                status.PutOutFire();}
                 if(MoveCode.lastCoroutine!=null){
                MoveCode.StopDashPanel(MoveCode.lastCoroutine);
            }
            
            MoveCode.StartDashPanel(timer,speedBoost,dashTrail);
            }
            
        }
    }

    
}
