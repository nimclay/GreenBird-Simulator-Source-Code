using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PushingObstacle : MonoBehaviour {

    private Vector3 startPoint; // the current position when loading the scene

    [SerializeField]
    private Vector3 endPoint; // the target point

    [SerializeField]
    private float forwardSpeed; // speed when moving to the end

    [SerializeField]
    private float backwardSpeed; // speed when moving back to start

    float currentSpeed; // the current speed (forward/backward)

    private Vector3 direction; // the direction the wall is moving
    private Vector3 destination; // the target point

    private Rigidbody obstacleRigid; // rigidbody of the wall


    public bool playable=false;
    public bool rotate;
    private void Start()
    {
        startPoint = transform.position;
        obstacleRigid = GetComponent<Rigidbody>();
        SetDestination(endPoint); // set the target point
    }

    private void FixedUpdate()
    {
        if(playable){
            if(rotate){
                Quaternion deltaRotation = Quaternion.Euler(endPoint * Time.fixedDeltaTime);
            obstacleRigid.MoveRotation(obstacleRigid.rotation * deltaRotation);
            }
        else{obstacleRigid.MovePosition(transform.position + direction * currentSpeed * Time.fixedDeltaTime); // start moving

        if (Vector3.Distance(obstacleRigid.position, destination) < currentSpeed * Time.fixedDeltaTime){ // set a new target point
            SetDestination(destination == startPoint ? endPoint : startPoint);}}}
        else{
            if(transform.position!=startPoint){
                transform.position=startPoint;
            }
        }
    }

    private void SetDestination(Vector3 destinationPoint)
    {
        
        destination = destinationPoint;
        direction = (destination - transform.position).normalized; // set the movement direction
        currentSpeed = destination == endPoint ? forwardSpeed : backwardSpeed; // set the speed
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.B)){
            playable=!playable;
        }
    }
}
