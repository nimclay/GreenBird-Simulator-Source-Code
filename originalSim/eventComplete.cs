using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventComplete : MonoBehaviour
{
    public bool eventCompleted;
    
    public bool MultiGather;
    public int numOfGather=5;
    private int currentGather;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    IEnumerator resetEvent(){
        yield return new WaitForSeconds(0.5f);
        eventCompleted = false;
    }
    public void CompEvent(){
        if(MultiGather){
            currentGather+=1;
            if(currentGather>=numOfGather){
                currentGather=0;
                CompEvent2();
            }
        }
        else{
        eventCompleted=true;
        StartCoroutine(resetEvent());}
    }

    public void CompEvent2(){
        eventCompleted=true;
        StartCoroutine(resetEvent());
    }
}
