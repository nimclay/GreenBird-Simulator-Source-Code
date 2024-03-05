using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStamManager : MonoBehaviour
{
    float maxHealth = 5;
    public float currentHealth;
    public float maxdashes = 10;
    public float currentDashes;

    public Image healthBar;
    public Image staminaBar;
    public Image teleportBar;
    
    inventory inventoryCode;

    public float teleportCharges=5;
    public float maxCharges=5;
    
    movement movementCode;
    StatusManager statuses;
    public GreenBirdAnim animationControl;

    public GameObject CanvasToHide;
    // Start is called before the first frame update
    void Start()
    {
        currentDashes=maxdashes;
        currentHealth=maxHealth;
        teleportCharges=maxCharges;

        inventoryCode = GetComponent<inventory>();
        movementCode = GetComponent<movement>();
        statuses = GetComponent<StatusManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(staminaBar!=null){
        staminaBar.fillAmount = 0.1f*currentDashes;}
        healthBar.fillAmount = (1/maxHealth)*currentHealth;
        if(currentDashes>=maxdashes){
            currentDashes=maxdashes;
        }
        if(teleportBar!=null){
        teleportBar.fillAmount=(1/maxCharges)*teleportCharges;}
        if(currentHealth<=0){
            KillPlayer();
        }
    }

    public void gainStam(string itemName){
        for(int i=0;i<inventoryCode.items.Length;i++){
            if(inventoryCode.items[i].itemName==itemName){
                if(inventoryCode.items[i].cure==true){
                    GetComponent<StatusManager>().CurePoison();
                }
                currentDashes += inventoryCode.items[i].staminaRegen;
                inventoryCode.items[i].itemAmount-=1;
                if(inventoryCode.inventoryOpen){
                inventoryCode.inventoryList();}
            }
        }
    }

    public void gainHealth(string itemName){
        for(int i=0;i<inventoryCode.items.Length;i++){
            if(inventoryCode.items[i].itemName==itemName){
                currentHealth += inventoryCode.items[i].healthRegen;
                currentHealth = Mathf.Round(currentHealth);
                if(currentHealth>=maxHealth){
                    currentHealth=maxHealth;
                }
            }
        }
    }


    public void LoseStam(){
        currentDashes-=1;
    }

    public void GainCharge(){
        teleportCharges+=1;
        if(teleportCharges>=maxCharges){
            teleportCharges=maxCharges;
        }
    }

    public void KillPlayer(){
        if(GameData.egg!=null){
            GameData.egg.GetComponent<eggBehaviour>().DropEgg();
        }
        statuses.CurePoison();
        statuses.putOutFire();
        statuses.StopElectric();
        animationControl.Die();
        
        currentHealth = maxHealth;
        currentDashes=0;
        teleportCharges=0;
        movementCode.rbody.velocity=Vector3.zero;
        movementCode.moveAble=false;
        CanvasToHide.SetActive(false);

    }

    public void KillPlayer2(){
        transform.position=movementCode.SpawnLocation;
        transform.rotation=Quaternion.Euler(new Vector3(0,0,0));
        movementCode.moveAble=true;
        CanvasToHide.SetActive(true);
    }
}
