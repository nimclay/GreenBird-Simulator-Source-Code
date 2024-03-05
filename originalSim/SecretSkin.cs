using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretSkin : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip clip;

    public int skinnum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.JoystickButton0))){
            if(GetComponent<interactable>().ObjInteractable){
                Gather();
            }
        }
    }

    void Gather(){
        audiosource.PlayOneShot(clip,Variables.volume);
        Variables.unlockedskins[skinnum] = true;
        SaveGame.Save();
        SaveGame.SaveSkins(Variables.unlockedskins);
        if(GetComponent<interactable>().ui!=null){
            Destroy(GetComponent<interactable>().ui);
        }
        Destroy(gameObject);

    }
}
