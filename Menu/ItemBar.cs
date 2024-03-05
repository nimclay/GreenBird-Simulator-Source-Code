using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemBar : MonoBehaviour
{
    inventory invCode;

    int currentItem;

    public TextMeshProUGUI itemNumText;
    HealthStamManager healthManger;

    public Image[] images;

    float delayTimer;
    bool delay=false;

    movement MovingCode;
    // Start is called before the first frame update
    void Start()
    {
        invCode = GameObject.FindObjectOfType<inventory>().GetComponent<inventory>();
        itemNumText.text = "0";
        healthManger = GameObject.FindObjectOfType<HealthStamManager>().GetComponent<HealthStamManager>();
        MovingCode = GameObject.FindObjectOfType<movement>().GetComponent<movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invCode.items[currentItem].itemAmount!=0){
        itemNumText.text = ""+ invCode.items[currentItem].itemAmount;}
        if(delay){
            delayTimer+=1*Time.deltaTime;
            if(delayTimer>=0.2f){
                delay = false;
                delayTimer=0;
            }
        }
        if((Input.GetKeyDown(KeyCode.DownArrow)||Input.GetAxis("NumPad Y")<0)&&!delay){
            useItem(currentItem);
        }
        if((Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetAxis("NumPad X")<0)&&!delay){
            ChangeItem(-1);
        }
        if((Input.GetKeyDown(KeyCode.RightArrow)||Input.GetAxis("NumPad X")>0)&&!delay){
            ChangeItem(1);
        }
    }

    void ChangeItem(int num){
        if(MovingCode.moveAble){
        delay=true;
        currentItem+=num;
        
        if(currentItem>4){
            currentItem=0;
        }
        if(currentItem<0){
            currentItem = 4;
        }
        itemNumText.text = ""+ invCode.items[currentItem].itemAmount;
        for(int i=0;i<images.Length;i++){
            if(i==currentItem){
                images[i].enabled = true;
            }
            else{
                images[i].enabled = false;
            }
        }}
    }

    void useItem(int num){
        if(MovingCode.moveAble){
        delay = true;
        if(invCode.items[currentItem].itemAmount!=0){
        healthManger.gainStam(invCode.items[currentItem].itemName);
        healthManger.gainHealth(invCode.items[currentItem].itemName);
        itemNumText.text = ""+invCode.items[num].itemAmount;}}
    }
}
