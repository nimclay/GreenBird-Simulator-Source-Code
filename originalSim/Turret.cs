using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject head;
    public GameObject Player;
    bool active;
    public ParticleSystem AttackEffects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(active){
        if(GameData2.SnowBird){
            if(!AttackEffects.isPlaying){
                AttackEffects.Play();
            }
            head.transform.LookAt(Player.transform.position);
        }}

        float distance = Vector3.Distance(head.transform.position,Player.transform.position);
        if(distance<=50){
            active = true;
        }
        else{
            active = false;
            if(AttackEffects.isPlaying){
                AttackEffects.Stop();
            }
        }
    }
}
