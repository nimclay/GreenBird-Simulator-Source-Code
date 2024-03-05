using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{

    public float standardSensX = 400;
    public float standardSensY = 400;
    public float standardDisance = 6;
    public float standardVolume = 0.5f;
    public float standardMusicVolume = 1;

    public Image SensXBar;
    public Image SensYBar;
    public Image CamDistanceBar;
    public Image VolumeBar;
    public Image MusicVolumeBar;
    public Image SelectThing;

    public Image activeImage;
    public float selectDistance=1;

    public Fade MenuFade;

    public KeyCode AccessMenuKeyBoard;
    public KeyCode AccessMenuController;
    public KeyCode SelectKeyBoard;
    public KeyCode SelectController;

    public string[] order;

    private bool selected;
    private bool scrollable = true;
    private bool MenuActive;

    private int currentorder=0;

    public Color selectedColor;
    public Color nonselectedcolor;

    public movement moveCode;
    public CameraMove cam;

    public MusicManager musicMan;
    // Start is called before the first frame update
    void Start()
    {
        Deselect();
    }

    // Update is called once per frame
    void Update()
    {
        if(MenuActive){
            if(scrollable){
                if(Input.GetKeyDown(KeyCode.UpArrow)||Input.GetAxis("NumPad Y")>0){
                    Scroll(-1);
                }
                else if(Input.GetKeyDown(KeyCode.DownArrow)||Input.GetAxis("NumPad Y")<0){
                    Scroll(1);
                }
                if(Input.GetKeyDown(SelectController)||Input.GetKeyDown(SelectKeyBoard)){
                    Select();
                }
            }
            else if(selected){
                if((Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetAxis("NumPad X")<0)){
                    ChangeValues(-0.1f);
                }
                else if((Input.GetKeyDown(KeyCode.RightArrow)||Input.GetAxis("NumPad X")>0)){
                    ChangeValues(0.1f);
                }
                if(Input.GetKeyDown(SelectController)||Input.GetKeyDown(SelectKeyBoard)){
                    Deselect();
                }
                
            }
        }

        if(Input.GetKeyDown(AccessMenuController)||Input.GetKeyDown(AccessMenuKeyBoard)){
            MenuActive = !MenuActive;
            if(MenuActive){
                TurnOnMenu();
            }
            else{
                TurnOffMenu();
            }
        }

    }

    IEnumerator scrollDelay(){
        scrollable = false;
        yield return new WaitForSeconds(0.1f);
        scrollable = true;
    }

    void Scroll(int value){
        currentorder+=value;
        if(currentorder>=order.Length){
            currentorder = 0;
        }
        if(currentorder<0){
            currentorder = order.Length-1;
        }
        CheckCurrentEntity();
        SelectThing.transform.localPosition = new Vector2(0,70-currentorder*selectDistance);
    }
    void Select(){
        selected = true;
        scrollable = false;
        SelectThing.color = selectedColor;
    }

    void ChangeValues(float changeamount){
        CheckCurrentEntity();
        activeImage.fillAmount = activeImage.fillAmount+changeamount;
    }
    void Deselect(){
        selected = false;
        scrollable = true;
        SelectThing.color = nonselectedcolor;
    }

    void CheckCurrentEntity(){
        if(order[currentorder]=="xsens"){
            activeImage = SensXBar;
        }
        else if(order[currentorder]=="ysens"){
            activeImage = SensYBar;
        }
        else if(order[currentorder]=="distance"){
            activeImage = CamDistanceBar;
        }
        else if(order[currentorder]=="volume"){
            activeImage = VolumeBar;
        }
        else if(order[currentorder]=="music volume"){
            activeImage = MusicVolumeBar;
        }
    }


    void TurnOnMenu(){
        MenuFade.ShowUI();
        moveCode.moveAble=false;
        moveCode.rbody.velocity = Vector3.zero;
        cam.camMoveAble=false;
        float sfxvol = Variables.volume;
        VolumeBar.fillAmount = sfxvol;
        float musicvol = Variables.musicVolume;
        MusicVolumeBar.fillAmount = musicvol;
        SensXBar.fillAmount = ((Variables.xsensitivity-200)/400);
        SensYBar.fillAmount = ((Variables.ysensitivity-200)/400);
        CamDistanceBar.fillAmount = ((Variables.camdistance-4)/(2/0.3f));
    }
    void TurnOffMenu(){
        MenuFade.hideUI();
        moveCode.moveAble=true;
        cam.camMoveAble=true;
        ApplyValues();
        Deselect();
    }

    void ApplyValues(){
        float sfxvol = VolumeBar.fillAmount;
        float musicvol = MusicVolumeBar.fillAmount;
        float sensx = 200+SensXBar.fillAmount*400;
        float sensy = 200+SensYBar.fillAmount*400;
        float distance =  4+CamDistanceBar.fillAmount*(2/0.3f);
        Variables.camdistance = distance;
        Variables.xsensitivity = sensx;
        Variables.ysensitivity = sensy;
        Variables.volume = sfxvol;
        Variables.musicVolume = musicvol;
        musicMan.ChangeSound(musicMan.lastSound);
        cam.UpDateVariables();
    }
}
