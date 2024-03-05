using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
 
public class CameraMove : MonoBehaviour
{
 
    public float YMin = -7f;
    private const float YMax = 80.0f;
 
    public Transform lookAt;
    public Transform bugLook;
 
    public Transform Player;
 
    public float distance = 10.0f;
    public float bugDistance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    public float xsensitivity = 4.0f;
    public float ysensitivity= 4.0f;

    public bool invertx;
    public bool inverty;

    public bool bugMode;
    
    public bool camMoveAble=true;
    public float[] distances;


    Vector3 relativePos;
     public Transform target;
    // public float distance = 3f;
     public float distanceOffset;
     public float zoomSpeed = 2f;
     public float xSpeed = 300f;
     public float ySpeed = 300f;
     public float yMinLimit = 50f;
     public float yMaxLimit = 180f;
    public LayerMask myLayerMask; 

    public GameObject NeonFilter;
    public GameObject InvertFilter;
    public float verticalOffset = 0.3f;
    public float bonusDistance;
    // Start is called before the first frame update
    void Start()
    {
        UpDateVariables();
    
        GetComponent<Camera>().layerCullDistances = distances;
        GetComponent<PostProcessLayer>().enabled = Variables.postProcessing;
        Cursor.lockState = CursorLockMode.Locked;
 
    }
    private float refrerenc = 0.0f;
    void Update()
     {
         relativePos = transform.position - (target.position);
         RaycastHit hit;
         if (Physics.Raycast(target.position, relativePos, out hit, distance + 0.5f,myLayerMask))
         {
             float offset = distance - hit.distance;
             offset = Mathf.Clamp(offset, 0, distance-2f);
             distanceOffset = Mathf.SmoothDamp(distanceOffset,offset,ref refrerenc,0.1f);
         }
         else
         {
             distanceOffset = 0;
         }
     }
 
    // Update is called once per frame
    void LateUpdate()
    {
        if(camMoveAble){
        currentX += Input.GetAxis("Mouse X") * xsensitivity * Time.deltaTime;
       currentX += Input.GetAxis("Joystick x")*xsensitivity*Time.deltaTime;
        currentY += Input.GetAxis("Mouse Y") * ysensitivity * Time.deltaTime;
        currentY += Input.GetAxis("Joystick y")*ysensitivity*Time.deltaTime;
 
        currentY = Mathf.Clamp(currentY, YMin, YMax);
        
        if(bugMode){
            Vector3 Direction = new Vector3(0, 0, -bugDistance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = bugLook.position + rotation * Direction;
 
        transform.LookAt(bugLook.position);
        }
        else{
        Vector3 Direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        //transform.position = lookAt.position + rotation * Direction;
        var position = rotation * new Vector3(0.0f, 0.0f, -distance + distanceOffset) + lookAt.position;
        transform.position = position;

        Vector3 Look = new Vector3(lookAt.position.x,lookAt.position.y+verticalOffset,lookAt.position.z);
 
        transform.LookAt(Look);}}
        //GetComponent<AudioSource>().pitch = 1+(Player.gameObject.GetComponent<movement>().additionalspeed/Player.gameObject.GetComponent<movement>().speed);

 
     
 
    }

     public void NeonOn(){
        NeonFilter.SetActive(true);
    }
    public void NeonOff(){
        NeonFilter.SetActive(false);
    }
    public void InvertOn(){
        InvertFilter.SetActive(true);
    }
    public void InvertOff(){
        InvertFilter.SetActive(false);
    }


    public void UpDateVariables(){
        distance = Variables.camdistance+bonusDistance;
        xsensitivity = Variables.xsensitivity;
        ysensitivity = Variables.ysensitivity;
        if(invertx){
            xsensitivity = -xsensitivity;
        }
        if(inverty){
            ysensitivity = -ysensitivity;
        }

    }


   

    
}