using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TextMeshPro Coins;
    public TextMeshPro Level;
    public GameObject FutureLevelBarrier;
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
        yield return new WaitForSeconds(1f);
        Coins.text = ""+Variables.coins;
        Level.text = ""+Mathf.Round(Variables.level);
        if(Variables.WorldSaved){
            FutureLevelBarrier.SetActive(false);
        }
    }
}
