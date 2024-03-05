using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushBarrier : MonoBehaviour
{
    public pushObjectButton buttCode;
    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            buttCode.toggle();
        }
    }
}
