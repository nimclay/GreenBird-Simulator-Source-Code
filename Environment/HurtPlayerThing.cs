using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerThing : MonoBehaviour
{
    public bool bouncyPad;

    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            if(bouncyPad){
                Player.GetComponent<SimHealth>().GetHurt();
                Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Player.GetComponent<Rigidbody>().AddForce(0,800,0);
            }}
        
    }
}
