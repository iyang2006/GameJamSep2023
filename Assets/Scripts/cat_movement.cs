using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat_movement : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody body;
    [SerializeField] private BoxCollider collid;
    [SerializeField] private float jumpStrength;
    private Vector3 movement;
    private bool grounded;
    private int jumpCount;
    private bool onWall;
    private bool leftWall;
    // private bool inJump;
    // private float jumpTime;
    // [SerializeField] private float jumpTimeLimit;

    // Start is called before the first frame update
    void Start()
    {
        grounded = true;
        jumpCount = 0;
        onWall = false;
        leftWall = true;
        //inJump = false;
        //jumpTime = 0;
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "floor_tag") {
            grounded = true;
            jumpCount = 0;
        }
        if (col.gameObject.tag == "wall_tag") {
            onWall = true;
        }
    }

    void OnCollisionExit(Collision col) {
        if (col.gameObject.tag == "floor_tag") {
            grounded = false;
        }
        if (col.gameObject.tag == "wall_tag") {
            onWall = false;
        }
    }

    // Update is called once per frame
    void Update()
    {   
        // if (Time.time - jumpTime >= jumpTimeLimit) {
        //     jumpTime = 0;
        //     inJump = false;
        // }

        //horizontal movement
        if (Input.GetKey(KeyCode.LeftArrow) == Input.GetKey(KeyCode.RightArrow)) {
            movement.x = 0;
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) {
            movement.x = -1;
        }
        else {
            movement.x = 1;
        }

        //jumping
        if (Input.GetKey(KeyCode.UpArrow)) {
            if (grounded == true) {
                grounded = false;
                Debug.Log("Jump");
                Vector3 vel = body.velocity;
                vel.y = 5 * jumpStrength;
                body.velocity = vel;
            }
            else if (onWall == true && jumpCount < 3) {
                // inJump = true;
                // jumpTime = Time.time;
                onWall = false;
                Debug.Log("Wall jump");
                jumpCount += 1;
                Debug.Log(jumpCount);
                Vector3 vel = body.velocity;
                vel.y = 5 * jumpStrength;
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
}
