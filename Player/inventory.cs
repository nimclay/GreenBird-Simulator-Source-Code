using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventory : MonoBehaviour
{
   
    public ParticleSystem collectParticles;
    public AudioSource audioSource;
    public AudioClip gatherSound;
    
    [System.Serializable]
    public class item{
        public string itemName;
        public int itemSellCost;
        public int itemBuyCost;
        public int rarity;
        public int itemAmount;
        public int staminaRegen=1;
        public int healthRegen=2;
        public string itemLocation;
        public bool edible;
        public bool cure;
        public bool cureFire;
        public bool cureIce;
        public Sprite itemIMG;
    }

    public GameObject Content;
    
    public GameObject itemSlot;
    
    public GameObject InventoryObj;
    public bool inventoryOpen=false;
    RectTransform contentRect;
    public item[] items;


    public float LengthOfItems;
    float items2;
    float row;
    public float LengthOfItems2;
    public float gap=5;

    public CursorController cursorControlls;

    public Color[] color;

    public CameraMove camMoveCode;

    public float Coins;

    bool food=true;
    bool desert;
    bool key;
    public bool uselessness;
    
    // Start is called before the first frame update
    void Start()
    {
        if(Content!=null){
        contentRect = Content.GetComponent<RectTransform>();
       
        InventoryObj.SetActive(false);}
    }

    // Update is called once per frame
    void Update()
    {
         
        if((Input.GetKeyDown(KeyCode.I)||Input.GetKeyDown(KeyCode.JoystickButton3))&&!uselessness){
            GetComponent<movement>().moveAble=false;
            
            GetComponent<movement>().rbody.velocity = Vector3.zero;
            inventoryOpen=!inventoryOpen;
            
            if(inventoryOpen==true){
                cursorControlls.active = true;
                camMoveCode.camMoveAble=false;
                InventoryObj.SetActive(true);            
                foreach (Transform child in Content.transform)
                {
                Destroy(child.gameObject);
                }
                LengthOfItems=0;
                items2=0;
                row=0;
                for(int i=0;i<items.Length;i++){
                if(items[i].itemAmount>0&&items[i].edible){
                    LengthOfItems+=1;
                    items2+=1;
                    if(items2>5){
                        row+=1;
                        items2=1;
                    }
                    GameObject newItemSlot = Instantiate(itemSlot,Content.transform);
                    newItemSlot.transform.position = new Vector3(0,0,0);
                    //newItemSlot.transform.SetParent(Content.transform, true);

                    newItemSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(-156+(78*(items2-1)),62-(62*row));
                    newItemSlot.transform.GetChild(1).GetComponent<Image>().sprite = items[i].itemIMG;
                    newItemSlot.transform.GetChild(2).GetComponent<Text>().text = ""+items[i].itemName;
                    newItemSlot.GetComponent<Image>().color = color[(items[i].rarity)-1];
                    newItemSlot.transform.GetChild(4).GetComponent<Text>().text = "";
                    newItemSlot.transform.GetChild(5).GetComponent<Text>().text = "";
                    if(items[i].edible){
                        newItemSlot.transform.GetChild(0).GetComponent<ConsumableButton>().enabled = true;
                        newItemSlot.transform.GetChild(0).GetComponent<ConsumableButton>().itemName = items[i].itemName;
                        newItemSlot.transform.GetChild(4).GetComponent<Text>().text = ""+items[i].healthRegen;
                    newItemSlot.transform.GetChild(5).GetComponent<Text>().text = ""+items[i].staminaRegen;
                    }
                    newItemSlot.transform.GetChild(3).GetComponent<Text>().text = ""+items[i].itemAmount;
                }}  
            }
            else{
                InventoryObj.SetActive(false); 
                GetComponent<movement>().moveAble=true;
                camMoveCode.camMoveAble=true;
                 cursorControlls.active=false;
            }

        }

        
    }
    public void collectItem(bool PlaySound){
        collectParticles.Play();
        if(PlaySound){
        audioSource.PlayOneShot(gatherSound,0.5f);}
    }

    public void inventoryList(){
        if(food){InventoryFood();}
        else if(desert){InventoryDesert();}
        else if(key){InventoryKey();}
    }


    public void InventoryFood(){
        food=true;
        desert=false;
        key=false;

        cursorControlls.active = true;
        camMoveCode.camMoveAble=false;
        InventoryObj.SetActive(true);            
        foreach (Transform child in Content.transform)
        {
        Destroy(child.gameObject);
        }
        LengthOfItems=0;
        items2=0;
        row=0;
        for(int i=0;i<items.Length;i++){
        if(items[i].itemAmount>0&&items[i].edible){
            LengthOfItems+=1;
            items2+=1;
            if(items2>5){
                row+=1;
                items2=1;
            }
            GameObject newItemSlot = Instantiate(itemSlot,Content.transform);
            newItemSlot.transform.position = new Vector3(0,0,0);
            //newItemSlot.transform.SetParent(Content.transform, true);

            newItemSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(-156+(78*(items2-1)),62-(62*row));
            newItemSlot.transform.GetChild(1).GetComponent<Image>().sprite = items[i].itemIMG;
            newItemSlot.transform.GetChild(2).GetComponent<Text>().text = ""+items[i].itemName;
            newItemSlot.GetComponent<Image>().color = color[(items[i].rarity)-1];
            newItemSlot.transform.GetChild(4).GetComponent<Text>().text = "";
            newItemSlot.transform.GetChild(5).GetComponent<Text>().text = "";
            if(items[i].edible){
                newItemSlot.transform.GetChild(0).GetComponent<ConsumableButton>().enabled = true;
                newItemSlot.transform.GetChild(0).GetComponent<ConsumableButton>().itemName = items[i].itemName;
                newItemSlot.transform.GetChild(4).GetComponent<Text>().text = ""+items[i].healthRegen;
            newItemSlot.transform.GetChild(5).GetComponent<Text>().text = ""+items[i].staminaRegen;
            }
            newItemSlot.transform.GetChild(3).GetComponent<Text>().text = ""+items[i].itemAmount;
        }}  
        
                
    }
    public void InventoryDesert(){
        food=false;
        desert=true;
        key=false;


        cursorControlls.active = true;
        camMoveCode.camMoveAble=false;
        InventoryObj.SetActive(true);            
        foreach (Transform child in Content.transform)
        {
        Destroy(child.gameObject);
        }
        LengthOfItems=0;
        items2=0;
        row=0;
        for(int i=0;i<items.Length;i++){
        if(items[i].itemAmount>0&&items[i].itemLocation=="desert"){
            LengthOfItems+=1;
            items2+=1;
            if(items2>5){
                row+=1;
                items2=1;
            }
            GameObject newItemSlot = Instantiate(itemSlot,Content.transform);
            newItemSlot.transform.position = new Vector3(0,0,0);
            //newItemSlot.transform.SetParent(Content.transform, true);

            newItemSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(-156+(78*(items2-1)),62-(62*row));
            newItemSlot.transform.GetChild(1).GetComponent<Image>().sprite = items[i].itemIMG;
            newItemSlot.transform.GetChild(2).GetComponent<Text>().text = ""+items[i].itemName;
            newItemSlot.GetComponent<Image>().color = color[(items[i].rarity)-1];
            newItemSlot.transform.GetChild(4).GetComponent<Text>().text = "";
            newItemSlot.transform.GetChild(5).GetComponent<Text>().text = "";
            if(items[i].edible){
                newItemSlot.transform.GetChild(0).GetComponent<ConsumableButton>().enabled = true;
                newItemSlot.transform.GetChild(0).GetComponent<ConsumableButton>().itemName = items[i].itemName;
                newItemSlot.transform.GetChild(4).GetComponent<Text>().text = ""+items[i].healthRegen;
            newItemSlot.transform.GetChild(5).GetComponent<Text>().text = ""+items[i].staminaRegen;
            }
            newItemSlot.transform.GetChild(3).GetComponent<Text>().text = ""+items[i].itemAmount;
        }}  

    }
    public void InventoryKey(){
        food=false;
        desert=false;
        key=true;

        cursorControlls.active = true;
        camMoveCode.camMoveAble=false;
        InventoryObj.SetActive(true);            
        foreach (Transform child in Content.transform)
        {
        Destroy(child.gameObject);
        }
        LengthOfItems=0;
        items2=0;
        row=0;
        for(int i=0;i<items.Length;i++){
        if(items[i].itemAmount>0&&items[i].itemLocation=="key"){
            LengthOfItems+=1;
            items2+=1;
            if(items2>5){
                row+=1;
                items2=1;
            }
            GameObject newItemSlot = Instantiate(itemSlot,Content.transform);
            newItemSlot.transform.position = new Vector3(0,0,0);
            //newItemSlot.transform.SetParent(Content.transform, true);

            newItemSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(-156+(78*(items2-1)),62-(62*row));
            newItemSlot.transform.GetChild(1).GetComponent<Image>().sprite = items[i].itemIMG;
            newItemSlot.transform.GetChild(2).GetComponent<Text>().text = ""+items[i].itemName;
            newItemSlot.GetComponent<Image>().color = color[(items[i].rarity)-1];
            newItemSlot.transform.GetChild(4).GetComponent<Text>().text = "";
            newItemSlot.transform.GetChild(5).GetComponent<Text>().text = "";
            if(items[i].edible){
                newItemSlot.transform.GetChild(0).GetComponent<ConsumableButton>().enabled = true;
                newItemSlot.transform.GetChild(0).GetComponent<ConsumableButton>().itemName = items[i].itemName;
                newItemSlot.transform.GetChild(4).GetComponent<Text>().text = ""+items[i].healthRegen;
            newItemSlot.transform.GetChild(5).GetComponent<Text>().text = ""+items[i].staminaRegen;
            }
            newItemSlot.transform.GetChild(3).GetComponent<Text>().text = ""+items[i].itemAmount;
        }}  
    }
}
