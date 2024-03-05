using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusSim : MonoBehaviour
{
    [Header("Fire Status")]
    public ParticleSystem fireEffects;
    public float fireDuration;
    public float fireSpeed = -10;

    private float fireTimer = 0;
    public bool OnFire = false;

    private movement moveCode;
    // Start is called before the first frame update
    void Start()
    {
        moveCode = GetComponent<movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(OnFire){
            fireTimer += 1*Time.deltaTime;
            if(fireTimer>=fireDuration){
                PutOutFire();
            }
        }
    }
    public void SetOnFire(){
        moveCode.bonusSpeed = fireSpeed;
       
        fireEffects.Play();
        OnFire = true;
        fireTimer = 0;
    }
    public void PutOutFire(){
        moveCode.bonusSpeed = 0;
        
        fireEffects.Stop();
        OnFire = false;
        fireTimer = 0;
        moveCode.rbody.AddForce(0,moveCode.doubleJumpForce,0);
    }
}
