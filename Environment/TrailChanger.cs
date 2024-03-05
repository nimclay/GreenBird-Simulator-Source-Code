using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Delay(){
        yield return new WaitForSeconds(0.2f);
        ChangeColor();
    }
    public void ChangeColor(){
        if(Variables.trailMaterial!=null){
        GetComponent<TrailRenderer>().material = Variables.trailMaterial;}
    }
}
