using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBehavoir : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SellItem(string itemName){
        int amounttoSell = 1;
        inventory invent;
        invent = GetComponent<inventory>();
        for(int i=0;i>invent.items.Length;i++){
            if(invent.items[i].itemName==itemName){
                float itemCost =0;
                itemCost = invent.items[i].itemSellCost;
                invent.Coins += itemCost;   
                invent.items[i].itemAmount -= amounttoSell;
            }
        }
    }
     public void BuyItem(string itemName){
        int amounttoSell = 1;
        inventory invent;
        invent = GetComponent<inventory>();
        for(int i=0;i>invent.items.Length;i++){
            if(invent.items[i].itemName==itemName){
                float itemCost =0;
                itemCost=invent.items[i].itemBuyCost;
                
                invent.Coins -= itemCost;   
                invent.items[i].itemAmount += amounttoSell;
            }
        }
    }
}
