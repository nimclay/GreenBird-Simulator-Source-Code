using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkins : MonoBehaviour
{
    public static Material body;
    public static Material foot;
    public static Material beak;
    public static Material eye;
    public static Material eyeOutside;

    public GameObject[] PlayerBodyAndArms;
    public GameObject PlayerHead;
    public GameObject[] Playerlegs;

    public static bool Mats = false;

    // Start is called before the first frame update
    void Start()
    {
        if(Mats){
        ChangeMaterialsPlayer();}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChangeMaterialsPlayer(){
        for(int i = 0; i < PlayerBodyAndArms.Length; i++){
            var material = PlayerBodyAndArms[i].GetComponent<Renderer>().materials;
            material[0] = body;
            PlayerBodyAndArms[i].GetComponent<Renderer>().materials = material;
        }
        var headmats = PlayerHead.GetComponent<Renderer>().materials;
        headmats[2] = beak;
        headmats[3] = eyeOutside;
        headmats[1] = body;
        headmats[0] = eye;
        PlayerHead.GetComponent<Renderer>().materials = headmats;
        for(int x = 0; x < Playerlegs.Length; x++){
            var legMats = Playerlegs[x].GetComponent<Renderer>().materials;
            legMats[4] = foot;
            legMats[1] = foot;
            legMats[2] = foot;
            legMats[3] = foot;
            legMats[0] = body;
            Playerlegs[x].GetComponent<Renderer>().materials = legMats;
        }
    }
}
