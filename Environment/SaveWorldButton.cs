using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveWorldButton : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    private AudioSource audiosource;
    [SerializeField] private Material pressedMat;

    private bool activated=false;

    public GameObject Monsters;
    public MusicManager music;
    public Material SkyBoxMat;

    public Color fogColor;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();

    }
    void OnTriggerEnter(Collider other){
        if(!activated){
            activated = true;
            Pressed();
        }
    }
    void Pressed(){
        audiosource.PlayOneShot(clip,0.5f);
        GetComponent<Renderer>().material = pressedMat;
        music.ChangeSound("default2");
        Monsters.SetActive(false);
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogStartDistance = 10;
        RenderSettings.fogEndDistance = 500;
        RenderSettings.skybox = SkyBoxMat;
        Variables.WorldSaved = true;
        
    }
}
