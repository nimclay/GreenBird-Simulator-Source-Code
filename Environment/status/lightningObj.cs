using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightningObj : MonoBehaviour
{
 void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            other.GetComponent<StatusManager>().Electrify();
        }
    }
}
