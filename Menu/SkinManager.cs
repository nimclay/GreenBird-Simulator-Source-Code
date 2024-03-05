using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    [System.Serializable]
    public class Item{
        public string name;
        public Material body;
        public Material eye;
        public Material eyeOutside;
        public Material beak;
        public Material foot;
        public int price;
        public bool unlocked;
        public bool unpurchaseable;
        public Statistics stats;
    }
    [System.Serializable]
    public class Statistics{
        public float bonusMoveSpeed=0;
        public float bonusDamage=0;
        public float bonusJumpHeight=0;
        //mission stats
        public float bonusMissionTime=0;
        public float bonusHealth=0;
        public float bonusTimeDegradation=0;
        public float bonusDifficulty=0;
    }




    public Item[] Skins;

    int currentNumber =0;
    public TextMeshPro CostText;
    public TextMeshPro Name;
    public TextMeshPro unlockedText;
    public TextMeshPro CurrentCoinText;
    public TextMeshPro PerksText;

    public Color good;
    public Color bad;


    public GameObject[] BodyAndArms;
    public GameObject Head;
    public GameObject[] legs;

    public GameObject[] PlayerBodyAndArms;
    public GameObject PlayerHead;
    public GameObject[] Playerlegs;

    private bool pressable=true;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("started") == 1){
            SaveGame.Load();
            LoadSkins();
        }
        else{
            SaveGame.SaveSkins(SaveSkins());
        }
        ChangeMaterials();
        SaveGame.Load();

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow)||Input.GetAxis("HorizontalArrows")<0){
            Prev();
        }
        else if(Input.GetKey(KeyCode.RightArrow)||Input.GetAxis("HorizontalArrows")>0){
            Next();
        }
    }

    public void Equip(){
        if(Skins[currentNumber].unlocked){
        ChangeMaterialsPlayer();}
    }
    public void Next(){
        if(pressable){
            pressable=false;
            StartCoroutine(ButtonDelay());
        currentNumber+=1;
        if(currentNumber>Skins.Length-1){
            currentNumber = 0;
        }}
        ChangeMaterials();
    }
    public void Prev(){
        if(pressable){
            pressable=false;
            StartCoroutine(ButtonDelay());
        currentNumber-=1;
        if(currentNumber<0){
            currentNumber = Skins.Length-1;
        }
        ChangeMaterials();}
    }
    public void Purchase(){
        if(Skins[currentNumber].unlocked==false){
            if(!Skins[currentNumber].unpurchaseable){
            if(Variables.coins >= Skins[currentNumber].price){
            Skins[currentNumber].unlocked = true;
            Variables.coins -= Skins[currentNumber].price;}}
            ChangeMaterials();
            SaveGame.SaveSkins(SaveSkins());
            SaveGame.Save();
        }
    }
    IEnumerator ButtonDelay(){
        yield return new WaitForSeconds(0.2f);
        pressable  =true;
    }

    void ChangeMaterials(){
        for(int i = 0; i < BodyAndArms.Length; i++){
            var material = BodyAndArms[i].GetComponent<Renderer>().materials;
            material[0] = Skins[currentNumber].body;
            BodyAndArms[i].GetComponent<Renderer>().materials = material;
        }
        var headmats = Head.GetComponent<Renderer>().materials;
        headmats[2] = Skins[currentNumber].beak;
        headmats[3] = Skins[currentNumber].eyeOutside;
        headmats[1] = Skins[currentNumber].body;
        headmats[0] = Skins[currentNumber].eye;
        Head.GetComponent<Renderer>().materials = headmats;
        for(int x = 0; x < legs.Length; x++){
            var legMats = legs[x].GetComponent<Renderer>().materials;
            legMats[4] = Skins[currentNumber].foot;
            legMats[1] = Skins[currentNumber].foot;
            legMats[2] = Skins[currentNumber].foot;
            legMats[3] = Skins[currentNumber].foot;
            legMats[0] = Skins[currentNumber].body;
            legs[x].GetComponent<Renderer>().materials = legMats;
        }
        if(Skins[currentNumber].unpurchaseable){
            CostText.text = "secret";
        }
        else{CostText.text = ""+Skins[currentNumber].price;}
        CurrentCoinText.text = ""+Variables.coins;
        SetStatisticsText();
        if(Skins[currentNumber].price <= Variables.coins){
            CostText.color = good;
        }
        else{
            CostText.color = bad;
        }
        if(Skins[currentNumber].unlocked==true){
            unlockedText.text = "unlocked";
            unlockedText.color = good;
            CostText.text = "";
        }
        else{
            unlockedText.text = "locked";
            unlockedText.color = bad;
        }
        Name.text = ""+Skins[currentNumber].name;
    }
    void ChangeMaterialsPlayer(){
        SaveMaterial();
        for(int i = 0; i < PlayerBodyAndArms.Length; i++){
            var material = PlayerBodyAndArms[i].GetComponent<Renderer>().materials;
            material[0] = Skins[currentNumber].body;
            PlayerBodyAndArms[i].GetComponent<Renderer>().materials = material;
        }
        var headmats = PlayerHead.GetComponent<Renderer>().materials;
        headmats[2] = Skins[currentNumber].beak;
        headmats[3] = Skins[currentNumber].eyeOutside;
        headmats[1] = Skins[currentNumber].body;
        headmats[0] = Skins[currentNumber].eye;
        PlayerHead.GetComponent<Renderer>().materials = headmats;
        for(int x = 0; x < Playerlegs.Length; x++){
            var legMats = Playerlegs[x].GetComponent<Renderer>().materials;
            legMats[4] = Skins[currentNumber].foot;
            legMats[1] = Skins[currentNumber].foot;
            legMats[2] = Skins[currentNumber].foot;
            legMats[3] = Skins[currentNumber].foot;
            legMats[0] = Skins[currentNumber].body;
            Playerlegs[x].GetComponent<Renderer>().materials = legMats;
        }
        LoadStatistics();
    }

    void SaveMaterial(){
        PlayerSkins.Mats = true;
        PlayerSkins.foot = Skins[currentNumber].foot;
        PlayerSkins.body = Skins[currentNumber].body;
        PlayerSkins.beak = Skins[currentNumber].beak;
        PlayerSkins.eye = Skins[currentNumber].eye;
        PlayerSkins.eyeOutside = Skins[currentNumber].eyeOutside;
    }
    bool[] SaveSkins(){
        bool[] skins= new bool[Skins.Length];
        for(int i = 0; i<skins.Length;i++){
            skins[i] = Skins[i].unlocked;
        }
        return skins;
    }
    void LoadSkins(){
        bool[] unlockedSkins = Variables.unlockedskins;
        for(int i = 0; i < unlockedSkins.Length; i++){
            Skins[i].unlocked = unlockedSkins[i];
        }
    }

    void LoadStatistics(){
        Variables.bonusMoveSpeed = Skins[currentNumber].stats.bonusMoveSpeed;
        Variables.bonusDamage = Skins[currentNumber].stats.bonusDamage;
        Variables.bonusJumpHeight = Skins[currentNumber].stats.bonusJumpHeight;
        Variables.bonusMissionTime = Skins[currentNumber].stats.bonusMissionTime;
        Variables.bonusHealth = Skins[currentNumber].stats.bonusHealth;
        Variables.bonusTimeDegradation = Skins[currentNumber].stats.bonusTimeDegradation;
        Variables.bonusDifficulty = Skins[currentNumber].stats.bonusDifficulty;
    }


    void SetStatisticsText(){
        string text = "";
        bool perk=false;
        if(Skins[currentNumber].stats.bonusMoveSpeed!=0){
            if(Skins[currentNumber].stats.bonusMoveSpeed>0){
                text += "Move Speed: <color=green>"+"+"+Skins[currentNumber].stats.bonusMoveSpeed+"</color>\n";
            }
            else if(Skins[currentNumber].stats.bonusMoveSpeed<0){
                text += "Move Speed: <color=red>"+"-"+Skins[currentNumber].stats.bonusMoveSpeed+"</color>\n";
            }
            perk=true;
        }
        if(Skins[currentNumber].stats.bonusDamage!=0){
            if(Skins[currentNumber].stats.bonusDamage>0){
                text += "Damage: <color=green>"+"+"+Skins[currentNumber].stats.bonusDamage+"</color>\n";
            }
            else if(Skins[currentNumber].stats.bonusDamage<0){
                text += "Damage: <color=red>"+"-"+Skins[currentNumber].stats.bonusDamage+"</color>\n";
            }
            perk=true;
        }
        if(Skins[currentNumber].stats.bonusJumpHeight!=0){
            if(Skins[currentNumber].stats.bonusJumpHeight>0){
                text += "Jump Height: <color=green>"+"+"+Skins[currentNumber].stats.bonusJumpHeight+"</color>\n";
            }
            else if(Skins[currentNumber].stats.bonusJumpHeight<0){
                text += "Jump Height: <color=red>"+"-"+Skins[currentNumber].stats.bonusJumpHeight+"</color>\n";
            }
            perk=true;
        }
        if(Skins[currentNumber].stats.bonusMissionTime!=0){
            if(Skins[currentNumber].stats.bonusMissionTime>0){
                text += "Mission Time: <color=green>"+"+"+Skins[currentNumber].stats.bonusMissionTime+"</color>\n";
            }
            else if(Skins[currentNumber].stats.bonusMissionTime<0){
                text += "Mission Time: <color=red>"+"-"+Skins[currentNumber].stats.bonusMissionTime+"</color>\n";
            }
            perk=true;
        }
        if(Skins[currentNumber].stats.bonusHealth!=0){
            if(Skins[currentNumber].stats.bonusHealth>0){
                text += "Bird Health: <color=green>"+"+"+Skins[currentNumber].stats.bonusHealth+"</color>\n";
            }
            else if(Skins[currentNumber].stats.bonusHealth<0){
                text += "Bird Health: <color=red>"+"-"+Skins[currentNumber].stats.bonusHealth+"</color>\n";
            }
            perk=true;
        }
        if(Skins[currentNumber].stats.bonusTimeDegradation!=0){
            if(Skins[currentNumber].stats.bonusTimeDegradation>0){
                text += "Time Degradation: <color=red>"+"+"+Skins[currentNumber].stats.bonusTimeDegradation+"</color>\n";
            }
            else if(Skins[currentNumber].stats.bonusTimeDegradation<0){
                text += "Time Degradation: <color=green>"+"-"+Skins[currentNumber].stats.bonusTimeDegradation+"</color>\n";
            }
            perk=true;
        }
        if(Skins[currentNumber].stats.bonusDifficulty!=0){
            if(Skins[currentNumber].stats.bonusDifficulty>0){
                text += "Difficulty: <color=red>"+"+"+Skins[currentNumber].stats.bonusDifficulty+"</color>\n";
            }
            else if(Skins[currentNumber].stats.bonusDifficulty<0){
                text += "Difficulty: <color=green>"+"-"+Skins[currentNumber].stats.bonusDifficulty+"</color>\n";
            }
            perk=true;
        }
        if(!perk){
            text = "No Perks";
        }
        PerksText.text = text;
    }
}
