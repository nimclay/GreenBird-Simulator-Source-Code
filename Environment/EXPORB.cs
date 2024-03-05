using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPORB : MonoBehaviour
{
    private float EXP = 50;
    private float Multiplyer;
    private bool used=false;
    Vector3 PlayerDirection;
    public GameObject Player;
    public float speed;
    private Rigidbody rbody;

    public bool playing;
    private bool existing = false;
    // Start is called before the first frame update
    void Awake()
    {
        Multiplyer = transform.localScale.magnitude;
        rbody = GetComponent<Rigidbody>();
    }
    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            if(!used){
                used=true;
                GetOrb();
            }
        }
    }
    void GetOrb(){
        SuperBird.levelxp += EXP*Multiplyer;
        Destroy(this.gameObject);
    }
    void Update(){
        PlayerDirection = Player.transform.position - transform.position;
        if(existing){
            StartCoroutine(living());
        }
        if(playing&&!existing){
            existing = true;
        }
    }
    void FixedUpdate(){
        if(playing){
        rbody.AddForce(new Vector3(PlayerDirection.x*speed,PlayerDirection.y*speed,PlayerDirection.z*speed));
        if(rbody.velocity.magnitude>=15){
            rbody.velocity = rbody.velocity.normalized * 15;
        }}
    }
    IEnumerator living(){
        yield return new WaitForSeconds(10f);
        Destroy(this.gameObject);
    }
    
}
