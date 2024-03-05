using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FoodButton : MonoBehaviour,  IPointerEnterHandler
{
    bool hovering;
    public GameObject Player;
    float x;
    Color boxColor;
    public Color hoverColor;

    public string type;

    // Start is called before the first frame update
    void Awake()
    {
        boxColor = GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if(hovering){
          
            GetComponent<Image>().color = hoverColor;
            if((Input.GetKeyDown(KeyCode.Mouse0)||Input.GetKeyDown(KeyCode.JoystickButton1))){
                if(type=="food"){
                Player.GetComponent<inventory>().InventoryFood();}
                if(type=="desert"){
                Player.GetComponent<inventory>().InventoryDesert();}
                if(type=="key"){
                    Debug.Log("Key");
                Player.GetComponent<inventory>().InventoryKey();}
            }
        }
        else{
            GetComponent<Image>().color = boxColor;
        }
    }

     void OnTriggerEnter2D(Collider2D other){
         hovering = true;
     }
     void OnTriggerExit2D(Collider2D other){
         hovering = false;
     }

     public void OnPointerOver(PointerEventData eventData)
     {
         
     }
    
     public void OnPointerEnter(PointerEventData eventData){
;
        hovering = true;
     }
     public void OnPointerExit(PointerEventData pointerEventData){
         hovering=false;
       
     }
}
