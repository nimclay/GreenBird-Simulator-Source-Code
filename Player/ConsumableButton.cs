using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConsumableButton : MonoBehaviour,  IPointerEnterHandler
{
    bool hovering;
    public GameObject Player;
    public string itemName;
    float x;
    Color boxColor;


    // Start is called before the first frame update
    void Awake()
    {
        boxColor = GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if(hovering){
          
            GetComponent<Image>().color = Color.gray;
            if((Input.GetKeyDown(KeyCode.Mouse0)||Input.GetKeyDown(KeyCode.JoystickButton1))){
                Player.GetComponent<HealthStamManager>().gainStam(itemName);
                Player.GetComponent<HealthStamManager>().gainHealth(itemName);
            }
        }
        else{
            GetComponent<Image>().color = boxColor;
        }
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

     void OnTriggerEnter2D(Collider2D other){
         hovering = true;
     }
     void OnTriggerExit2D(Collider2D other){
         hovering = false;
     }
}
