using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat_movement : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody body;
    [SerializeField] private Transform trans;
    [SerializeField] private float rayLength;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float poundIntensity;
    private Vector3 movement;
    [SerializeField] private bool grounded;
    private int jumpCount;
    private bool onWall;
    public bool leftWall;
    [SerializeField] private float wallJumpStrength;
    private bool inJump;
    private float jumpTime;
    [SerializeField] private float jumpTimeLimit;

    // Start is called before the first frame update
    void Start()
    {
        grounded = true;
        jumpCount = 0;
        onWall = false;
        leftWall = true;
        inJump = true;
    }

    private LayerMask mask = LayerMask.GetMask("cat");

    void OnCollisionEnter(Collision col) {
        if ((col.gameObject.layer == LayerMask.NameToLayer("platforms")) || (col.gameObject.layer == LayerMask.NameToLayer("boxes"))) {
            if (Physics.Raycast(trans.position, (trans.up * -1), rayLength, ~mask)) {
                Debug.Log("cat grounded");
                grounded = true;
                jumpCount = 0;
            }
            else if (Physics.Raycast(trans.position, (trans.right), rayLength, ~mask) || Physics.Raycast(trans.position, (trans.right * -1), rayLength, ~mask)) {
                onWall = true;
            }
        }
    }

    void OnCollisionExit(Collision col) {
        if ((col.gameObject.layer == LayerMask.NameToLayer("platforms")) || (col.gameObject.layer == LayerMask.NameToLayer("boxes"))) {
            if (Physics.Raycast(trans.position, (trans.up * -1), rayLength, ~mask)
            && !(Physics.Raycast(trans.position, (trans.right), rayLength, ~mask) || Physics.Raycast(trans.position, (trans.right * -1), rayLength, ~mask))) {
                grounded = false;
            }
            else if (Physics.Raycast(trans.position, (trans.right), rayLength, ~mask) || Physics.Raycast(trans.position, (trans.right * -1), rayLength, ~mask)) {
                onWall = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if (Physics.Raycast(trans.position, (trans.up * -1), rayLength, ~mask)) {
            grounded = true;
        }     

        if (!(Physics.Raycast(trans.position, (trans.right), rayLength, ~mask) || Physics.Raycast(trans.position, (trans.right * -1), rayLength, ~mask))) {
            onWall = false;
        }

        if (Time.time - jumpTime >= jumpTimeLimit) {
            jumpTime = 0;
            inJump = false;
        }

        if (Physics.Raycast(trans.position, (trans.up * -1), rayLength, ~mask) == false) {
            grounded = false;
        }

        //horizontal movement
        if (inJump == false) {
            if (Input.GetKey(KeyCode.LeftArrow) == Input.GetKey(KeyCode.RightArrow)) {
                movement.x = 0;
            }
            else if (Input.GetKey(KeyCode.LeftArrow)) {
                movement.x = -1;
                leftWall = true;
            }
            else {
                movement.x = 1;
                leftWall = false;
            }
        }

        //jumping
        if (Input.GetKey(KeyCode.UpArrow)) {
            if (grounded == true) {
                grounded = false;
                Debug.Log("cat jump");
                Vector3 vel = body.velocity;
                vel.y = 5 * jumpStrength;
                body.velocity = vel;
            }
            else if (onWall == true && jumpCount < 3) {
                inJump = true;
                movement.x = 0;
                jumpTime = Time.time;
                onWall = false;
                Debug.Log("wall jump");
                jumpCount += 1;
                Debug.Log(jumpCount);
                Vector3 vel = body.velocity;
                vel.y = 5 * wallJumpStrength;
                if (leftWall == true) {
                    vel.x = 2 * jumpStrength;
                }
                else {
                    vel.x = -2 * jumpStrength;
                }
                body.velocity = vel;
            }
        }

        body.MovePosition(body.position + (movement * moveSpeed * Time.fixedDeltaTime));

    }

    public void Bomb(float intensity) {
        if (grounded) {
            grounded = false;
            Vector3 vel = body.velocity;
            vel.y = poundIntensity * intensity;
            body.velocity = vel;
        }
    }
}
