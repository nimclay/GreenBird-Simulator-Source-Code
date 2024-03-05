using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPlat : MonoBehaviour
{
    public Vector3 endPos;
    public Vector3 endRot;
    Vector3 startPos;
    Vector3 startRot;
    interactable interCode;
    public inventory invCode;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation.eulerAngles;
        interCode = GetComponent<interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(interCode.ObjInteractable){
            if(Input.GetKeyDown(KeyCode.E)){
                Collect();
            }
        }
    }

    void Collect(){
        transform.localPosition = endPos;
        transform.localRotation = Quaternion.Euler(endRot);
        Destroy(interCode.ui);
        interCode.ObjInteractable=false;    
        interCode.enabled=false;
        invCode.collectItem(true);

        StartCoroutine(cooldown());
    }
    void Reset(){
        transform.localPosition = startPos;
        transform.localRotation = Quaternion.Euler(startRot);
        interCode.enabled=true;
    }

    IEnumerator cooldown(){
        yield return new WaitForSeconds(60f);
        Reset();
    }
}
