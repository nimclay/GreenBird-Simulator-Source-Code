using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class occilatingObject : MonoBehaviour
{
    public float forceToAdd=10;
    Rigidbody rbody;
    bool forward;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(forward){
            timer+=1;
            if(timer>=1.5){
                forward=false;
                timer=0;
            }
        rbody.AddRelativeForce(Vector3.forward * forceToAdd);}
        else if(!forward){
            timer+=1;
            if(timer>=1.5){
                forward=true;
                timer=0;
            }
        rbody.AddRelativeForce(Vector3.forward * -forceToAdd);}


    }
}
