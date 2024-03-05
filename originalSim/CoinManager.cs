using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public float amountOfCoins;
    public TextMeshProUGUI CoinText;
    public string TagName = "GC:";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GainCoins(float coins){
        amountOfCoins += coins;
        CoinText.text = TagName+amountOfCoins;
    }
    public void LoseCoins(float coins){
        amountOfCoins-=coins;
        CoinText.text = TagName+amountOfCoins;
    }
}
