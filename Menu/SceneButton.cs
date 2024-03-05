using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    public int[] sceneNumber;

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
            int num = GetNumber();
            loadscene.scene  = num;
            SceneManager.LoadScene(0);
        }
    }
    int GetNumber(){
        int num = 0;
        if((Variables.highscore==0)){
            num = sceneNumber[0];
        }
        else{
            num=Random.Range(0,sceneNumber.Length);
            num = sceneNumber[num];
        }
        return num;

    }
}
