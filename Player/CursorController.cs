using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    public Rigidbody2D rbody;
    public float speed = 10;

    public bool active;
    Image imageCursor;
    public bool menu;


    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        imageCursor = GetComponent<Image>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(reset());
        if(!menu){
        active = false;}
        else{
            active=true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(active){
            if(imageCursor.enabled==false){
                imageCursor.enabled = true;
            }
            if(Input.GetKeyDown(KeyCode.Alpha1)){
                StartCoroutine(reset());
            }
        }
        else{
            if(imageCursor.enabled==true){
                rbody.velocity=Vector2.zero;
                imageCursor.enabled = false;
                transform.localPosition = Vector2.zero;
            }
        }
    }

    void FixedUpdate(){
        if(active){
        rbody.velocity = new Vector2((Input.GetAxis("Joystick x")+Input.GetAxis("Mouse X"))*speed, (Input.GetAxis("Joystick y")+Input.GetAxis("Mouse Y"))*speed);}
    }
    IEnumerator reset(){
        yield return new WaitForSeconds(0.1f);
        rbody.velocity=Vector2.zero;
        transform.localPosition = Vector2.zero;
        Input.ResetInputAxes();
        
    }
}
