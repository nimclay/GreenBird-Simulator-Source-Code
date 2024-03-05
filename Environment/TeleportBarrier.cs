using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBarrier : MonoBehaviour
{

    public Vector3 teleportLocation;
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
            if(other.transform.parent!=null){
                other.transform.parent.transform.position = teleportLocation;;
            }
            else{other.transform.position = teleportLocation;}
        }
    }
}
