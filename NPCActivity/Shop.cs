using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
    public ShopBehavoir shopBehaviour;
    public string[] itemForSale;
    public GameObject shopMenu;
    GameObject Player;
    inventory inventoryCode;
    movement PlayerCode;
    interactable interactionCode;

    public GameObject shopSellItem;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        inventoryCode = Player.GetComponent<inventory>();
        PlayerCode= Player.GetComponent<movement>();
        interactionCode = GetComponent<interactable>();
        shopMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(interactionCode.ObjInteractable){
            if(Input.GetKeyDown(KeyCode.E)){
                OpenShopMenu();
            }
            else if(Input.GetKeyDown(KeyCode.Q)){
                ExitShop();
            }
        }
    }

    void OpenShopMenu(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        shopMenu.SetActive(true);
        PlayerCode.rbody.velocity = Vector3.zero;
        PlayerCode.moveAble = false;
    }


    public void SellMenu(){
        shopMenu.SetActive(false);
        inventoryCode.InventoryObj.SetActive(true); 
        foreach (Transform child in inventoryCode.Content.transform)
        {
            Destroy(child.gameObject);
        }
        inventoryCode.LengthOfItems=0;
        for(int i=0;i<inventoryCode.items.Length;i++){
            if(inventoryCode.items[i].itemAmount>0){
                inventoryCode.LengthOfItems+=1;
                int tempInt = i;
                GameObject newItemSlot = Instantiate(shopSellItem,inventoryCode.Content.transform);
                newItemSlot.transform.position = new Vector3(0,(111-(33*i)),0);
                //newItemSlot.transform.SetParent(Content.transform, true);
                newItemSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,(111-(33*(inventoryCode.LengthOfItems-1))));
                newItemSlot.GetComponent<Button>().onClick.AddListener(() => shopBehaviour.SellItem(inventoryCode.items[tempInt].itemName));  
                newItemSlot.transform.GetChild(0).GetComponent<Text>().text = ""+inventoryCode.items[i].itemName;
                newItemSlot.transform.GetChild(1).GetComponent<Text>().text = "Rarity "+inventoryCode.items[i].rarity;
                newItemSlot.transform.GetChild(1).GetComponent<Text>().color = inventoryCode.color[(inventoryCode.items[i].rarity)-1];
                
                newItemSlot.transform.GetChild(2).GetComponent<Text>().text = ""+inventoryCode.items[i].itemAmount;
                newItemSlot.transform.GetChild(3).GetComponent<Text>().text = "$"+inventoryCode.items[i].itemSellCost;
            
            }
        }
    }
    public void BuyMenu(){
        shopMenu.SetActive(false);
        shopMenu.SetActive(false);
        inventoryCode.InventoryObj.SetActive(true); 
        foreach (Transform child in inventoryCode.Content.transform)
        {
            Destroy(child.gameObject);
        }
        inventoryCode.LengthOfItems=0;
        for(int i=0;i<inventoryCode.items.Length;i++){
            for(int x=0;x<itemForSale.Length;x++){
                if(inventoryCode.items[i].itemName == itemForSale[x]){
                    inventoryCode.LengthOfItems+=1;
                    GameObject newItemSlot = Instantiate(shopSellItem,inventoryCode.Content.transform);
                    newItemSlot.transform.position = new Vector3(0,(111-(33*i)),0);
                    //newItemSlot.transform.SetParent(Content.transform, true);
                    newItemSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,(111-(33*(inventoryCode.LengthOfItems-1))));
                    newItemSlot.GetComponent<Button>().onClick.AddListener(() => shopBehaviour.BuyItem(inventoryCode.items[i].itemName));  
                    newItemSlot.transform.GetChild(0).GetComponent<Text>().text = ""+inventoryCode.items[i].itemName;
                    newItemSlot.transform.GetChild(1).GetComponent<Text>().text = "Rarity "+inventoryCode.items[i].rarity;
                    newItemSlot.transform.GetChild(1).GetComponent<Text>().color = inventoryCode.color[(inventoryCode.items[i].rarity)-1];
                    newItemSlot.transform.GetChild(2).GetComponent<Text>().text = ""+inventoryCode.items[i].itemAmount;
                    newItemSlot.transform.GetChild(2).GetComponent<Text>().text = "$"+inventoryCode.items[i].itemBuyCost;
                }
            }
        }
    }
    public void ExitShop(){
        shopMenu.SetActive(false);
        inventoryCode.InventoryObj.SetActive(false);
        PlayerCode.rbody.velocity = Vector3.zero;
        PlayerCode.moveAble = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
