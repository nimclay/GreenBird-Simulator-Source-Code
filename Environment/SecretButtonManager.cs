using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretButtonManager : MonoBehaviour
{

    private float currentButton = 0;
    private float maxButton = 5;

    public GameObject SecretBarriers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPressed(){
        currentButton +=1;
        if(currentButton >= maxButton){
            currentButton = maxButton;
            SecretBarriers.SetActive(false);
        }

    }
}
