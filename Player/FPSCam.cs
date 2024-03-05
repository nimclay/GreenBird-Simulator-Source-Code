using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCam : MonoBehaviour
{
    public float sensX =10;
    public float sensY =10;

    Camera cam;

    float mouseX;
    float mouseY;

    float multiplyer = 1f;

    float xRotation;
    float yRotation;

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        CamInput();
        cam.transform.localRotation = Quaternion.Euler(xRotation,0,0);
        Player.transform.rotation = Quaternion.Euler(0,yRotation,0);
    }

    void CamInput(){

        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX *sensX*multiplyer;
        xRotation += mouseY *sensY *multiplyer;
        xRotation = Mathf.Clamp(xRotation,-90,90);
    }
}
