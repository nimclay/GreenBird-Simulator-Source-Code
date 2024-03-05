using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPad : MonoBehaviour
{
    bool collided;
    float timer;
    public float power=30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(collided){
            timer+=1*Time.deltaTime;
            if(timer>=0.5f){
                collided=false;timer=0;
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"&&!collided){
            collided=true;
            other.GetComponent<Rigidbody>().velocity=Vector3.zero;
            other.GetComponent<Rigidbody>().AddForce(new Vector3(0,power,0),ForceMode.VelocityChange);
        }
    }
}
