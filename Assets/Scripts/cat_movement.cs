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

    // Start is called before the first frame update
    void Start()
    {
        grounded = true;
        jumpCount = 0;
        onWall = false;
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
                Debug.Log("Jump");
                Vector3 vel = body.velocity;
                vel.y = 5 * jumpStrength;
                body.velocity = vel;
            }
            else if (onWall == true && jumpCount < 3) {
                Debug.Log("Wall jump");
                jumpCount += 1;
                Debug.Log(jumpCount);
                Vector3 vel = body.velocity;
                vel.y = 5 * jumpStrength;
                body.velocity = vel;
            }
        }

        body.MovePosition(body.position + (movement * moveSpeed * Time.fixedDeltaTime));

    }
}
