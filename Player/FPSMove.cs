using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMove : MonoBehaviour
{
    public float moveSpeed=10;
    
    float horizontalMovement;
    float verticalMovement;

    public float rbdrag=6f;
    public float movementMultiplyer = 10f;
    public float gravity = -20;

    Vector3 moveDirection;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput();
        ControlDrag();
    }

    void FixedUpdate(){
        MovePlayer();
    }

    void MoveInput(){
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward*verticalMovement+transform.right * horizontalMovement;
        Debug.Log(moveDirection);
    }

    void MovePlayer(){
        rb.AddForce(moveDirection.normalized*moveSpeed*movementMultiplyer);
        rb.AddForce(new Vector3(0,rb.mass*gravity,0));
    }
    void ControlDrag(){
        rb.drag = rbdrag;
    }
}
