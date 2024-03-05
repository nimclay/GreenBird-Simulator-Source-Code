using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{

    private bool invinsible=false;

    public ParticleSystem hitEffect;
    private Collider col;
    public GameObject RemoveThing;
    public GameObject OrbPref;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(){
        if(!invinsible){
            invinsible = true;
            StartCoroutine(Buffer());
            GetHit();
        }
    }

    IEnumerator Buffer(){
        invinsible = true;
        yield return new WaitForSeconds(0.3f);
        invinsible = false;
    }

    void GetHit(){
        RemoveThing.SetActive(false);
        col.enabled = false;
        hitEffect.Play();
        StartCoroutine(resetThing());
        SpawnOrb();
    }

    void SpawnOrb(){
        int NumOfOrb = Random.Range(1,5);
        for(int i = 0; i<=NumOfOrb;i++){
            Vector3 size = getSize();
            GameObject orb = Instantiate(OrbPref,transform.position,Quaternion.Euler(Vector3.zero));
            orb.GetComponent<EXPORB>().playing = true;
            orb.transform.localScale = size;

        }
    }
    IEnumerator resetThing(){
        yield return new WaitForSeconds(180f);
        Reset();
    }
    Vector3 getSize(){
        float scale = Random.Range(0.4f,1);
        Vector3 size = new Vector3(scale,scale,scale);
        return size;
    }
    void Reset(){
        col.enabled = true;
        RemoveThing.SetActive(true);
    }
}
